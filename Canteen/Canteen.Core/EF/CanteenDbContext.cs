using Canteen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.EF
{
    public class CanteenDbContext: DbContext // добавляем контекст работы с бд (нужно для entity framework)
    {
        public CanteenDbContext(DbContextOptions<CanteenDbContext> opt) : base(opt)
        {
            Database.EnsureCreated(); // создает автоматически БД, если ее еще нет
        }

        public DbSet<CookShop> CookShops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<SizePrice> SizePrice { get; set; }
    }
}
