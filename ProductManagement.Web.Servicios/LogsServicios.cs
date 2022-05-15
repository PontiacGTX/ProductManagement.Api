using Newtonsoft.Json;
using Repository.Models;
using Repository.Models.Requests;
using Repository.Models.Response;
using Shared;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Web.Servicios
{
    public class LogsServicios
    {
        private HttpClient _HttpClient;
        public AppSettingsAccess _AppSettings { get; }

        string BuildUrl(string endpoint, string parameter = null) => $"{_AppSettings.ApiBaseUrl}{endpoint}";

        public LogsServicios(HttpClient httpClient, AppSettingsAccess appSettingsAccess)
        {
            _HttpClient = httpClient;
            _AppSettings = appSettingsAccess;
        }
        public async Task<IList<ActividadProducto>> ObtenerLogsProductos(ObtenerLogsProductosModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointObtenerTodosProductos);
                var peticion = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(peticion, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());


                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ObtenerLogsProductosResponse>(respuesta.Data.ToString()).RegistroActividad;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IList<LogGenerico>> ObtenerTodosLogs(ObtenerTodosLogsModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointObtenerTodosProductos);
                var peticion = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(peticion, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());


                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ObtenerTodosLogsResponse>(respuesta.Data.ToString()).RegistroActividad;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
