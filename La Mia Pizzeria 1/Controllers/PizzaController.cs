using Azure;
using La_Mia_Pizzeria_1.DataBase;
using La_Mia_Pizzeria_1.Models;
using La_Mia_Pizzeria_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Runtime.ConstrainedExecution;

namespace La_Mia_Pizzeria_1.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {

            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzaList = db.Pizzas.ToList();
                //List<Pizza> pizzaList = new List<Pizza>();

                return View("Index", pizzaList);
            }
           
        }

        public IActionResult Dettaglio(int id)
        {

            /*
            bool FunzioneDiRicercaPizzaById(Pizza pizza)
            {
                return pizza.Id == id;
            }
            */

            using (PizzaContext db = new PizzaContext())

            {
                // LINQ: syntax methos
                Pizza PizzaTrovata = db.Pizzas
                    .Where(SingolaPizzaNelDb => SingolaPizzaNelDb.Id == id)
                    .Include(pizza=> pizza.Categoria)
                    .Include(pizza=> pizza.Ingredientis)
                    .FirstOrDefault();

                // LINQ: query syntax
                /* Pizza pizzaTrovato =
                     (from p in db.pizza
                      where p.Id == id
                      select p).FirstOrDefault<pizza>();*/

                // SQL QUERY
                /* Pizza pizzaTrovato =
                     db.pizzas.FromSql($"SELECT * FROM pizzas WHERE Id = {id}")
                     .FirstOrDefault<Pizza>(); */

                if (PizzaTrovata != null)
                {
                    return View(PizzaTrovata);
                }

                return NotFound("La pizza con l'id cercato non esiste!");

            }
            
           

        }


         
            [HttpGet]
        public IActionResult Create()
        {
            using(PizzaContext db = new PizzaContext())
            {
                List<Categoria> CategoriasFromDB = db.Categorias.ToList<Categoria>();

                PizzaCtegorieView modelForView = new PizzaCtegorieView();
                modelForView.Pizza = new Pizza();

                modelForView.Categorias = CategoriasFromDB;
                modelForView.Ingredientis = IngredientisConvertor.getListIngredientisForMultipleSelect();

                return View("Create", modelForView);
            }
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCtegorieView formData)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Categoria> categories = db.Categorias.ToList<Categoria>();

                    formData.Categorias = categories;


                    formData.Ingredientis = IngredientisConvertor.getListIngredientisForMultipleSelect();
                }


                return View("Create", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                if(formData.IngredientisSelectedFromMultipleSelect != null)
                {
                    formData.Pizza.Ingredientis = new List<Ingredienti>();

                    foreach(string ingredientiId in formData.IngredientisSelectedFromMultipleSelect)
                    {
                        int ingredientiIdIntFromSelcet = int.Parse(ingredientiId);

                        Ingredienti ingredienti = db.Ingredientis.Where(ingredientiDb => ingredientiDb.Id == ingredientiIdIntFromSelcet).FirstOrDefault();

                        // todo controllare eventuali altri errori tipo l'id del tag non esiste

                        formData.Pizza.Ingredientis.Add(ingredienti);


                    }


                }


                db.Pizzas.Add(formData.Pizza);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(int id) 
        { 
            using(PizzaContext db = new PizzaContext())
            {
                Pizza pizzaUpdate = db.Pizzas.Where(articolo => articolo.Id == id).Include(pizza => pizza.Ingredientis).FirstOrDefault();

                if (pizzaUpdate == null)
                {
                    return NotFound("Pizza non Trovata!");
                }
                List<Categoria> categories = db.Categorias.ToList<Categoria>();

                PizzaCtegorieView modelForView = new PizzaCtegorieView();

                modelForView.Pizza = pizzaUpdate;
                modelForView.Categorias = categories;

                List<Ingredienti> listIngredientiFromDb = db.Ingredientis.ToList<Ingredienti>();

                List<SelectListItem> listaOpzioniPerLaSelect = new List<SelectListItem>();

                foreach (Ingredienti ingredienti in listIngredientiFromDb)
                {
                    // Ricerco se il ingredienti che sto inserindo nella lista delle opzioni della select era già stato selezionato dall'utente
                    // all'interno della lista dei ingredienti del post da modificare
                    bool eraStatoSelezionato = pizzaUpdate.Ingredientis.Any(ingredientiSelezionati => ingredientiSelezionati.Id == ingredienti.Id );

                    SelectListItem opzioneSingolaSelect = new SelectListItem() { Text = ingredienti.Name, Value = ingredienti.Id.ToString(), Selected = eraStatoSelezionato };
                    listaOpzioniPerLaSelect.Add(opzioneSingolaSelect);
                }

                modelForView.Ingredientis = listaOpzioniPerLaSelect;

                return View("Update", modelForView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id , PizzaCtegorieView formData)
        {
            if (!ModelState.IsValid)
            {

                using (PizzaContext db = new PizzaContext())
                {
                    List<Categoria> categorias = db.Categorias.ToList <Categoria>();

                    formData.Categorias = categorias;

                }
                return View("Update", formData);
            }

            using(PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToUpdate = db.Pizzas.Where(articolo => articolo.Id == id).Include(pizza => pizza.Ingredientis).FirstOrDefault();

                if (pizzaToUpdate != null)
                {
                    pizzaToUpdate.Nome = formData.Pizza.Nome;
                    pizzaToUpdate.Descrezione = formData.Pizza.Descrezione;
                    pizzaToUpdate.Prezzo = formData.Pizza.Prezzo;
                    pizzaToUpdate.Foto = formData.Pizza.Foto;
                    pizzaToUpdate.CategoriaId = formData.Pizza.CategoriaId;

                    // rimuoviamo i tag e inseriamo i nuovi
                    pizzaToUpdate.Ingredientis.Clear();

                    if (formData.IngredientisSelectedFromMultipleSelect != null)
                    {

                        foreach (string ingredientiId in formData.IngredientisSelectedFromMultipleSelect)
                        {
                            int ingredientiIdIntFromSelect = int.Parse(ingredientiId);

                            Ingredienti ingredienti = db.Ingredientis.Where(ingredientiDb => ingredientiDb.Id == ingredientiIdIntFromSelect).FirstOrDefault();

                            // todo controllare eventuali altri errori tipo l'id del ingredienti non esiste

                            pizzaToUpdate.Ingredientis.Add(ingredienti);
                        }
                    }


                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La Pizza che volevi modificare non è stata trovata");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToDelete = db.Pizzas.Where(post => post.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La pizza da eliminare non è stata trovata!");
                }
            }
        }

        [HttpDelete]
        public IActionResult ProvaDelete()
        {
            return View("Create");
        }

    }
}
