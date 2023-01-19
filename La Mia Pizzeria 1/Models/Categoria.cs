namespace La_Mia_Pizzeria_1.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pizza>? Pizzas { get; set; }

        public Categoria()
        { 

        }

        public Categoria(string name, List<Pizza> pizzas)
        {
            Name = name;
            Pizzas = pizzas;
        }
    }
}
