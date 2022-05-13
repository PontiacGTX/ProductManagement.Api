using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Fabrica
{
    public static class Factory
    {
        public static Respuesta ObtenerRespuesta<T>(object data, int statusCode = 200, String message = "Success")
            where T:Respuesta
        {
            Type typeResponse = typeof(T);
            if (typeResponse == typeof(Respuesta))
                return new Respuesta { StatusCode = statusCode, Message = message, Data = data, Success = true };
            else if (typeResponse == typeof(InternalServerErrorResponse))
                return new InternalServerErrorResponse { StatusCode = statusCode is not 200 ? statusCode : 500, Message = message, Data = data, Success = false };

            return null!;
        }
    }
}
