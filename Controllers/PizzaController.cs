using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.data;
using la_mia_pizzeria_static.Models.Pizzaform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        PizzeriaDbContext db;

        public PizzaController() : base()
        {
            // Usiamo Data quindi DB
            // Metodo mostrato da Paolo per dichiarare soltanto una volta il nostro DB anziché di volta in volta 
            db = new PizzeriaDbContext();
        }
        //Read 
        public IActionResult Index()
        {
            // Usiamo Data quindi DB . Metodo Vecchio dove bisognava dichiarare di volta in volta il db in uso
            //PizzeriaDbContext db = new PizzeriaDbContext();


            // Usiamo il Model per mostrare una lista
            List<Pizza> Pizzas = db.Pizzas.Include(pizzas => pizzas.Category).ToList();


            // In return possiamo passare un singolo argomento  
            return View(Pizzas);
        }

        public IActionResult Detail(int id)
        { 

            // Mostriamo nel dettaglio l'oggetto instanziato per ID includendo a sua volta la tabella Category
            Pizza Pizzas = db.Pizzas.Where(p => p.Id == id).Include("Category").FirstOrDefault();

            return View(Pizzas);
        }

        public IActionResult Create()
        {
            Pizzaform formData = new Pizzaform();

            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();

    

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // Equivalente di CSRF di Laravel
        // OLD public IActionResult Create(Pizza Pizza)
        public IActionResult Create(Pizzaform formData)
        {
            if (!ModelState.IsValid)
            {
                //return View(post);


                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            // Pusha nel db con .add
            // OLD db.Pizzas.Add(Pizzas);
            db.Pizzas.Add(formData.Pizza);
            //Salva i cambiamenti effettuati
            db.SaveChanges();


            //Redirect alla Index quindi alla lista di pizze creata
            return RedirectToAction("Index");
        }




        public IActionResult Update(int id)
        {
            Pizza Pizzas = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            if (Pizzas == null)
                return NotFound();


            Pizzaform formData = new Pizzaform();

            formData.Pizza = Pizzas;
            formData.Categories = db.Categories.ToList();

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
                return View();
            }

           // Pizza Pizzas = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            //if (Pizzas == null)
            //{
            //    return NotFound();
            //}


            //OLD
            //Pizzas.Title = formData.Title;
            //Pizzas.Description = formData.Description;
            //Pizzas.Image = formData.Image;
            //Pizzas.Price = formData.Price;


            db.Pizzas.Update(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza Pizzas = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            if (Pizzas == null)
            {
                return NotFound();
            }

            db.Pizzas.Remove(Pizzas);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
