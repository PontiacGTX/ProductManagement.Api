using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class ExisteUsuarioModel
    {
        public InformacionUsuario UsuarioPeticion { get; set; }
        public InformacionUsuario Usuario { get; set; }
    }
}
