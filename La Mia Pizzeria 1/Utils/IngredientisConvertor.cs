using Azure;
using La_Mia_Pizzeria_1.DataBase;
using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace La_Mia_Pizzeria_1.Utils
{
    public class IngredientisConvertor
    {
        public static List<SelectListItem> GetListIngredientisForMultipleSelect(PizzaContext db)
        {
            List<Ingredienti> ingredientisFromDb = db.Ingredientis.ToList();

            List<SelectListItem> listaPerLaSelectMultipla = new List<SelectListItem>();

            foreach (Ingredienti ingredienti in ingredientisFromDb)
            {
                SelectListItem opzioneSingolaSelectMultipla = new SelectListItem() { Text = ingredienti.Name, Value = ingredienti.Id.ToString() };
                listaPerLaSelectMultipla.Add(opzioneSingolaSelectMultipla);
            }

            return listaPerLaSelectMultipla;
        }
    }
}
