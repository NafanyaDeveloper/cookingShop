using Canteen.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canteen.Controllers
{
    public class DishController: Controller // аналогично категори контроллер и кукшоп контроллер
    {
        private readonly IDishRepository _repo;

        public DishController(IDishRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> DishesByCookShopList(Guid id) // карточки блюд для столовой
        {
            try
            {
                return PartialView("DishesList", await _repo.GetByCookShopAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public async Task<IActionResult> DishesByCategoryList(Guid id)// карточки блюд для категории
        {
            try
            {
                return PartialView("DishesList", await _repo.GetByCategoryAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
