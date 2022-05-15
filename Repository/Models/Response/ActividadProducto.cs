using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Response
{
    public class ActividadProducto
    {
        public int IDActividadProducto { get; set; }
        public string Descripcion { get; set; }
        public string TipoActividad { get; set; }
        public string  UsuarioPeticion { get; set; }
        public DateTime Fecha { get; set; }

    }
}
