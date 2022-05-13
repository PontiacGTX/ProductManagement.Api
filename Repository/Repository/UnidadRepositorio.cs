using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
namespace Repository.Repository
{
    public class UnidadRepositorio
    {
        public IRepository<ProductoRegistroActividad> RegistroActividadProducto { get; }
        public IRepository<Producto> ProductoRepositorio { get; }
        public IRepository<RegistroActividad> RegistroActividadRepositorio { get; }
        public IRepository<TipoActividad> TipoActividadRepositorio { get; }
        public IRepository<ApplicationUser> UsuarioRepositorio { get; }
        public UnidadRepositorio(IRepository<Producto> productoRepositorio,
            IRepository<RegistroActividad> registroActividad,
            IRepository<ProductoRegistroActividad> registroActividadProducto, 
            IRepository<TipoActividad> tipoActividadRepositorio,
            IRepository<ApplicationUser> usuarioRepositorio)
        {
            RegistroActividadProducto = registroActividadProducto;
            RegistroActividadRepositorio = registroActividad;
            ProductoRepositorio =productoRepositorio;
            TipoActividadRepositorio = tipoActividadRepositorio;
            UsuarioRepositorio = usuarioRepositorio;
        }

    }
}
