using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Canteen.Data.Entities
{
    public class SizePrice
    {
        public Guid Id { get; set; }
        [Required] // обязательный параметр
        public int Size { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public Guid DishId { get; set; } // связь один ко многим 
    }
}
