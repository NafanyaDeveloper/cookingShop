using Canteen.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Data.Repositories
{
    public interface IDishRepository// интерфейс, определяющий функциональной для соответствующего репозитория
    {
        Task<List<Dish>> GetAllAsync();
        Task<Dish> GetByIdAsync(Guid id);
        Task<List<Dish>> GetByCategoryAsync(Guid id); // получаем список блюд из какой-то категории
        Task<List<Dish>> GetByCookShopAsync(Guid id); // получаем список блюд по столовой 
        Task<Dish> CreateAsync(Dish item);
        Task<bool> UpdateAsync(Dish item);
        Task<bool> DeleteAsync(Guid id);
    }
}
