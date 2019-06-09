using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Canteen.Data.Entities
{
    public class Dish
    {
        public Guid Id { get; set; }
        [Required] // обязательный параметр
        public string Title { get; set; }
        [Required]
        public string Img { get; set; }
        [NotMapped] // аналогично с классом CookShop
        public IFormFile Image { get; set; }
        [Required]
        public Guid CategoryId { get; set; } // связь один ко многим
        [Required]
        public int Calorie { get; set; }
        [Required]
        public int Protein { get; set; }
        [Required]
        public int Fat { get; set; }
        [Required]
        public int Carbohydrate { get; set; }

        public List<SizePrice> SizePrice { get; set; } = new List<SizePrice>(); // связь один ко многим
    }
}
