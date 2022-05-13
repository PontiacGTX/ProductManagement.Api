using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class ObtenerProductosModel
    {

        public InformacionUsuario UsuarioPeticion { get; set; }
        [CollectionValidation(MinValueCount =1)]
        public IList<int> ProductosId { get; set; } = new List<int>();
    }
}
