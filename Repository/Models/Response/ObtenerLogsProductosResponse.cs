using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Response
{
    public class ObtenerLogsProductosResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<ActividadProducto> RegistroActividad { get; set; } = new List<ActividadProducto>();
    }
}
