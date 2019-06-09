using Canteen.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Data.Repositories
{
    public interface ISizePriceRepository// интерфейс, определяющий функциональной для соответствующего репозитория
    {
        Task<List<SizePrice>> GetAllAsync();
        Task<SizePrice> GetByIdAsync(Guid id); 
        Task<List<SizePrice>> GetByDishAsync(Guid id); // получаем список размер/цена конкретного блюда
        Task<SizePrice> CreateAsync(SizePrice item);
        Task<bool> CreateRangeAsync(List<SizePrice> item); // создает сразу несколько
        Task<bool> UpdateAsync(SizePrice item);
        Task<bool> UpdateRangeAsync(List<SizePrice> item); // изменяет сразу несколько
        Task<bool> DeleteAsync(Guid id);
    }
}
