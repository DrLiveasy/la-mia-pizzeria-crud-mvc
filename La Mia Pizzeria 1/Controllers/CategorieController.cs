using La_Mia_Pizzeria_1.DataBase;
using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace La_Mia_Pizzeria_1.Controllers
{
    public class CategorieController : Controller
    {

        public IActionResult Categorie()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Categoria> categoriaList = db.Categorias.ToList();

                return View("Categorie", categoriaList);
            }
        }

        [HttpGet]
        public IActionResult CategorieCreate()
        {
            return View("CategorieCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategorieCreate(Categoria formData)
        {
            if (!ModelState.IsValid)
            {
                return View("CategorieCreate", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Categorias.Add(formData);
                db.SaveChanges();
            }

            return RedirectToAction("Categorie");
        }


        [HttpGet]
        public IActionResult  CategoriaUpdate(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Categoria CategoriaToUpdate = db.Categorias.Where(articolo => articolo.Id == id).FirstOrDefault();

                if (CategoriaToUpdate == null)
                {
                    return NotFound("la categoria è stata trovata");
                }
                return View("CategoriaUpdate", CategoriaToUpdate);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoriaUpdate(Categoria formData)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoriaUpdate", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                Categoria postToUpdate = db.Categorias.Where(articolo => articolo.Id == formData.Id).FirstOrDefault();

                if (postToUpdate != null)
                {
                    postToUpdate.Name = formData.Name;
                    db.SaveChanges();

                    return RedirectToAction("Categorie");
                }
                else
                {
                    return NotFound("La categoria che volevi modificare non è stata trovata!");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoriaDelete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Categoria categorieToDelete = db.Categorias.Where(post => post.Id == id).FirstOrDefault();

                if (categorieToDelete != null)
                {
                    db.Categorias.Remove(categorieToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Categorie");
                }
                else
                {
                    return NotFound("la categorie da eliminare non è stata trovata!");
                }
            }
        }


    }
}
