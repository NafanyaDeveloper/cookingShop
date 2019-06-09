using Canteen.Core.EF;
using Canteen.Core.Services;
using Canteen.Data.Entities;
using Canteen.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Canteen.Core.Repositories
{
    public class CookShopRepository: ICookShopRepository
    {
        private readonly CanteenDbContext _context; // контекст работы с БД
        private readonly IFileLoader _file; // сервис сохранения изображения

        public CookShopRepository(CanteenDbContext context, IFileLoader file) // механизм внедрения зависимостей
        {
            _context = context;
            _file = file;
        }

        public async Task<List<CookShop>> GetAllAsync()
        {
            return await _context.CookShops.ToListAsync(); // получаем все
        }

        public async Task<CookShop> GetByIdAsync(Guid id)  // получаем по конкретному id, подгружая связанную сущность
        {
            return await _context.CookShops
                .Include(x => x.Categories)
                .FirstAsync(y => y.Id == id);
        }

        public async Task<CookShop> CreateAsync(CookShop item) // создание
        {
            item.Img = await _file.LoadImg(item.Image); // сохранение изображения и получение его пути
            var result = await _context.CookShops.AddAsync(item); // добавили
            await _context.SaveChangesAsync();
            return result.Entity; // вытащили то, как оно добавилось и вернули пользователю
        }

        public async Task<bool> UpdateAsync(CookShop item) // изменение
        {
            if (item.Image != null)
                item.Img = await _file.LoadImg(item.Image); // если пользователь загрузил изображение, то меняем его
            _context.CookShops.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id) // удаление
        {
            CookShop cs = await _context.CookShops.FindAsync(id);
            if (cs == null)
                return false;
            _context.CookShops.Remove(cs);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
