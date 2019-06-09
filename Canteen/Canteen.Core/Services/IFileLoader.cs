using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Core.Services
{
    public interface IFileLoader // интерфейс для класса, который берет файл изображение и сохраняет его 
    {
        Task<string> LoadImg(IFormFile file);
    }
}
