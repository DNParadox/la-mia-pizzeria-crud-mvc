using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.Pizzaform
{

    //Con questa classe faccio da "Ponte" tra la tabella Pizza e Categories
    public class Pizzaform
    {
        //model del db che con le views: create, read (list, details), update
        public Pizza Pizza { get; set; }

        //views: create, update, 
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Tags { get; set; }

     
        public List<int>? SelectedTags { get; set; }
    }
}
