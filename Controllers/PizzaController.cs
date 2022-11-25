using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.data;
using la_mia_pizzeria_static.Models.Pizzaform;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        PizzeriaDbContext db;

        IDbPizzaRepository PizzaRepository;
      

        public PizzaController(IDbPizzaRepository _pizzaRepository) : base()
        {
            // Usiamo Data quindi DB
            // Metodo mostrato da Paolo per dichiarare soltanto una volta il nostro DB anziché di volta in volta 
            db = new PizzeriaDbContext();

            PizzaRepository = _pizzaRepository;
        }
        //Read 
        public IActionResult Index()
        {
            
            // Usiamo il Model per mostrare una lista
            List<Pizza> Pizzas = PizzaRepository.All();

            // In return possiamo passare un singolo argomento  
            return View(Pizzas);

        }

        public IActionResult Detail(int id)
        {

            // Mostriamo nel dettaglio l'oggetto instanziato per ID includendo a sua volta la tabella Category/Tag 
            Pizza Pizzas = PizzaRepository.getById(id);

            return View(Pizzas);
        }

        public IActionResult Create()
        {
            Pizzaform formData = new Pizzaform();

            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();

            formData.Tags = new List<SelectListItem>();

            List<Tag> tagList = db.Tags.ToList();
            
            foreach (Tag tag in tagList)
            {
                formData.Tags.Add(new SelectListItem(tag.Title, tag.Id.ToString()));
            }

    

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // Equivalente di CSRF di Laravel
        // OLD public IActionResult Create(Pizza Pizza)
        public IActionResult Create(Pizzaform formData)
        {
            if (!ModelState.IsValid)
            {


                formData.Categories = db.Categories.ToList();

                formData.Tags = new List<SelectListItem>();

                List<Tag> tagList = db.Tags.ToList();

                foreach (Tag tag in tagList)
                {
                    formData.Tags.Add(new SelectListItem(tag.Title, tag.Id.ToString()));
                }


                return View(formData);
            }



            PizzaRepository.Create(formData.Pizza, formData.SelectedTags);

            //Redirect alla Index quindi alla lista di pizze creata
            return RedirectToAction("Index");
        }




        public IActionResult Update(int id)
        {
            Pizza Pizzas = PizzaRepository.getById(id);

            if (Pizzas == null)
                return NotFound();


            Pizzaform formData = new Pizzaform();

            formData.Pizza = Pizzas;
            formData.Categories = db.Categories.ToList();
            formData.Tags = new List<SelectListItem>();

            List<Tag> tagsList = db.Tags.ToList();
            
            foreach (Tag tag in tagsList)
            {
                formData.Tags.Add(new SelectListItem(
                  tag.Title,
                  tag.Id.ToString(),
                  Pizzas.Tags.Any(t => t.Id == tag.Id)
                 ));
            }

            //return View() --> non funziona perchè non ha la memoria della Pizzas
            return View(formData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizzaform formData)
        {

            formData.Pizza.Id = id;

            if (!ModelState.IsValid)
            {
                //return View(post);
                formData.Categories = db.Categories.ToList();

                formData.Tags = new List<SelectListItem>();

                List<Tag> tagList = db.Tags.ToList();

                foreach (Tag tag in tagList)
                {
                    formData.Tags.Add(new SelectListItem(tag.Title, tag.Id.ToString()));
                }

                return View(formData);
            }


            Pizza PizzaItem = PizzaRepository.getById(id);

            if (PizzaItem == null)
            {
                return NotFound();
            }



            PizzaRepository.Update(PizzaItem, formData.Pizza, formData.SelectedTags);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza Pizzas = PizzaRepository.getById(id);

            if (Pizzas == null)
            {
                return NotFound();
            }

            PizzaRepository.Delete(Pizzas);


            return RedirectToAction("Index");
        }
    }
}
