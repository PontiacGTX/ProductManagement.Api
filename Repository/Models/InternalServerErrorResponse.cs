using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class InternalServerErrorResponse: Respuesta
    {
        public override int StatusCode { get; set; } = 500;
    }
}
