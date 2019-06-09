using Canteen.Data.Entities;
using Canteen.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Canteen.Controllers
{
    public class AdminController: Controller //контроллер админки (по адрессу localhost:5000/admin)
    {
        private readonly ICookShopRepository _repoCS;
        private readonly ICategoryRepository _repoCtg;
        private readonly IDishRepository _repoD;
        private readonly ISizePriceRepository _repoSP;

        public AdminController(ICookShopRepository repoCS, ICategoryRepository repoCtg,
            IDishRepository repoD, ISizePriceRepository repoSP) // механизм внедрения зависимостей
        {
            _repoCS = repoCS;
            _repoCtg = repoCtg;
            _repoD = repoD;
            _repoSP = repoSP;
        }

        public async Task<IActionResult> Index() // берем из репозитория данные о сталовых и возвращаем html код с
        {                                       // карточками столовых
            try
            {
                return View("CookShop/Index",await _repoCS.GetAllAsync());
            }
            catch(Exception ex)
            {
                return View("CookShop/Index");
            }
        }

        [HttpGet]
        public IActionResult CreateCookShop() // страница для создания столовой 
        {
            try
            {
                return View("CookShop/CreateCookShop");
            }
            catch (Exception ex)
            {
                return View("CookShop/Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCookShop(CookShop item) // метод, обрабатывающий форму создания столовой 
        {
            try
            {
                await _repoCS.CreateAsync(item);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditCookShop(Guid id) // страница редактирования столовой 
        {
            try
            {
                return View("CookShop/EditCookShop", await _repoCS.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCookShop(CookShop item) // обрабатывает форму редактирования столовой 
        {
            try
            {
                await _repoCS.UpdateAsync(item);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteCookShop(Guid id) //удаление столовой
        {
            try
            {
                await _repoCS.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> CookShopInfo(Guid id) // страница с карточками категорий для определенной столовой
        {
            try
            {
                ViewBag.CookShop = _repoCS.GetByIdAsync(id).Result.Title; // сохраним название столовой, чтобы использовать в верстке
                ViewBag.CookShopId = id; //аналогично для ид
                return View("Category/CookShopInfo", await _repoCtg.GetByCookShopAsync(id));
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult CreateCategory(Guid id) //форма создания категории для конкретной столовой
        {
            try
            {
                ViewBag.CookShopId = id;
                return View("Category/CreateCategory");
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category) // обработка формы создания категории
        {
            try
            {
                await _repoCtg.CreateAsync(category);
                return RedirectToAction("CookShopInfo", new { id = category.CookShopId });
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Guid id)// форма для редактирования категории
        {
            try
            {
                return View("Category/EditCategory", await _repoCtg.GetByIdAsync(id));
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)//обработка формы редактирования категории
        {
            try
            {
                await _repoCtg.UpdateAsync(category);
                return RedirectToAction("CookShopInfo", new { id = category.CookShopId });
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteCategory(Guid id) // удаление категории
        {
            try
            {
                Guid csId = _repoCtg.GetByIdAsync(id).Result.CookShopId;
                await _repoCtg.DeleteAsync(id);
                return RedirectToAction("CookShopInfo", new { id = csId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> CategoryInfo(Guid id)// страница с карточками блюд для определенной категории
        {
            try
            {
                Category category = await _repoCtg.GetByIdAsync(id);
                ViewBag.Category = category.Title; // передаем в верстку название категории
                ViewBag.CookShopId = category.CookShopId;// ид столовой
                ViewBag.CategoryId = id; // и ид категории
                return View("Dish/CategoryInfo", await _repoD.GetByCategoryAsync(id));
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult CreateDish(Guid id) // форма создания блюда
        {
            try
            {
                ViewBag.CategoryId = id;
                return View("Dish/CreateDish");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(Dish dish) // обработка формы создания блюда
        {
            try
            {
                await _repoD.CreateAsync(dish);
                return RedirectToAction("CategoryInfo", new { id = dish.CategoryId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditDish(Guid id)// редактирование блюда
        {
            try
            {
                return View("Dish/EditDish", await _repoD.GetByIdAsync(id));
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDish(Dish dish)// обработка формы редактирования блюда
        {
            try
            {
                await _repoD.UpdateAsync(dish);
                return RedirectToAction("CategoryInfo", new { id = dish.CategoryId });
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteDish(Guid id)// удаление блюда
        {
            try
            {
                Guid ctgId = _repoD.GetByIdAsync(id).Result.CategoryId;
                await _repoD.DeleteAsync(id);
                return RedirectToAction("CategoryInfo", new { id = ctgId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DishInfo(Guid id) // страница с карточками размер/цена для определенного блюда
        {
            try
            {
                Dish dish = await _repoD.GetByIdAsync(id);
                ViewBag.Dish = dish.Title;
                ViewBag.CategoryId = dish.CategoryId;
                ViewBag.DishId = id;
                return View("SizePrice/DishInfo", await _repoSP.GetByDishAsync(id));
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult CreateSizePrice(Guid id)//форма редактирования размер/цена
        {
            try
            {
                ViewBag.DishId = id;
                return View("SizePrice/CreateSizePrice");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSizePrice(SizePrice sp)// обработка формы редактирования размер/цена
        {
            try
            {
                await _repoSP.CreateAsync(sp);
                return RedirectToAction("DishInfo", new { id = sp.DishId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditSizePrice(Guid id)// форма редактирования размер/цена
        {
            try
            {
                return View("SizePrice/EditSizePrice", await _repoSP.GetByIdAsync(id));
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditSizePrice(SizePrice sp)// обработка формы редактирования размер/цена
        {
            try
            {
                await _repoSP.UpdateAsync(sp);
                return RedirectToAction("DishInfo", new { id = sp.DishId });
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteSizePrice(Guid id)// удаление размер/цена
        {
            try
            {
                Guid dishId = _repoSP.GetByIdAsync(id).Result.DishId;
                await _repoSP.DeleteAsync(id);
                return RedirectToAction("DishInfo", new { id = dishId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

    }
}
