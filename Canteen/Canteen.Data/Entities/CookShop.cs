using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Canteen.Data.Entities
{
    public class CookShop // класс столовой
    {
        public Guid Id { get; set; }
        [Required] // обязательный параметр 
        [MaxLength(50)] // максимальная длина 50
        public string Title { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string CloseTime { get; set; }
        [Required]
        public string Img { get; set; }
        [NotMapped] // это поле не будет занесено в БД, оно используется для передачи файла изображения в контроллер
        public IFormFile Image { get; set; } // само же изображение храниться как путь на изображение в фйловой системе
                                                // в поле Img
        public List<Category> Categories { get; set; } = new List<Category>(); // связь один ко многим
    }
}
