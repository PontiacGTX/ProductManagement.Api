using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class EliminarUsuarioModel
    {
        public InformacionUsuario RequestingUser { get; set; }
        public InformacionUsuario UserToDelete { get; set; }
    }
}
