using La_Mia_Pizzeria_1.Models;
using Microsoft.EntityFrameworkCore;

namespace La_Mia_Pizzeria_1.DataBase
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;"
                                        + "Database=PizzeriaMVC;"
                                        + "Integrated Security=True;"
                                        + "TrustServerCertificate=True");
        }
    }
}
