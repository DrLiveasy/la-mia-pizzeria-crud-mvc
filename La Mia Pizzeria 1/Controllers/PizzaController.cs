using La_Mia_Pizzeria_1.DataBase;
using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Runtime.ConstrainedExecution;

namespace La_Mia_Pizzeria_1.Controllers
{
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
    }
}
