using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Web.Servicios
{
    public class UnidadServicios
    {
        public ProductosServicios ProductosServicios { get; set; }
        public UsuarioServicios UsuarioServicios { get; set; }
        public LogsServicios LogsServicios { get; set; }
        public UnidadServicios(ProductosServicios productosServicios, UsuarioServicios usuarioServicios, LogsServicios logsServicios)
        {
            UsuarioServicios = usuarioServicios;
            ProductosServicios = productosServicios;
            LogsServicios= logsServicios;
        }
    }
}
