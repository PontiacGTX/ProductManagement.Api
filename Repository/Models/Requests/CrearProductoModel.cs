using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class CrearProductoModel
    {
        public Producto Producto { get; set; }
        public InformacionUsuario Usuario { get; set; }
    }
}
