using Canteen.Core.EF;
using Canteen.Core.Services;
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
    public class DishRepository : IDishRepository // тут все аналогично репозиторию категории и кукшоп
    {
        private readonly CanteenDbContext _context;
        private readonly ICategoryRepository _repoCtg;
        private readonly IFileLoader _file;

        public DishRepository(CanteenDbContext context, ICategoryRepository repoCtg,
            IFileLoader file)
        {
            _context = context;
            _repoCtg = repoCtg;
            _file = file;
        }

        public async Task<List<Dish>> GetAllAsync()
        {
            return await _context.Dishes.ToListAsync();
        }

        public async Task<Dish> GetByIdAsync(Guid id)
        {
            return await _context.Dishes.FindAsync(id);
        }

        public async Task<List<Dish>> GetByCategoryAsync(Guid id)
        {
            return await _context.Dishes.Include(d => d.SizePrice).Where(x => x.CategoryId == id).ToListAsync();
        }

        public async Task<List<Dish>> GetByCookShopAsync(Guid id)
        {
            List<Category> categories = await _repoCtg.GetByCookShopAsync(id);
            List<Dish> result = new List<Dish>();
            foreach(Category c in categories)
            {
                List<Dish> dishes = await GetByCategoryAsync(c.Id);
                if (dishes.Count > 0)
                    result.AddRange(dishes);
            }
            return result;
        }
        public async Task<Dish> CreateAsync(Dish item)
        {
            item.Id = Guid.Empty;
            item.Img = await _file.LoadImg(item.Image);
            var result = await _context.Dishes.AddAsync(item);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdateAsync(Dish item)
        {
            if (item.Image != null)
                item.Img = await _file.LoadImg(item.Image);
            _context.Dishes.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Dish d = await _context.Dishes.FindAsync(id);
            if (d == null)
                return false;
            _context.Dishes.Remove(d);
            await _context.SaveChangesAsync();
            return true;
        }  
    }
}
