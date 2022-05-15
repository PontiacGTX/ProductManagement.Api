using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class UnidadServicios
    {
        public ProductosServicios  ProductosServicios { get; set; }
        public UsuarioServicio UsuarioServicios { get; set; }
        public RegistroActividadServicio RegistroActividadServicio { get; set; }
        public UnidadServicios(ProductosServicios productosServicios, UsuarioServicio usuarioServicio, RegistroActividadServicio registroActividadServicio)
        {
            ProductosServicios = productosServicios;
            UsuarioServicios = usuarioServicio;
            RegistroActividadServicio = registroActividadServicio;
        }
    }
}
