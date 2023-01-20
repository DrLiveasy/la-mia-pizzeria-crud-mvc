using Azure;
using La_Mia_Pizzeria_1.DataBase;
using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace La_Mia_Pizzeria_1.Utils
{
    public class IngredientisConvertor
    {
        public static List<SelectListItem> getListIngredientisForMultipleSelect()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Ingredienti> IngredientisFromDb = db.Ingredientis.ToList<Ingredienti>();

                // Creare una lista di SelectListItem e tradurci al suo interno tutti i nostri Tag che provengono da Db
                List<SelectListItem> listaPerLaSelectMultipla = new List<SelectListItem>();

                foreach (Ingredienti ingredienti in IngredientisFromDb)
                {
                    SelectListItem opzioneSingolaSelectMultipla = new SelectListItem() { Text = ingredienti.Name , Value = ingredienti.Id.ToString() };
                    listaPerLaSelectMultipla.Add(opzioneSingolaSelectMultipla);
                }

                return listaPerLaSelectMultipla;
            }
        }
    }
}

