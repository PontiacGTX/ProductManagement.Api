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
    public class ProductoRepositorio : IRepository<Producto>
    {
        public AppDbContext.AppDbContext _Db { get; }
        public ProductoRepositorio(AppDbContext.AppDbContext ctx)
        {
            _Db = ctx;
        }


        async Task<Producto> IRepository<Producto>.Agregar(Producto entidad)
        {
            var entry = await _Db.Productos.AddAsync(entidad);
            bool saved = await _Db.SaveChangesAsync()>0;
            return saved ? entry.Entity : null;
        }

        async Task<long> IRepository<Producto>.Cuenta(Expression<Func<Producto, bool>> selector)
        =>await _Db.Productos.LongCountAsync(selector);

        async Task<long> IRepository<Producto>.CuentaTotal()
         => await _Db.Productos.LongCountAsync();

         async Task<bool> IRepository<Producto>.Eliminar<TID>(TID id)
        {
            Producto producto = await _Db.Productos.FindAsync(id);
            if (producto is null)
                return false;

            //foreach(var registroActividades in _Db.ProductoRegistroActividades.Include(x=>x.Producto).Where(x=>x.Producto.ID==producto.ID))
            //{
            //    try
            //    {
            //        RegistroActividad registroActividad = null;
            //        if (registroActividades.RegistroActividad is not null)
            //        {
            //            registroActividad = await _Db.RegistroActividad.FindAsync(registroActividades.RegistroActividad);
            //        }

            //        _Db.ProductoRegistroActividades.Remove(registroActividades);
            //        if (registroActividad != null)
            //        {
            //            _Db.RegistroActividad.Remove(registroActividad);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            //_Db.Productos.Remove(producto);
            producto.Habilitado = false;
            bool saved = await _Db.SaveChangesAsync()>0;
            return saved;
        }

        async Task<bool> IRepository<Producto>.Eliminar(Producto entidad)
        {
            if (entidad is null)
                return false;
            Producto producto = await _Db.Productos.FindAsync(entidad.ID);
            producto.Habilitado = false;

            //foreach (var registroActividades in _Db.ProductoRegistroActividades.Include(x => x.Producto).Include(x=>x.RegistroActividad).Where(x => x.Producto.ID == entidad.ID))
            //{
            //    try
            //    {
            //        RegistroActividad registroActividad = null;
            //        if (registroActividades.RegistroActividad is not null)
            //        {
            //            registroActividad = await _Db.RegistroActividad.FindAsync(registroActividades.RegistroActividad);
            //        }

            //        _Db.ProductoRegistroActividades.Remove(registroActividades);
            //        if (registroActividad != null)
            //        {
            //            _Db.RegistroActividad.Remove(registroActividad);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

            //_Db.Productos.Remove(entidad);
            bool saved = await _Db.SaveChangesAsync() > 0;
            return saved;
        }

        async Task<Producto> IRepository<Producto>.Modificar<TID>(TID id, Producto entidad)
        {
            Producto productoModificar = await _Db.Productos.FindAsync(id);
            productoModificar.Descripcion = entidad.Descripcion;
            productoModificar.Existencia = entidad.Existencia;
            productoModificar.Precio = entidad.Precio;
            return (await _Db.SaveChangesAsync() > 0) ? productoModificar : null;
        }

        async Task<Producto> IRepository<Producto>.ObtenerEntidad<TID>(TID id)
        {
            return await _Db.Productos.FindAsync(id);
        }

        async Task<IList<Producto>> IRepository<Producto>.ObtenerEntidades()
       => await _Db.Productos.ToListAsync();

       async Task<IList<Producto>> IRepository<Producto>.ObtenerEntidades(Expression<Func<Producto, bool>> selector)
        => await _Db.Productos
            .Include(x=>x.RegistroActividadProducto)
            .Include(x=>x.RegistroActividadProducto.Select(x=>x.RegistroActividad))
            .Where(selector)
            .ToListAsync();

        Task<Producto> IRepository<Producto>.PrimeroOValorPredeterminado(Expression<Func<Producto, bool>> selector)
        => _Db.Productos.FirstOrDefaultAsync(selector);
    }
}
