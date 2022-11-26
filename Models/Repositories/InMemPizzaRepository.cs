using System.Runtime.Serialization;
namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemPizzaRepository : IDbPizzaRepository
    {

        public static List<Pizza> Pizzas = new List<Pizza>();

        public InMemPizzaRepository()
        {

        }
        

        public List<Pizza> All()
        {
            
            return Pizzas; 
        }
        
        

        public void Create(Pizza pizzas, List<int> SelectedTags)
        {
            pizzas.Id = Pizzas.Count;
            pizzas.Category = new Category() { Id = 1, Title = "Categoria Inventata" };


            // Dichiarazioni 
            //pizzas.Id = 1;
            //pizzas.Title = "Pizzum";
            //pizzas.Description = "Descrizione eccellente";
            //pizzas.Price = 10;
            //pizzas.Image = "Error 404";
            //pizzas.CategoryId = 1;



            // Fine dichiarazioni

            pizzas.Tags = new List<Tag>();

            TagToPizza(pizzas, SelectedTags);


            Pizzas.Add(pizzas);

        }

        private static void TagToPizza(Pizza pizzas, List<int> SelectedTags)
        {
            pizzas.Category = new Category() { Id = 1 , Title = "Categoria Inventata"};


            foreach ( int tagId in SelectedTags)
            {
                pizzas.Tags.Add(new Tag() { Id = tagId, Title = "Tag placeholder" + tagId });
            }
        }

        public void Delete (Pizza pizzas)
        {
            Pizzas.Remove(pizzas);
        }
       
        public Pizza getById(int id)
        {
            Pizza pizzas = Pizzas.Where(post => post.Id == id).FirstOrDefault();

            pizzas.Category = new Category() { Id = 1, Title = "Categoria Inventata" };
         


            return pizzas;
        }

        public void Update(Pizza pizzas, Pizza formData, List<int>? SelectedTags)
        {
            pizzas = formData;
            pizzas.Category = new Category() { Id = 1, Title = "Categoria Inventata" };

            pizzas.Tags = new List<Tag>();

            TagToPizza(pizzas, SelectedTags);

        }
    }
}
