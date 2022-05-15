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
    public class ProductosServicios
    {
        HttpClient _HttpClient { get; set; }
        public AppSettingsAccess _AppSettings { get; }

        string BuildUrl(string endpoint,string parameter=null) => $"{_AppSettings.ApiBaseUrl}{endpoint}";
        public ProductosServicios(HttpClient httpClient, AppSettingsAccess appSettingsAccess)
        {
            _HttpClient = httpClient;
            _AppSettings = appSettingsAccess;
        }
        
        public async Task<Producto> CrearProducto(CrearProductoModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            Producto productoResponse =null; 
            try
            {
                var url = BuildUrl(_AppSettings.EndpointAgregarProducto);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage= await _HttpClient.PostAsync(url, new StringContent(producto,Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message,respuesta.StatusCode);
                }
                productoResponse = JsonConvert.DeserializeObject<CrearProductoResponse>(respuesta.Data.ToString()).Producto;
                return productoResponse;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<bool> ExisteIdProducto(ExisteIdProductoModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointExisteIdProducto);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage= await _HttpClient.PostAsync(url, new StringContent(producto,Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message,respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ExisteProductoResponse>(respuesta.Data.ToString()).Existe;
            }
            catch (Exception ex)
            {

                throw;
            }
        } 
        public async Task<bool> ExisteNombreProducto(ExisteNombreProductoModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointExisteNombreProducto);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage= await _HttpClient.PostAsync(url, new StringContent(producto,Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message,respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ExisteProductoResponse>(respuesta.Data.ToString()).Existe;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<bool> EliminarProducto(EliminarProductoModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            
            try
            {

                var url = BuildUrl(_AppSettings.EndpointEliminarProducto);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return (bool)JsonConvert.DeserializeObject<EliminarProductoResponse>(respuesta.Data.ToString())?.Eliminado!;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Producto> ActualizarProducto(ActualizarProductoModel modelo)
        {
            HttpResponseMessage responseMessage =null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointActualizarProducto);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ActualizarProductoResponse>(respuesta.Data.ToString())?.Producto; 
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Producto> ObtenerProducto(ObtenerProductoModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointObtenerProducto);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ObtenerProductoResponse>(respuesta.Data.ToString())?.Producto;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IList<Producto>> ObtenerListaProductos(ObtenerProductosModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointObtenerListaProductos);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
                return JsonConvert.DeserializeObject<ObtenerProductosResponse>(respuesta.Data.ToString()).Productos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IList<Producto>> ObtenerTodosProductos(ObtenerTodosProductosModel modelo)
        {
            HttpResponseMessage responseMessage = null;
            Respuesta respuesta = null;
            try
            {
                var url = BuildUrl(_AppSettings.EndpointObtenerTodosProductos);
                var producto = JsonConvert.SerializeObject(modelo);
                responseMessage = await _HttpClient.PostAsync(url, new StringContent(producto, Encoding.UTF8, "application/json"));
                respuesta = JsonConvert.DeserializeObject<Respuesta>(await responseMessage.Content.ReadAsStringAsync());
                

                if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized || responseMessage.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ServerException(respuesta.Message, respuesta.StatusCode);
                }
               return JsonConvert.DeserializeObject<ObtenerProductosResponse>(respuesta.Data.ToString()).Productos;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
