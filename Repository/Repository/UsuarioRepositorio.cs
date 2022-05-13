using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UsuarioRepositorio : IRepository<ApplicationUser>
    {
        public AppDbContext.AppDbContext _Db { get; }
        public UserManager<ApplicationUser> _UserManager { get; }

        public UsuarioRepositorio(AppDbContext.AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
        {
            _Db = appDbContext;
            _UserManager = userManager;
        }

        async Task<long> IRepository<ApplicationUser>.CuentaTotal()
        => await _Db.Users.LongCountAsync();

        async Task<long> IRepository<ApplicationUser>.Cuenta(Expression<Func<ApplicationUser, bool>> selector)
         => await _Db.Users.LongCountAsync(selector);


        async Task<ApplicationUser> IRepository<ApplicationUser>.ObtenerEntidad<TID>(TID id)
        => await _Db.Users.FindAsync(id);

        async Task<IList<ApplicationUser>> IRepository<ApplicationUser>.ObtenerEntidades()
        => await _Db.Users.ToListAsync();

        async Task<IList<ApplicationUser>> IRepository<ApplicationUser>.ObtenerEntidades(Expression<Func<ApplicationUser, bool>> selector)
        => await _Db.Users.Where(selector).ToListAsync();

        async Task<ApplicationUser> IRepository<ApplicationUser>.PrimeroOValorPredeterminado(Expression<Func<ApplicationUser, bool>> selector)
        => await _Db.Users.FirstOrDefaultAsync(selector);

        async Task<ApplicationUser> IRepository<ApplicationUser>.Agregar(ApplicationUser entidad)
        {
            var entry = await _Db.Users.AddAsync(entidad);
            return await _Db.SaveChangesAsync() > 0 ? entry.Entity : null;
        }

        async Task<ApplicationUser> IRepository<ApplicationUser>.Modificar<TID>(TID id, ApplicationUser entidad)
        {
            ApplicationUser user = await _Db.Users.FindAsync(id);
            if (user is null)
            {
                return null;
            }
            user.PhoneNumber = entidad.PhoneNumber;
            user.UserName = entidad.UserName;
            user.Email = entidad.Email;
            user.PasswordHash = _UserManager.PasswordHasher.HashPassword(user, entidad.PasswordHash);
            user.Habilitado = entidad.Habilitado;
            var saved =await _Db.SaveChangesAsync()>0;
            return saved? user : null;
        }

        async Task<bool> IRepository<ApplicationUser>.Eliminar<TID>(TID id)
        {
            ApplicationUser user = await _Db.Users.FindAsync(id);
            if (user is null)
            {
                return true;
            }
            user.Habilitado = false;
            var saved = await _Db.SaveChangesAsync() > 0;
            return saved;
        }

        async Task<bool> IRepository<ApplicationUser>.Eliminar(ApplicationUser entidad)
        {
            ApplicationUser user = entidad;
            if (user is null)
            {
                return true;
            }
            user.Habilitado = false;
            var saved = await _Db.SaveChangesAsync() > 0;
            return saved;
        }
    }
}
