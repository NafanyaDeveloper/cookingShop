using Canteen.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Data.Repositories
{
    public interface ICategoryRepository// интерфейс, определяющий функциональной для соответствующего репозитория
    {
        Task<List<Category>> GetAllAsync(); // получаем все категории
        Task<Category> GetByIdAsync(Guid id); // получаем конкретную по ид
        Task<List<Category>> GetByCookShopAsync(Guid id); // получаем список категорий в столовой 
        Task<Category> CreateAsync(Category item); // создание
        Task<bool> UpdateAsync(Category item); // изменение
        Task<bool> DeleteAsync(Guid id); // удаление
    }
}
