using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class UsuarioDatos
    {
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public bool Habilitado { get; set; }
    }
}
