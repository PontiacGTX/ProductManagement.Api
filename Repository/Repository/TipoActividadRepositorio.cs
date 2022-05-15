using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TipoActividadRepositorio:IRepository<TipoActividad>
    {
        public AppDbContext.AppDbContext _Db { get; }

        public TipoActividadRepositorio(AppDbContext.AppDbContext ctx)
        {
            _Db = ctx; 
        }

        async Task<long> IRepository<TipoActividad>.CuentaTotal()
        => await _Db.TipoActividades.LongCountAsync();

        async Task<long> IRepository<TipoActividad>.Cuenta(Expression<Func<TipoActividad, bool>> selector)
        => await _Db.TipoActividades.LongCountAsync(selector);

        async Task<TipoActividad> IRepository<TipoActividad>.ObtenerEntidad<TID>(TID id)
        => await _Db.TipoActividades.FindAsync(id);

        async Task<IList<TipoActividad>> IRepository<TipoActividad>.ObtenerEntidades()
        => await _Db.TipoActividades.ToListAsync();

        async Task<IList<TipoActividad>> IRepository<TipoActividad>.ObtenerEntidades(Expression<Func<TipoActividad, bool>> selector)
        => await _Db.TipoActividades.Where(selector).ToListAsync();

        async Task<TipoActividad> IRepository<TipoActividad>.PrimeroOValorPredeterminado(Expression<Func<TipoActividad, bool>> selector)
        => await _Db.TipoActividades.FirstOrDefaultAsync(selector);

        async Task<TipoActividad> IRepository<TipoActividad>.Agregar(TipoActividad entidad)
        {
            var entry = await _Db.TipoActividades.AddAsync(entidad);
            return await _Db.SaveChangesAsync() > 0 ? entry.Entity : null;
        }

        async Task<TipoActividad> IRepository<TipoActividad>.Modificar<TID>(TID id, TipoActividad entidad)
        {
            var tipoActividad =await   _Db.TipoActividades.FindAsync(id);
            tipoActividad.Actividad = entidad.Actividad;
            return await _Db.SaveChangesAsync() > 0 ? tipoActividad : null;
        }

        async Task<bool> IRepository<TipoActividad>.Eliminar<TID>(TID id)
        {
            TipoActividad tipoActividad =await   _Db.TipoActividades.FindAsync(id);
            if (tipoActividad is null)
                return true;
            foreach(var registro in _Db.RegistroActividad.Include(x=>x.TipoArchividad).Where(x=>x.IdTipoActividad==tipoActividad.IdTipoActividad))
            {
                try
                {
                    _Db.RegistroActividad.Remove(registro);
                    foreach (var registroActividadProducto in _Db.ProductoRegistroActividades.Include(x => x.RegistroActividad).Where(x => x.IDRegistroActividad == registro.IDRegistroActividad))
                    {
                        try
                        {
                            _Db.ProductoRegistroActividades.Remove(registroActividadProducto);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                
            }
            _Db.TipoActividades.Remove(tipoActividad);

            return await _Db.SaveChangesAsync() > 0;
        }

        async Task<bool> IRepository<TipoActividad>.Eliminar(TipoActividad entidad)
        {
            TipoActividad tipoActividad = entidad;
            if (tipoActividad is null)
                return true;
            foreach (var registro in _Db.RegistroActividad.Include(x => x.TipoArchividad).Where(x => x.IdTipoActividad == tipoActividad.IdTipoActividad))
            {
                try
                {
                    _Db.RegistroActividad.Remove(registro);
                    foreach (var registroActividadProducto in _Db.ProductoRegistroActividades.Include(x => x.RegistroActividad).Where(x => x.IDRegistroActividad == registro.IDRegistroActividad))
                    {
                        try
                        {
                            _Db.ProductoRegistroActividades.Remove(registroActividadProducto);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }

            }
            _Db.TipoActividades.Remove(tipoActividad);

            return await _Db.SaveChangesAsync() > 0;
        }

        public Task<IList<TipoActividad>> ObtenerEntidades<TProperty, TSecondProperty>(Expression<Func<TipoActividad, bool>> selector, Expression<Func<TipoActividad, TProperty>> includeClause, Expression<Func<TipoActividad, TSecondProperty>> secondIncludeClause)
        {
            throw new NotImplementedException();
        }
    }
}
