using Canteen.Data.Entities;
using Canteen.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canteen.Controllers
{
    public class CookShopController: Controller// контроллер, который получает данные из репозитория 
    {                                           // и передает их в частичное представление(т.е. методы возвращают кусок )
                                                // html кода, который мы вставляем с помощью js в соотв. блоки
        private readonly ICookShopRepository _repo;
        

        public CookShopController(ICookShopRepository repo) // внедрение зависимостей
        { 
            _repo = repo;
        }

        public async Task<IActionResult> CookShopList() // список столовых для верхнего меню
        {
            try
            {
                return PartialView("CookShopList", await _repo.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex); // при ошибке - статус-код 500 и ошибку
            }
        }

        public async Task<IActionResult> CookShopCards() // карточки со столовыми
        {
            try
            {
                return PartialView("CookShopCards", await _repo.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public async Task<IActionResult> CookShopInfo(Guid id)//информация о времени работы и работает или нет столовая
        {
            try
            {
                CookShop cs = await _repo.GetByIdAsync(id);
                return Ok(new
                {
                    title = cs.Title,
                    stime = cs.StartTime,
                    etime = cs.CloseTime,
                    isOpen = DateTime.Now > DateTime.Parse(cs.StartTime) && DateTime.Now < DateTime.Parse(cs.CloseTime)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
