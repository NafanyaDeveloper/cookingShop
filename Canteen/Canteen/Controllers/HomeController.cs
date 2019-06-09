using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Canteen.Data.Repositories;
using Canteen.Data.Entities;

namespace Canteen.Controllers
{
    public class HomeController : Controller // контроллер, который надо бы удалить, его в начале проекта сделал для тестов
    {
        private readonly ICookShopRepository _repo;

        public HomeController(ICookShopRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _repo.GetAllAsync());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public async Task<IActionResult> About()
        {
            try
            {
                return Ok(await _repo.CreateAsync(new CookShop { Title = "Hello", StartTime = "10", CloseTime = "20" }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
