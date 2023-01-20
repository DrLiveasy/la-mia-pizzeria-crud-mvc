using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace La_Mia_Pizzeria_1.DataBase
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Ingredienti> Ingredientis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;"
                                        + "Database=PizzeriaMVC;"
                                        + "Integrated Security=True;"
                                        + "TrustServerCertificate=True");
        }
    }
}
