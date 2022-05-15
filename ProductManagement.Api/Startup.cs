using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository.AppDbContext;
using Repository.Entities;
using Repository.Repository;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ManagementServiceDb"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddUserManager<UserManager<ApplicationUser>>();

            services.AddScoped<IRepository<Producto>, ProductoRepositorio>();
            services.AddScoped<IRepository<RegistroActividad>, RegistroActividadRepositorio>();
            services.AddScoped<IRepository<ProductoRegistroActividad>, RegistroActividadesProductoRepositorio>();
            services.AddScoped<IRepository<TipoActividad>, TipoActividadRepositorio>();
            services.AddScoped<IRepository<ApplicationUser>, UsuarioRepositorio>();

            services.AddScoped<UnidadRepositorio>();

            services.AddScoped<ProductosServicios>();
            services.AddScoped<UsuarioServicio>();
            services.AddScoped<RegistroActividadServicio>();

            services.AddScoped<UnidadServicios>();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManagement.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagement.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            if (ctx.Database.EnsureCreated());
            if (ctx.Users is not null)
            {
                ctx.SeedUserDb(userManager);
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
