using la_mia_pizzeria_static.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;


namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository : IDbPizzaRepository
    {
        private PizzeriaDbContext db;

        public DbPizzaRepository()
        {
            db = new PizzeriaDbContext();
        }

        public List<Pizza> All()
        {
            return db.Pizzas.Include(pizzas => pizzas.Category).Include(Pizzas => Pizzas.Tags).ToList();
        }

        public Pizza getById(int id)
        {
            return db.Pizzas.Where(p => p.Id == id).Include("Category").Include("Tags").FirstOrDefault();
        }




        public void Create(Pizza Pizzas, List<int> SelectedTags )
        {

            Pizzas.Tags = new List<Tag>();

            foreach (int tagId in SelectedTags)
            {
                Tag tag = db.Tags.Where(t => t.Id == tagId).FirstOrDefault();
                Pizzas.Tags.Add(tag);
            }
          

            db.Pizzas.Add(Pizzas);
            //Salva i cambiamenti effettuati
            db.SaveChanges();

        }


        public void Update(Pizza Pizzas, Pizza formData, List<int>? SelectedTags)
        {
          if(SelectedTags == null)
            {
                SelectedTags = new List<int>();
            }

            Pizzas.Title = formData.Title;
            Pizzas.Description = formData.Description;
            Pizzas.Image = formData.Image;
            Pizzas.CategoryId = formData.CategoryId;


            Pizzas.Tags.Clear();


            foreach (int tagId in SelectedTags)
            {
                Tag tag = db.Tags.Where(t => t.Id == tagId).FirstOrDefault();
                Pizzas.Tags.Add(tag);
            }
            //db.Pizzas.Update(formData.Pizza);
            db.SaveChanges();
        }


        public void Delete(Pizza Pizzas)
        {
            db.Pizzas.Remove(Pizzas);
            db.SaveChanges();
        }
    }
}
