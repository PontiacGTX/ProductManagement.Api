using Newtonsoft.Json;
using Repository.Entities;
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
    public class UsuarioServicios
    {
         HttpClient _HttpClient { get; set; }
         AppSettingsAccess _AppSettings{ get; set; }
        string BuildUrl(string endpoint) => $"{_AppSettings.ApiBaseUrl}{endpoint}";
        public UsuarioServicios(HttpClient httpClient,AppSettingsAccess appSettingsAccess)
        {
            _HttpClient = httpClient;
            _AppSettings = appSettingsAccess; 
        }

        public async Task<ApplicationUser> CrearUsuario(CrearUsuarioModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointCrearUsuario);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<CrearUsuarioResponse>(respuesta.Data.ToString()).User;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ExisteUsuario(ExisteUsuarioModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointExisteUsuario);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ExisteUsuarioResponse>(respuesta.Data.ToString()).Existe;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApplicationUser> ObtenerUsuario(ObtenerUsuarioModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointObtenerUsuario);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                { 
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<CrearUsuarioResponse>(respuesta.Data.ToString()).User;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
