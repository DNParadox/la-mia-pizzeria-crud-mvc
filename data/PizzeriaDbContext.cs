using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static.Models;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.data
{
    public class PizzeriaDbContext : DbContext
    {
        //Richiamo Tabella Pizzas
        public DbSet<Pizza> Pizzas { get; set; }

        // Richiamo Tabella Category
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True;Encrypt=false;");

        }
    }
}
