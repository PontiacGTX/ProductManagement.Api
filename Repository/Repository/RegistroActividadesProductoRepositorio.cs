using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Entities;
namespace Repository.Repository
{
    public class RegistroActividadesProductoRepositorio:IRepository<ProductoRegistroActividad>
    {
        public AppDbContext.AppDbContext _Db { get; }
        public RegistroActividadesProductoRepositorio(AppDbContext.AppDbContext ctx)
        {
            _Db = ctx;
        }

       async Task<long> IRepository<ProductoRegistroActividad>.CuentaTotal()
        => await _Db.ProductoRegistroActividades.LongCountAsync();

       async Task<long> IRepository<ProductoRegistroActividad>.Cuenta(Expression<Func<ProductoRegistroActividad, bool>> selector)
       => await _Db.ProductoRegistroActividades.LongCountAsync(selector);

        async Task<ProductoRegistroActividad> IRepository<ProductoRegistroActividad>.ObtenerEntidad<TID>(TID id)
        => await _Db.ProductoRegistroActividades.FindAsync(id);

        async Task<IList<ProductoRegistroActividad>> IRepository<ProductoRegistroActividad>.ObtenerEntidades()
        => await _Db.ProductoRegistroActividades.ToListAsync();

        async Task<IList<ProductoRegistroActividad>> IRepository<ProductoRegistroActividad>.ObtenerEntidades(Expression<Func<ProductoRegistroActividad, bool>> selector)
        => await _Db.ProductoRegistroActividades
                    .Include(x=>x.Producto)
                    .Include(x=>x.RegistroActividad)
                    .Where(selector)
                    .ToListAsync();

        async Task<ProductoRegistroActividad> IRepository<ProductoRegistroActividad>.PrimeroOValorPredeterminado(Expression<Func<ProductoRegistroActividad, bool>> selector)
         => await _Db.ProductoRegistroActividades.Include(x => x.Producto).Include(x => x.RegistroActividad).FirstOrDefaultAsync(selector);

        async Task<ProductoRegistroActividad> IRepository<ProductoRegistroActividad>.Agregar(ProductoRegistroActividad entidad)
        {
            EntityEntry<ProductoRegistroActividad> entry = null;
            bool saved = false;
            try
            {
                 entry = await _Db.ProductoRegistroActividades.AddAsync(entidad);
                 saved = await _Db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            return saved? entry.Entity : null;
        }

        async Task<ProductoRegistroActividad> IRepository<ProductoRegistroActividad>.Modificar<TID>(TID id, ProductoRegistroActividad entidad)
        {
            ProductoRegistroActividad entity = null;
            bool saved = false;
            try
            {
                entity = await _Db.ProductoRegistroActividades.FindAsync(id);
                entity.IDRegistroActividad = entidad.IDRegistroActividad;
                entity.ID = entity.ID;
                saved = await _Db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            return saved ? entity : null;
        }

        async Task<bool> IRepository<ProductoRegistroActividad>.Eliminar<TID>(TID id)
        {
            ProductoRegistroActividad productoRegistroActividad = await _Db.ProductoRegistroActividades.FindAsync(id);

            if (productoRegistroActividad == null)
                return false;

            RegistroActividad registroActividad = null;
            registroActividad = _Db.ProductoRegistroActividades.Include(x=>x.RegistroActividad).FirstOrDefault(x=>x.IDProductoRegistroActividad==productoRegistroActividad.IDProductoRegistroActividad)?.RegistroActividad;


            _Db.ProductoRegistroActividades.Remove(productoRegistroActividad);


            if (registroActividad is not null)
            {
                registroActividad = await _Db.RegistroActividad.FindAsync(registroActividad.IDRegistroActividad);
                if(registroActividad is not null)
                {
                    _Db.RegistroActividad.Remove(registroActividad);
                }
            }

            return await _Db.SaveChangesAsync() > 0;
        }

        async Task<bool> IRepository<ProductoRegistroActividad>.Eliminar(ProductoRegistroActividad entidad)
        {
            if (entidad == null)
                return false;

            RegistroActividad registroActividad = null;
            registroActividad = _Db.ProductoRegistroActividades.Include(x => x.RegistroActividad).FirstOrDefault(x => x.IDProductoRegistroActividad == entidad.IDProductoRegistroActividad)?.RegistroActividad;


            _Db.ProductoRegistroActividades.Remove(entidad);


            if (registroActividad is not null)
            {
                registroActividad = await _Db.RegistroActividad.FindAsync(registroActividad.IDRegistroActividad);
                if (registroActividad is not null)
                {
                    _Db.RegistroActividad.Remove(registroActividad);
                }
            }

            return await _Db.SaveChangesAsync() > 0;
        }

        public async Task<IList<ProductoRegistroActividad>> ObtenerEntidades<TProperty, TSecondProperty>(Expression<Func<ProductoRegistroActividad, bool>> selector, Expression<Func<ProductoRegistroActividad, TProperty>> includeClause, Expression<Func<ProductoRegistroActividad, TSecondProperty>> secondIncludeClause)
        => await _Db.ProductoRegistroActividades.Include(includeClause).Include(secondIncludeClause).Where(selector).ToListAsync();
    }
}
