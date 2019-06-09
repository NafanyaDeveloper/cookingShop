using Canteen.Data.Entities;
using Canteen.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canteen.Controllers
{
    public class CategoryController: Controller // контроллер, который получает данные из репозитория 
    {                                           // и передает их в частичное представление(т.е. методы возвращают кусок )
                                                // html кода, который мы вставляем с помощью js в соотв. блоки
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo) // механизм внедрения зависимостей
        {
            _repo = repo;
        }

        public async Task<IActionResult> CategoriesList(Guid id) // вернет список категорий для определенной столовой 
        {                                                           // для бокового меню
            try
            {
                return PartialView("CategoryList", await _repo.GetByCookShopAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex); // в случае ошибки код 500 и текст ошибки
            }
        }
    }
}
