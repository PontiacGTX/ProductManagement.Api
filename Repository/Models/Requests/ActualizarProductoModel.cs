using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class ActualizarProductoModel
    {
        public InformacionUsuario Usuario { get; set; }
        public  Producto Producto { get; set; }
        public int IDProducto { get; set; }
    }
}
