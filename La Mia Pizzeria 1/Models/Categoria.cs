namespace La_Mia_Pizzeria_1.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public Categoria(){}
        public Categoria(string name)
        {
            Name = name;
        }
    }
}
