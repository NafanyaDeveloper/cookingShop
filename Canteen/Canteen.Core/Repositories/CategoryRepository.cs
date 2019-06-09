using Canteen.Core.EF;
using Canteen.Data.Entities;
using Canteen.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CanteenDbContext _context; // контекст работы с БД

        public CategoryRepository(CanteenDbContext context) // механизм внедрения зависимостей
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync() // получаем все
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id) // получаем по конкретному id, подгружая связанную сущность
        {
            return await _context.Categories
                .Include(x => x.Dishes).FirstAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetByCookShopAsync(Guid id) // получаем по столовой
        {
            return await _context.Categories.Where(x => x.CookShopId == id).ToListAsync();
        }
        public async Task<Category> CreateAsync(Category item) //создание
        {
            item.Id = Guid.Empty; // обнуляем гуид, если юзер попытается забить его своим значением 
            var result = await _context.Categories.AddAsync(item);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdateAsync(Category item) // обновление
        {
            _context.Categories.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id) //удаление
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
