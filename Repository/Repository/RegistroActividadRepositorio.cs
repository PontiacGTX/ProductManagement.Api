using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Repository
{
    public class RegistroActividadRepositorio : IRepository<RegistroActividad>
    {
        public AppDbContext.AppDbContext _Db { get; }
        public RegistroActividadRepositorio(AppDbContext.AppDbContext ctx)
        {
            _Db = ctx;
        }

        async Task<long> IRepository<RegistroActividad>.Cuenta(Expression<Func<RegistroActividad, bool>> selector)
        => await _Db.RegistroActividad.Include(x => x.Usuario).LongCountAsync(selector);

       async Task<long> IRepository<RegistroActividad>.CuentaTotal()
       =>await _Db.RegistroActividad.LongCountAsync();

        async Task<bool> IRepository<RegistroActividad>.Eliminar<TID>(TID id)
        {
            RegistroActividad registroActividad = await _Db.RegistroActividad.FindAsync(id);
            if (registroActividad is null)
                return false;

            foreach(var registroActividadProducto in _Db.ProductoRegistroActividades.Include(x=>x.RegistroActividad).Where(x=>x.RegistroActividad.IDRegistroActividad== registroActividad.IDRegistroActividad))
            {
                _Db.ProductoRegistroActividades.Remove(registroActividadProducto);
            }

            _Db.RegistroActividad.Remove(registroActividad);

            return await _Db.SaveChangesAsync() > 0;

        }

        async Task<bool> IRepository<RegistroActividad>.Eliminar(RegistroActividad entidad)
        {
            if (entidad is null)
                return false;

            foreach (var registroActividadProducto in _Db.ProductoRegistroActividades.Include(x => x.RegistroActividad).Where(x => x.RegistroActividad.IDRegistroActividad == entidad.IDRegistroActividad))
            {
                try
                {
                    _Db.ProductoRegistroActividades.Remove(registroActividadProducto);
                }
                catch (Exception ex)
                {

                }
            }

            _Db.RegistroActividad.Remove(entidad);

            return await _Db.SaveChangesAsync() > 0;
        }

        async Task<RegistroActividad> IRepository<RegistroActividad>.Modificar<TID>(TID id, RegistroActividad entidad)
        {
            RegistroActividad registroActividad = await _Db.RegistroActividad.FindAsync(id);
           
            if (registroActividad == null)
                throw new NullReferenceException();

            registroActividad.FechaActividad = entidad.FechaActividad;
            registroActividad.Detalles = entidad.Detalles;
            registroActividad.Id = entidad.Id;

            return await _Db.SaveChangesAsync() > 0 ? registroActividad : null;


        }

        async Task<RegistroActividad> IRepository<RegistroActividad>.ObtenerEntidad<TID>(TID id)
        =>await _Db.RegistroActividad.FindAsync(id);

        async Task<IList<RegistroActividad>> IRepository<RegistroActividad>.ObtenerEntidades()
        => await _Db.RegistroActividad.ToListAsync();

        async Task<IList<RegistroActividad>> IRepository<RegistroActividad>.ObtenerEntidades(Expression<Func<RegistroActividad, bool>> selector)
        => await _Db.RegistroActividad.Where(selector).ToListAsync();

        async Task<RegistroActividad> IRepository<RegistroActividad>.PrimeroOValorPredeterminado(Expression<Func<RegistroActividad, bool>> selector)
        => await _Db.RegistroActividad.FirstOrDefaultAsync(selector);

        async Task<RegistroActividad> IRepository<RegistroActividad>.Agregar(RegistroActividad entidad)
        {
            var entry = await _Db.RegistroActividad.AddAsync(entidad);
            return await _Db.SaveChangesAsync() > 0 ? entry.Entity : null;
        }
    }
}
