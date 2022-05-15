using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.HelperClass;
using Repository.Models;
using Repository.Models.Requests;
using Servicios;
using Servicios.Fabrica;
using Shared.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {
        UnidadServicios UnidadServicios { get; set; }
        public ManagementController(UnidadServicios unidadServicios)
        {
            UnidadServicios = unidadServicios;
        }
        [HttpPost("Logs/Logs/Productos")]
        public async Task<IActionResult> ObtenerLogsProductos(ObtenerLogsProductosModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.RegistroActividadServicio.ObtenerLogsProductos(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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

            string validationMessage = ModelState.GetErrors();
            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
        [HttpPost("Logs/Logs/Todos")]
        public async Task<IActionResult> ObtenerTodosLogs(ObtenerTodosLogsModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.RegistroActividadServicio.ObtenerTodosLogs(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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

            string validationMessage = ModelState.GetErrors();
            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }

        [HttpPost("Usuarios/Usuario/Obtener")]
        public async Task<IActionResult> ObtenerUsuario(ObtenerUsuarioModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.UsuarioServicios.ObtenerUsuario(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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

            string validationMessage = ModelState.GetErrors();
            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
        [HttpPost("Usuarios/Usuario/ExisteEmail")]
        public async Task<IActionResult> ObtenerUsuario(ExisteUsuarioModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.UsuarioServicios.ExisteUsuario(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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

            string validationMessage = ModelState.GetErrors();
            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }

        [HttpPost("Usuarios/Crear")]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.UsuarioServicios.CrearUsuario(modelo);
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

            string validationMessage = ModelState.GetErrors();
            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
        [HttpPost("Productos/Producto")]
        public async Task<IActionResult> ObtenerProducto(ObtenerProductoModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.ObtenerProducto(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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
            string validationMessage = ModelState.GetErrors();

            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }

        [HttpPost("Productos/Producto/ExisteNombre")]
        public async Task<IActionResult> ExisteProducto(ExisteNombreProductoModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.ExisteNombreProducto(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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
            string validationMessage = ModelState.GetErrors();

            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        } 

        [HttpPost("Productos/Producto/ExisteId")]
        public async Task<IActionResult> ExisteProducto(ExisteIdProductoModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.ExisteIdProducto(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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
            string validationMessage = ModelState.GetErrors();

            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
        [HttpPost("Productos/Agregar")]
        public async Task<IActionResult> AgregarProducto(CrearProductoModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.CrearProducto(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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
            string validationMessage = ModelState.GetErrors();

            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
        [HttpPost("Productos/Eliminar")]
        public async Task<IActionResult> EliminarProducto(EliminarProductoModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.EliminarProducto(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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
            string validationMessage = ModelState.GetErrors();

            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }

        [HttpPost("Productos/Actualizar")]
        public async Task<IActionResult> ActualizarProducto(ActualizarProductoModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.ActualizarProducto(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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
            string validationMessage = ModelState.GetErrors();

            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }

        [HttpPost("Productos/Lista")]
        public async Task<IActionResult> ObtenerProductos(ObtenerProductosModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.ObtenerProductos(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
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
            string validationMessage = ModelState.GetErrors();

            return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
        [HttpPost("Productos/Todos")]
        public async Task<IActionResult> ObtenerProductos(ObtenerTodosProductosModel modelo)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Respuesta respuesta = await UnidadServicios.ProductosServicios.ObtenerTodosProductos(modelo);
                    return respuesta.StatusCode switch
                    {
                        StatusCodes.Status200OK => Ok(respuesta),
                        StatusCodes.Status401Unauthorized => StatusCode(StatusCodes.Status401Unauthorized, respuesta),
                        StatusCodes.Status404NotFound => StatusCode(StatusCodes.Status404NotFound, respuesta),
                        _ => throw new ServerException(string.IsNullOrEmpty(respuesta.Message) ? "There was an unexpected error" : respuesta.Message, respuesta.StatusCode),
                    };
                }
                catch (System.Exception ex)
                {
                    string message = ex.Message;
                    if(ex is ServerException)
                    {
                        message = string.Concat(ex.Message,$" HttpStatus: {(ex as ServerException).ApiCode}");
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, Factory.ObtenerRespuesta<InternalServerErrorResponse>(null, message: message));
                }
                   
            }
                string validationMessage = ModelState.GetErrors();

                return StatusCode(StatusCodes.Status400BadRequest, Factory.ObtenerRespuesta<Respuesta>(null, StatusCodes.Status400BadRequest, validationMessage));
        }
    }
}
