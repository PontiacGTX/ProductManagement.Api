using Repository.Entities;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;
using Repository.Models.Requests;
using Servicios.Fabrica;
using Repository.Models.Response;

namespace Servicios
{
    public class UsuarioServicio
    {
        
        UnidadRepositorio _UnidadRepositorio { get; set; }
        public UsuarioServicio(UnidadRepositorio unidadRepositorio)
        {
            _UnidadRepositorio = unidadRepositorio;
        }

        public async Task<Respuesta> CrearUsuario(CrearUsuarioModel modelo)
        {
            ApplicationUser user =await  _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.Email);
            if(user is not null)
            {
                ApplicationUser applicationUserCpy = user.DeepCopy();
                applicationUserCpy.PasswordHash = "";
                return Factory.ObtenerRespuesta<Respuesta>(new CrearUsuarioResponse { User =applicationUserCpy  });

            }
            var usuarioExistente = await _UnidadRepositorio.ProductoRepositorio.PrimeroOValorPredeterminado(x => x.Descripcion == modelo.Email);
            if(usuarioExistente is not null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(new CrearUsuarioResponse { User = null }, 400, $"There is an user already using this email {modelo.Email}");
            }
            TipoActividad tipoActividad =await  _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Crear Usuario");
            DateTime fecha = DateTime.Now;

            RegistroActividad registroActividad = await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
            {
                ActividadDescripcion = $"{nameof(CrearUsuario)}",
                Detalles = $"Usuario Creado en {fecha}",
                FechaActividad = fecha,
                Id = user.Id,
                IdTipoActividad = tipoActividad.IdTipoActividad,

            });
            ApplicationUser userCpy = user.DeepCopy();
            userCpy.PasswordHash = "";

            return Factory.ObtenerRespuesta<Respuesta>(new CrearUsuarioResponse { User =userCpy });
        }

        public async Task<Respuesta> ModificarUsuario(ModificarUsuarioModel modelo)
        {
            ApplicationUser usuarioPeticion=await   _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.UsuarioPeticion.Email);
            if (usuarioPeticion is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }
            ApplicationUser usuarioEdicion = await _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.UsuarioEdicion.Email);
            if (usuarioEdicion is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 404, $"couldn't find an user with email {modelo.UsuarioEdicion.Email} to be updated");
            }

            TipoActividad tipoActividad =await  _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Modificar Usuario");
            if (tipoActividad is null)
            {
                
            }

            ApplicationUser usuarioEditado =  await _UnidadRepositorio.UsuarioRepositorio.Modificar(usuarioEdicion.Id, usuarioEdicion);
            DateTime fecha = DateTime.Now;
            if(usuarioEditado is null)
            {
                return Factory.ObtenerRespuesta<InternalServerErrorResponse>(new ModificarUsuarioResponse { Usuario = usuarioEditado }, 500, "There was an internal server error");
            }
            ApplicationUser applicationUserCpy = usuarioEditado.DeepCopy();
            applicationUserCpy.PasswordHash = "";
            RegistroActividad registroActividad = null;
            try
            {
                registroActividad = await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
                {
                    ActividadDescripcion = $"User {modelo.UsuarioPeticion.Email} successfully updated user {modelo.UsuarioEdicion.Email}",
                    Detalles = $"{modelo.UsuarioPeticion.Email} modifico {modelo.UsuarioEdicion.Email} at {fecha}",
                    Id = usuarioPeticion.Id,
                    IdTipoActividad = tipoActividad.IdTipoActividad,
                    FechaActividad = fecha
                });
                if(registroActividad is null)
                {
                    return Factory.ObtenerRespuesta<Respuesta>(new ModificarUsuarioResponse { Usuario = applicationUserCpy }, 200, "Proccessed User update, there were some errors while logging");
                }
            }
            catch (Exception)
            {
            }

            return Factory.ObtenerRespuesta<Respuesta>(new ModificarUsuarioResponse { Usuario = applicationUserCpy });
        }

        public async Task<Respuesta> EliminarUsuario(EliminarUsuarioModel modelo)
        {
            ApplicationUser usuarioPeticion =await  _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.RequestingUser.Email && x.Habilitado==true);
            if(usuarioPeticion is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.RequestingUser.Email}");
            }
            ApplicationUser usuarioAEliminar = await _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(X => X.Email == modelo.UserToDelete.Email);
            if(usuarioAEliminar  is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 404, $"couldn't find an user with email {modelo.UserToDelete.Email} to be deleted");
            }
            bool eliminado = await _UnidadRepositorio.UsuarioRepositorio.Eliminar(usuarioAEliminar);
            DateTime fecha = DateTime.Now;
            if(eliminado)
            {
                TipoActividad  tipoActividad =await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Eliminar Usuario");
                if(tipoActividad is null)
                {
                    usuarioAEliminar.Habilitado = true;
                    await _UnidadRepositorio.UsuarioRepositorio.Modificar(usuarioAEliminar.Id,usuarioAEliminar);
                }
                RegistroActividad registroActividad = null;
                try
                {
                    registroActividad = await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
                    {
                        ActividadDescripcion = $"User {modelo.RequestingUser} successfully deleted user {modelo.UserToDelete.Email}",
                        Detalles = $"{modelo.RequestingUser.Email} deleted {modelo.UserToDelete.Email} at {fecha}",
                        Id = usuarioPeticion.Id,
                        IdTipoActividad = tipoActividad.IdTipoActividad,
                        FechaActividad = fecha
                    });
                }
                catch (Exception)
                {
                }

                if(registroActividad is null)
                {
                    return Fabrica.Factory.ObtenerRespuesta<Respuesta>(new EliminarUsuarioResponse { Eliminado = true }, 200, "Completed with errors couldn't save a log");
                }
             
            }
            return Factory.ObtenerRespuesta<Respuesta>(new EliminarUsuarioResponse { Eliminado = eliminado });
        
        }

        public async Task<Respuesta>  ObtenerUsuario(ObtenerUsuarioModel modelo)
        {
            ApplicationUser applicationUserRequest = await _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.UsuarioPeticion.Email);
            if (applicationUserRequest == null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }

            ApplicationUser user = await _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.Usuario.Email);
            DateTime fecha = DateTime.Now;
            ApplicationUser applicationUserCpy = user.DeepCopy();
            applicationUserCpy.PasswordHash = "";
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(new ObtenerUsuarioResponse { Usuario = user },404,$"couldn't find an user with Email {modelo.Usuario.Email}");
            }
            TipoActividad tipoActividad = await _UnidadRepositorio.TipoActividadRepositorio.PrimeroOValorPredeterminado(x => x.Actividad == "Leer Usuario");
            if(tipoActividad is not null)
            {
                RegistroActividad registroActividad = await _UnidadRepositorio.RegistroActividadRepositorio.Agregar(new RegistroActividad
                {
                    Detalles = $"{modelo.UsuarioPeticion.Email} have requested information for user {modelo.Usuario.Email}",
                    ActividadDescripcion=$"{nameof(ObtenerUsuario)} at {fecha}",
                    FechaActividad=fecha,
                    IdTipoActividad = tipoActividad.IdTipoActividad,
                    Id = applicationUserRequest.Id,
                    
                }); 
                if(registroActividad is null)
                {
                    return Factory.ObtenerRespuesta<Respuesta>(new ObtenerUsuarioResponse { Usuario = applicationUserCpy }, 200, "There were some problems when logging the information but request completed successfully");
                }
            }
            return Factory.ObtenerRespuesta<Respuesta>(new ObtenerUsuarioResponse { Usuario = applicationUserCpy });

        }
    }
}
