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

    }
}
