using Microsoft.AspNetCore.Mvc;

namespace La_Mia_Pizzeria_1.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
