using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Response
{
    public class CrearProductoResponse
    {
        public int? IDActividad { get; set; }
        public Producto? Producto { get; set; }
    }
}
