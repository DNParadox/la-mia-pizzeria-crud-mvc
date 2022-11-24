namespace la_mia_pizzeria_static.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public List<Pizza>? Pizzas { get; set; }
    }
}
