using La_Mia_Pizzeria_1.DataBase;
using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace La_Mia_Pizzeria_1.Controllers
{
    public class CategorieController : Controller
    {
        private readonly PizzaContext _db;

        public CategorieController(PizzaContext db)
        {
            _db = db;
        }

        public IActionResult Categorie()
        {
            List<Categoria> categoriaList = _db.Categorias.ToList();
            return View("Categorie", categoriaList);
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

            _db.Categorias.Add(formData);
            _db.SaveChanges();

            return RedirectToAction("Categorie");
        }


        [HttpGet]
        public IActionResult CategoriaUpdate(int id)
        {
            Categoria categoriaToUpdate = _db.Categorias.Find(id);

            if (categoriaToUpdate == null)
            {
                return NotFound("La categoria non è stata trovata");
            }

            return View("CategoriaUpdate", categoriaToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoriaUpdate(Categoria formData)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoriaUpdate", formData);
            }

            Categoria categoriaToUpdate = _db.Categorias.Find(formData.Id);

            if (categoriaToUpdate != null)
            {
                categoriaToUpdate.Name = formData.Name;
                _db.SaveChanges();

                return RedirectToAction("Categorie");
            }
            else
            {
                return NotFound("La categoria che volevi modificare non è stata trovata!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoriaDelete(int id)
        {
            Categoria categoriaToDelete = _db.Categorias.Find(id);

            if (categoriaToDelete != null)
            {
                _db.Categorias.Remove(categoriaToDelete);
                _db.SaveChanges();

                return RedirectToAction("Categorie");
            }
            else
            {
                return NotFound("La categoria da eliminare non è stata trovata!");
            }
        }
    }
}
