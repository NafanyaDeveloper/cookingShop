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
    public class SizePriceRepository : ISizePriceRepository // тут все аналогично репозиторию категории и кукшоп
    {
        private readonly CanteenDbContext _context;

        public SizePriceRepository(CanteenDbContext context)
        {
            _context = context;
        }

        public async Task<List<SizePrice>> GetAllAsync()
        {
            return await _context.SizePrice.ToListAsync();
        }

        public async Task<SizePrice> GetByIdAsync(Guid id)
        {
            return await _context.SizePrice.FindAsync(id);
        }

        public async Task<List<SizePrice>> GetByDishAsync(Guid id)
        {
            return await _context.SizePrice.Where(sp => sp.DishId == id).ToListAsync();
        }
        public async Task<SizePrice> CreateAsync(SizePrice item)
        {
            item.Id = Guid.Empty;
            var result = await _context.SizePrice.AddAsync(item);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> CreateRangeAsync(List<SizePrice> item)
        {
            await _context.SizePrice.AddRangeAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(SizePrice item)
        {
            _context.SizePrice.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRangeAsync(List<SizePrice> item)
        {
            _context.SizePrice.UpdateRange(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            SizePrice sp = await _context.SizePrice.FindAsync(id);
            if (sp == null)
                return false;
            _context.SizePrice.Remove(sp);
            await _context.SaveChangesAsync();
            return true;
        }        
    }
}
