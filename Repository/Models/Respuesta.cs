using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Respuesta
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public virtual int StatusCode { get; set; }
        public bool Success { get; set; }
    }
}
