using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Repository;

namespace Repository.AppDbContext
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> opts):base(opts)
        {

        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoRegistroActividad> ProductoRegistroActividades { get; set; }
        public DbSet<RegistroActividad> RegistroActividad { get; set; }
        public DbSet<TipoActividad> TipoActividades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(x => x.ID);
            modelBuilder.Entity<ProductoRegistroActividad>().HasKey(x => x.ID);
            modelBuilder.Entity<RegistroActividad>().HasKey(x=>x.IDRegistroActividad);
            modelBuilder.Entity<RegistroActividad>().HasOne(x => x.Usuario).WithMany(x => x.RegistroActividades);
            modelBuilder.Entity<RegistroActividad>().HasOne(x => x.TipoArchividad).WithMany(x => x.RegistroActividades);
            modelBuilder.Entity<ProductoRegistroActividad>().HasOne(x => x.RegistroActividad).WithMany(x => x.RegistroActividadProductos);
            modelBuilder.Entity<ProductoRegistroActividad>().HasOne(x => x.Producto).WithMany(x => x.RegistroActividadProducto);
            
            modelBuilder.SeedDatabase(new List<TipoActividad> 
            { 
                new TipoActividad { Actividad="Crear Producto" , IdTipoActividad=1 },
                new TipoActividad { Actividad="Modificar Producto", IdTipoActividad = 2 },
                new TipoActividad { Actividad="Eliminar Producto", IdTipoActividad=3 },
                new TipoActividad { Actividad="Leer Producto", IdTipoActividad=4 },
                new TipoActividad { Actividad="Crear Usuario" , IdTipoActividad=5 },
                new TipoActividad { Actividad="Modificar Usuario", IdTipoActividad = 6 },
                new TipoActividad { Actividad="Eliminar Usuario", IdTipoActividad=7 },
                new TipoActividad { Actividad="Leer Usuario", IdTipoActividad=8 }
            });
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
