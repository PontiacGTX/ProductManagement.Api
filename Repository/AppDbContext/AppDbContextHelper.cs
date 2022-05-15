using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Microsoft.AspNetCore.Identity;

namespace Repository.AppDbContext
{
    public static class AppDbContextHelper
    {
        public static void SeedUserDb(this AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            SeedRoles(db);
            SeedUsers(db, userManager);
        }
        static void SeedRoles(this AppDbContext db)
        {
            if (!db.Roles.Any(x => x.Name == "Administrator"))
            {
                db.Roles.Add(new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" });
                db.SaveChanges();
            }
        }
         static  void SeedUsers(this AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
          
            if (!userManager.Users.Any(x => x.Email == "admin@email.com"))
            {
                var user = new ApplicationUser 
                {
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                    Habilitado = true,
                    UserName = "admin@mail.com"
                };
                var result = userManager.CreateAsync(user, "123456.ABc").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
                }
            }

        }
        public static void SeedDatabase<T>(this ModelBuilder modelBuilder, IList<T> collection)
        {
            switch(collection)
            {
                case IList<TipoActividad> col:
                modelBuilder.Entity<TipoActividad>().HasData(col);
                    break;
                case IList<ApplicationUser> users:
                modelBuilder.Entity<ApplicationUser>().HasData(users);
                    break;
            }
        }
    }
}
