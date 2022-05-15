using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Models;
using Repository.Models.Requests;
using Repository.Models.Response;
using Repository.Repository;
using Servicios.Fabrica;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class RegistroActividadServicio
    {
        UnidadRepositorio _UnidadRepositorio { get; }
        public RegistroActividadServicio(UnidadRepositorio unidadRepositorio)
        {
            _UnidadRepositorio = unidadRepositorio;
        }
        public async Task<Respuesta> ObtenerTodosLogs(ObtenerTodosLogsModel modelo)
        {
            ApplicationUser user = await _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.UsuarioPeticion.Email);
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }

            List<ProductoRegistroActividad> registros = (List<ProductoRegistroActividad>)await _UnidadRepositorio.RegistroActividadProducto.ObtenerEntidades<RegistroActividad, Producto>(x => x != null, include => include.RegistroActividad, include => include.Producto);
            ObtenerTodosLogsResponse logs = new ObtenerTodosLogsResponse();
            Dictionary<int,TipoActividad> actividades = (await _UnidadRepositorio.TipoActividadRepositorio.ObtenerEntidades()).ToDictionary(x=>x.IdTipoActividad, y=>y);
            var y = registros.Select(async x => await Task.FromResult(new LogGenerico
            {

                IDActividadProducto = x.IDProductoRegistroActividad,
                Descripcion = (bool)((await _UnidadRepositorio.TipoActividadRepositorio.ObtenerEntidad(x.RegistroActividad.IdTipoActividad))?.Actividad?.ContainsOrDefault("Producto")) ? x.Producto.Descripcion : x.RegistroActividad.Detalles,
                Fecha = x.RegistroActividad.FechaActividad,
                TipoActividad = actividades.GetValueOrDefault(x.RegistroActividad.IdTipoActividad)?.Actividad,
                UsuarioPeticion = (await _UnidadRepositorio.UsuarioRepositorio.ObtenerEntidad(x.RegistroActividad.Id)).Email,
                UsuarioSolicitado = (await _UnidadRepositorio.UsuarioRepositorio.ObtenerEntidad(x.RegistroActividad.Id)).Email


            })).ToArray();
            logs.RegistroActividad = (await Task.WhenAll(y)).Select(x=>x).ToList();
            return Factory.ObtenerRespuesta<Respuesta>(logs);
        }
        public async Task<Respuesta> ObtenerLogsProductos(ObtenerLogsProductosModel modelo)
        {
            ApplicationUser user = await _UnidadRepositorio.UsuarioRepositorio.PrimeroOValorPredeterminado(x => x.Email == modelo.UsuarioPeticion.Email);
            if (user is null)
            {
                return Factory.ObtenerRespuesta<Respuesta>(null, 401, $"Denied couldn't find an user with email {modelo.UsuarioPeticion.Email}");
            }
            IList<TipoActividad> tipos = await _UnidadRepositorio.TipoActividadRepositorio.ObtenerEntidades(x => EF.Functions.Like(x.Actividad, "%Producto"));
            if(tipos is { Count:0})
            {
                return Factory.ObtenerRespuesta<InternalServerErrorResponse>(null, 500, $"Couldn't find Activities related to this entity");
            }
            List<ProductoRegistroActividad> registros= (List<ProductoRegistroActividad>)await _UnidadRepositorio.RegistroActividadProducto.ObtenerEntidades<RegistroActividad, Producto>(x => tipos.Any(y => y.IdTipoActividad == x.RegistroActividad.IdTipoActividad), include => include.RegistroActividad, include => include.Producto);
            ObtenerLogsProductosResponse logsProductos = new ObtenerLogsProductosResponse();
            logsProductos.RegistroActividad = (IList<ActividadProducto>)registros.Select(async x => new ActividadProducto 
            { 
                Fecha = x.RegistroActividad.FechaActividad,
                Descripcion = x.Producto.Descripcion,
                TipoActividad = tipos.FirstOrDefault(y => y.IdTipoActividad == x.RegistroActividad.IdTipoActividad)?.Actividad, IDActividadProducto = x.IDProductoRegistroActividad, 
                UsuarioPeticion = (await _UnidadRepositorio.UsuarioRepositorio.ObtenerEntidad(x.RegistroActividad.Id))?.Email
            }).ToList();

            return Factory.ObtenerRespuesta<Respuesta>(logsProductos);
        }
    }
}
