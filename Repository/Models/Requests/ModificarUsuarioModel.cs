using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class ModificarUsuarioModel
    {
        public InformacionUsuario UsuarioPeticion { get; set; }
        public InformacionUsuario UsuarioEdicion { get; set; }
        public UsuarioDatos NuevosDatosUsuario { get; set; }
    }
}
