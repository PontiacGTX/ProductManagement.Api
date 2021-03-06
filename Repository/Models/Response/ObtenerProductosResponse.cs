using Newtonsoft.Json;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Response
{
     public class ObtenerProductosResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<Producto> Productos { get; set; } = new List<Producto>();
    }
}
