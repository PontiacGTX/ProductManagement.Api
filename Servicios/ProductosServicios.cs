using Repository.Entities;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Fabrica;
using Factory = Servicios.Fabrica.Factory;
using Repository.Models;
using Shared.Exceptions;
using Newtonsoft.Json;
using Repository.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Models.Response;

namespace Servicios
{
    public class ProductosServicios
    {
        UnidadRepositorio _UnidadRepositorio { get; }
        UserManager<ApplicationUser> UserManager { get; }
        public ProductosServicios(UnidadRepositorio Unidad, UserManager<ApplicationUser> userManager)
        {
            _UnidadRepositorio = Unidad;
            UserManager = userManager;
        }

        public async Task<Respuesta> ObtenerTodosProductos(ObtenerTodosProductosModel modelo)
        {
            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == modelo.UsuarioPeticion.Email);
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }
            IList<Producto> productos = await _UnidadRepositorio.ProductoRepositorio.ObtenerEntidades();
            DateTime fecha = DateTime.Now;
            if (productos is { Count: 0 })
            {
                return Factory.ObtenerRespuesta<Respuesta>(new ObtenerProductoResponse { Producto = new() }, 404, $"Couldn't find any existing product");
            }
            TipoActividad tipo = await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Leer Producto");

            if (tipo is not null)
            {

                await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
                {
                    ActividadDescripcion = $"{nameof(ObtenerProductos)}",
                    Detalles = $"{modelo.UsuarioPeticion.Email} requested all products's information",
                    FechaActividad = fecha,
                    IdTipoActividad = tipo.IdTipoActividad,
                    Id = user.Id
                });

            }

            return Factory.ObtenerRespuesta<Respuesta>(new ObtenerProductosResponse { Productos = productos });
        }

        public async Task<Respuesta> ExisteIdProducto(ExisteIdProductoModel modelo)
        {
            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == modelo.UsuarioPeticion.Email);
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }
            bool existe = await _UnidadRepositorio.ProductoRepositorio.Cuenta(x => x.ID == modelo.IdProducto) >0;

            return Factory.ObtenerRespuesta<Respuesta>(new ExisteProductoResponse { Existe = existe });
        }

        public async Task<Respuesta> ExisteNombreProducto(ExisteNombreProductoModel modelo)
        {
            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == modelo.UsuarioPeticion.Email);
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }
            bool existe = await _UnidadRepositorio.ProductoRepositorio.Cuenta(x => x.Descripcion == modelo.NombreProducto) >0;

            return Factory.ObtenerRespuesta<Respuesta>(new ExisteProductoResponse { Existe = existe });
        }


       public async Task<Respuesta> ObtenerProductos(ObtenerProductosModel modelo)
        {
            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == modelo.UsuarioPeticion.Email);
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }

             IList<Producto> productos =await  _UnidadRepositorio.ProductoRepositorio.ObtenerEntidades(x=>modelo.ProductosId.Contains(x.ID));
             DateTime fecha = DateTime.Now;
            if(productos is {  Count:0})
            {
                return Factory.ObtenerRespuesta<Respuesta>(new ObtenerProductosResponse { Productos =new List<Producto>() },404,$"Couldn't find an existing product with Id {string.Join(',',modelo.ProductosId)}");
            }
             TipoActividad tipo = await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Leer Producto");
            
            if(tipo is not null)
            {

                await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
                {
                    ActividadDescripcion = $"{nameof(ObtenerProductos)}",
                    Detalles=$"{modelo.UsuarioPeticion.Email} requested Products with Id{string.Join(",",modelo.ProductosId)}",
                    FechaActividad = fecha,
                    IdTipoActividad = tipo.IdTipoActividad, 
                    Id = user.Id 
                });
                
            }

            return Factory.ObtenerRespuesta<Respuesta>(new ObtenerProductosResponse { Productos = productos });
        }

        public async Task<Respuesta> CrearProducto(CrearProductoModel modelo)
        {
            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == modelo.Usuario.Email);
            if(user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.Usuario.Email}");
            }
            var productoExistente= await _UnidadRepositorio.ProductoRepositorio.PrimeroOValorPredeterminado(x => x.Descripcion == modelo.Producto.Descripcion);
            if (productoExistente is not null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(new CrearProductoResponse { Producto = productoExistente });
            }
            Producto prod = await _UnidadRepositorio.ProductoRepositorio.Agregar(modelo.Producto);
            DateTime registationTime = DateTime.Now;
            if(prod == null)
            {
                throw new ServerException($"Unable to create a {nameof(Producto)}: {JsonConvert.SerializeObject(modelo.Producto)}", 500);
            }

            TipoActividad tipoActividad = await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Crear Producto");
            if(tipoActividad is null)
            {
                await _UnidadRepositorio.ProductoRepositorio.Eliminar(prod);
                throw new ServerException("Cannot find a valid acitivity type \"Crear\"",500);
            }
            RegistroActividad registro = await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new
                RegistroActividad 
            {
                Id = user.Id,
                FechaActividad = registationTime,
                IdTipoActividad = tipoActividad.IdTipoActividad,
                ActividadDescripcion = $"{nameof(CrearProducto)}",
            });
           await _UnidadRepositorio.RegistroActividadProducto.Agregar(new ProductoRegistroActividad 
           { 
               IDRegistroActividad = registro.IDRegistroActividad, 
               ID = prod.ID
           });

            var response = new CrearProductoResponse
            {
                IDActividad = registro.IDRegistroActividad,
                Producto = prod
            };
            response.Producto.RegistroActividadProducto = null;
            return  Factory.ObtenerRespuesta<Respuesta>(response);
        }
        public async Task<Respuesta> ActualizarProducto(ActualizarProductoModel modelo)
        {
            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == modelo.Usuario.Email);
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.Usuario.Email}");
            }
            await _UnidadRepositorio.ProductoRepositorio.Modificar(modelo.IDProducto, modelo.Producto);
            TipoActividad tipo = await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Modificar Producto");

            if (tipo is null)
            {

                await _UnidadRepositorio.ProductoRepositorio.Eliminar(modelo.IDProducto);
                return Factory.ObtenerRespuesta<Respuesta>(null, 500, "An Internal Error Happened couldn't find an Activity type to assign to the product log");
            }

            DateTime fecha = DateTime.Now;

            await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
            {
                ActividadDescripcion = $"{nameof(ActualizarProducto)}",
                FechaActividad = fecha,
                IdTipoActividad = tipo.IdTipoActividad,
                Id = user.Id
            });

            return Factory.ObtenerRespuesta<Respuesta>(new ActualizarProductoResponse
            {
                Producto = await _UnidadRepositorio.ProductoRepositorio.ObtenerEntidad(modelo.IDProducto)
            });
        }
        
        public async Task<Respuesta> EliminarProducto(EliminarProductoModel modelo)
        {
            ApplicationUser user= await UserManager.Users.FirstOrDefaultAsync(x => x.Email ==modelo.Usuario.Email);
            if(user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.Usuario.Email}");
            }
            Producto producto =await  _UnidadRepositorio.ProductoRepositorio.ObtenerEntidad(modelo.IdProducto);
            if(producto is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(new EliminarProductoResponse { Eliminado = true }, 404, "No Encontrado") ;
            }
            bool eliminado = await _UnidadRepositorio.ProductoRepositorio.Eliminar(producto);
            eliminado = (await _UnidadRepositorio.ProductoRepositorio.ObtenerEntidad(producto.ID)).Habilitado == false;
             TipoActividad tipo = await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Eliminar Producto");
            
            if(tipo is null)
            {
                producto.Habilitado = true;
                await _UnidadRepositorio.ProductoRepositorio.Modificar(producto.ID,producto);
                return Factory.ObtenerRespuesta<Respuesta>(null, 500,"An Internal Error Happened couldn't find an Activity type to assign to the product log");
            }

            DateTime fecha = DateTime.Now;

            await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
            {
                ActividadDescripcion = $"{nameof(EliminarProducto)}",
                FechaActividad = fecha,
                IdTipoActividad = tipo.IdTipoActividad, 
                Id = user.Id 
            });
            return Factory.ObtenerRespuesta<Respuesta>(new EliminarProductoResponse
            { 
                Eliminado  =eliminado
            });

        } 

        public async Task<Respuesta> ObtenerProducto(ObtenerProductoModel modelo)
        {
            ApplicationUser user= await UserManager.Users.FirstOrDefaultAsync(x => x.Email ==modelo.Usuario.Email);
            if(user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.Usuario.Email}");
            }
            Producto producto =await  _UnidadRepositorio.ProductoRepositorio.ObtenerEntidad(modelo.IdProducto);
            if(producto is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(new ObtenerProductoResponse { Producto =null },404,$"Couldn't find an existing product with Id {modelo.IdProducto}");
            }
             TipoActividad tipo = await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Leer Producto");
            
            if(tipo is null)
            {
                producto.Habilitado = true;
                await _UnidadRepositorio.ProductoRepositorio.Modificar(producto.ID,producto);
                return Factory.ObtenerRespuesta<Respuesta>(null, 500,"An Internal Error Happened couldn't find an Activity type to assign to the product log");
            }

            DateTime fecha = DateTime.Now;

            await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
            {
                ActividadDescripcion = $"{nameof(ObtenerProducto)}",
                FechaActividad = fecha,
                IdTipoActividad = tipo.IdTipoActividad, 
                Id = user.Id 
            });

            return Factory.ObtenerRespuesta<Respuesta>(new ObtenerProductoResponse
            { 
               Producto = producto
            });

        }


    }
}
