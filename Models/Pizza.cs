

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Title { get; set; }



        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Column(TypeName = "text")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Image { get; set; }



        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(6, 30, ErrorMessage = "Il prezzo non deve essere inferiore a 6 euro")]
        public float Price { get; set; }

        //relazione 1 a n con Category ed è la vera e propria chiave esterna
        public int CategoryId { get; set; }

        //relazione 1 a n con Category l'istanza del record nelle query
        public Category? Category { get; set; }

        public List<Tag>? Tags { get; set; }
        public Pizza ()
        {

        }
        public Pizza(string title, string description, string image, float price)
        {
            this.Title = title;
            this.Description = description;
            this.Image = image;
            this.Price = price;
        }
    }
}
