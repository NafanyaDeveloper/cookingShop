using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Canteen.Data.Entities;

namespace Canteen.Data.Repositories
{
    public interface ICookShopRepository // интерфейс, определяющий функциональной для соответствующего репозитория
    {
        Task<List<CookShop>> GetAllAsync(); // получаем все столовые
        Task<CookShop> GetByIdAsync(Guid id); // получаем определенную по id
        Task<CookShop> CreateAsync(CookShop item); // создаем столовую
        Task<bool> UpdateAsync(CookShop item); // редактируем
        Task<bool> DeleteAsync(Guid id); // удаляем
    }
}
