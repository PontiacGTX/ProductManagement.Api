using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
namespace Repository.AppDbContext
{
    public static class AppDbContextHelper
    {
        public static void SeedDatabase<T>(this ModelBuilder modelBuilder, IList<T> collection)
        {
            switch(collection)
            {
                case IList<TipoActividad> col:
                modelBuilder.Entity<TipoActividad>().HasData(col);
                    break;
            }
        }
    }
}
