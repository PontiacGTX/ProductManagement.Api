using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Response
{
    public class ObtenerTodosLogsResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<LogGenerico> RegistroActividad { get; set; } = new List<LogGenerico>();
    }
}
