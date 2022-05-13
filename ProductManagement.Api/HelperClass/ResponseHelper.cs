using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Shared.Exceptions;
using Servicios.Fabrica;
using System;

namespace ProductManagement.Api.HelperClass
{
    public class HttpControllerBase:ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi =true)]
        public  IActionResult  HTTPResponse(Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
                        StatusCodes.Status400BadRequest=>BadRequest(respuesta),
                        StatusCodes.Status401Unauthorized => StatusCode(StatusCodes.Status401Unauthorized, respuesta),
                        StatusCodes.Status404NotFound => StatusCode(StatusCodes.Status404NotFound, respuesta),
                        _ => throw new ServerException(string.IsNullOrEmpty(respuesta.Message) ? "There was an unexpected error" : respuesta.Message, respuesta.StatusCode),
                    };
                }
                catch (System.Exception ex)
                {
                    string message = ex.Message;
                    if (ex is ServerException)
                    {
                        message = string.Concat(ex.Message, $" HttpStatus: {(ex as ServerException).ApiCode}");
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, Factory.ObtenerRespuesta<InternalServerErrorResponse>(null, message: message));
                }

            }
            string validationMessage =ModelState.GetErrors();
            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
    }
}
