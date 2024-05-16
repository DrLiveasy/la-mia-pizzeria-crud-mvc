using Azure;
using La_Mia_Pizzeria_1.DataBase;
using La_Mia_Pizzeria_1.Models;
using La_Mia_Pizzeria_1.Utils;
using LaMiaPizzeria1.Migrations;
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
        private readonly PizzaContext _db;
        public PizzaController(PizzaContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Pizza> pizzaList = await _db.Pizzas.ToListAsync();
            return View("Index", pizzaList);
        }
        public async Task<IActionResult> Dettaglio(int id)
        {
            Pizza pizzaTrovata = await _db.Pizzas
                .Include(pizza => pizza.Categoria)
                .Include(pizza => pizza.Ingredientis)
                .FirstOrDefaultAsync(pizza => pizza.Id == id);
            if (pizzaTrovata != null)
            {
                return View(pizzaTrovata);
            }
            return NotFound("La pizza con l'id cercato non esiste!");
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categoriasFromDB = _db.Categorias.ToList();
            var modelForView = new PizzaCtegorieView
            {
                Pizza = new Pizza(),
                Categorias = categoriasFromDB,
                Ingredientis = IngredientisConvertor.GetListIngredientisForMultipleSelect(_db)
            };
            return View("Create", modelForView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaCtegorieView formData)
        {
            if (ModelState.IsValid)
            {
                if (formData.IngredientisSelectedFromMultipleSelect != null)
                {
                    formData.Pizza.Ingredientis = new List<Ingredienti>();
                    foreach (var ingredientiId in formData.IngredientisSelectedFromMultipleSelect)
                    {
                        int ingredientiIdIntFromSelcet = int.Parse(ingredientiId);
                        Ingredienti ingredienti = await _db.Ingredientis.FindAsync(ingredientiIdIntFromSelcet);
                        formData.Pizza.Ingredientis.Add(ingredienti);
                    }
                }
                _db.Pizzas.Add(formData.Pizza);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            var categories = await _db.Categorias.ToListAsync();
            formData.Categorias = categories;
            formData.Ingredientis = IngredientisConvertor.GetListIngredientisForMultipleSelect(_db);
            return View("Create", formData);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Pizza pizzaUpdate = await _db.Pizzas
                .Where(articolo => articolo.Id == id)
                .Include(pizza => pizza.Ingredientis)
                .FirstOrDefaultAsync();
            if (pizzaUpdate == null)
            {
                return NotFound("Pizza non Trovata!");
            }
            var categories = await _db.Categorias.ToListAsync();
            var modelForView = new PizzaCtegorieView
            {
                Pizza = pizzaUpdate,
                Categorias = categories,
                Ingredientis = IngredientisConvertor.GetListIngredientisForMultipleSelect(_db)
            };
            return View("Update", modelForView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PizzaCtegorieView formData)
        {
            if (ModelState.IsValid)
            {
                Pizza pizzaToUpdate = await _db.Pizzas
                    .Where(articolo => articolo.Id == id)
                    .Include(pizza => pizza.Ingredientis)
                    .FirstOrDefaultAsync();
                if (pizzaToUpdate != null)
                {
                    pizzaToUpdate.Nome = formData.Pizza.Nome;
                    pizzaToUpdate.Descrezione = formData.Pizza.Descrezione;
                    pizzaToUpdate.Prezzo = formData.Pizza.Prezzo;
                    pizzaToUpdate.Foto = formData.Pizza.Foto;
                    pizzaToUpdate.CategoriaId = formData.Pizza.CategoriaId;
                    pizzaToUpdate.Ingredientis.Clear();
                    if (formData.IngredientisSelectedFromMultipleSelect != null)
                    {
                        foreach (string ingredientiId in formData.IngredientisSelectedFromMultipleSelect)
                        {
                            int ingredientiIdIntFromSelect = int.Parse(ingredientiId);
                            Ingredienti ingredienti = await _db.Ingredientis.FindAsync(ingredientiIdIntFromSelect);
                            if (ingredienti != null)
                            {
                                pizzaToUpdate.Ingredientis.Add(ingredienti);
                            }
                            else
                            {
                                return NotFound("L'ingrediente selezionato non esiste.");
                            }
                        }
                    }
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La Pizza che volevi modificare non è stata trovata");
                }
            }
            var categories = await _db.Categorias.ToListAsync();
            formData.Categorias = categories;
            formData.Ingredientis = IngredientisConvertor.GetListIngredientisForMultipleSelect(_db);
            return View("Update", formData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Pizza pizzaToDelete = await _db.Pizzas.FindAsync(id);
            if (pizzaToDelete != null)
            {
                _db.Pizzas.Remove(pizzaToDelete);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("La pizza da eliminare non è stata trovata!");
            }
        }
    }
}
