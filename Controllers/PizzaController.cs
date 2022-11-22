using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Extensions.Hosting;


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


            // Usiamo Model
            List<Pizza> Pizzas = db.Pizzas.ToList();


            // In return possiamo passare un singolo argomento
            return View(Pizzas);
        }

        public IActionResult Detail(int id)
        {


            Pizza Pizzas = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            return View(Pizzas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // Equivalente di CSRF di Laravel
        public IActionResult Create(Pizza Pizzas)
        {
            if (!ModelState.IsValid)
            {
                //return View(post);
                return View();
            }

            // Pusha nel db con .add
            db.Pizzas.Add(Pizzas);
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

            //return View() --> non funziona perchè non ha la memoria della Pizzas
            return View(Pizzas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza formData)
        {

            if (!ModelState.IsValid)
            {
                //return View(post);
                return View();
            }

            Pizza Pizzas = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            if (Pizzas == null)
            {
                return NotFound();
            }

            Pizzas.Title = formData.Title;
            Pizzas.Description = formData.Description;
            Pizzas.Image = formData.Image;
            Pizzas.Price = formData.Price;

            //db.Posts.Update(post);
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
