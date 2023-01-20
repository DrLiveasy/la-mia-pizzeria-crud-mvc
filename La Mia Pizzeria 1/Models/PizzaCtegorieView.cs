using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace La_Mia_Pizzeria_1.Models
{
    public class PizzaCtegorieView
    {
        // Il post vuoto che il mio form dovrà compilare
        public Pizza Pizza { get; set; }

        // QUesta lista di categories servirà per la select nel from in modo che possa far visualizzare all'utente
        // tutte le categorie da cui poter selezionare un opzione per il Post
        public List<Categoria>? Categorias { get; set; }

        // Questa proprietà ci servirà per poter passare alla vista che contiene il form
        // la lista di tutti i tag da cui l'utente potrà selezionare
        public List<SelectListItem>? Ingredientis { get; set; }

        // Predisponiamo il nostro modello per la vista Create e MOdify con questa nuova proprietà per poter
        // ricevere gli id dei tag che l'utente ha selezionato. Purtroppo questi saranno di tipo string perchè quando avevamo
        // passato i Value per ogni opzione nella select questi erano di tipo string!
        public List<string>? IngredientisSelectedFromMultipleSelect { get; set; }

    }
}
