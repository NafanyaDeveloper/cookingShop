using Canteen.Core.Repositories;
using Canteen.Core.Services;
using Canteen.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canteen.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddRepositories( //вынесли подключение репозиториев (для удобства)
            this IServiceCollection services)
        {
            services
                .AddTransient<ICookShopRepository, CookShopRepository>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<IDishRepository, DishRepository>()
                .AddTransient<ISizePriceRepository, SizePriceRepository>();


            return services;
        }

        public static IServiceCollection AddServices( //вынесли подключение репозиториев (для удобства)
            this IServiceCollection services)
        {
            services
                .AddTransient<IFileLoader, FileLoader>();


            return services;
        }
    }
}
