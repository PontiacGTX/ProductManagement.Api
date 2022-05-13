using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class ExisteNombreProductoModel
    {
        public InformacionUsuario UsuarioPeticion { get; set; }
        public string NombreProducto { get; set; }
    }
}
