using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Shared
{
    public class AppSettingsAccess
    {
        IConfiguration Configuration { get; set; }
        public AppSettingsAccess()
        {

            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            Configuration = configuration;

        }
        public AppSettingsAccess(string path)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(path)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            Configuration = configuration;

        }
        public string ApiBaseUrl => Configuration["ServiciosAdministracion:baseurlApi"];
        public string EndpointObtenerUsuario => Configuration["ServiciosAdministracion:ObtenerUsuario"];
        public string EndpointExisteUsuario => Configuration["ServiciosAdministracion:ExisteUsuario"];
        public string EndpointCrearUsuario => Configuration["ServiciosAdministracion:CrearUsuario"];
        public string EndpointAgregarProducto => Configuration["ServiciosAdministracion:AgregarProducto"];
        public string EndpointEliminarProducto => Configuration["ServiciosAdministracion:EliminarProducto"];
        public string EndpointActualizarProducto => Configuration["ServiciosAdministracion:ActualizarProducto"];
        public string EndpointExisteNombreProducto => Configuration["ServiciosAdministracion:ExisteNombreProducto"];
        public string EndpointExisteIdProducto => Configuration["ServiciosAdministracion:ExisteIdProducto"];
        public string EndpointObtenerProducto => Configuration["ServiciosAdministracion:ObtenerProducto"];
        public string EndpointObtenerListaProductos => Configuration["ServiciosAdministracion:ObtenerListaProductos"];
        public string EndpointObtenerTodosProductos => Configuration["ServiciosAdministracion:ObtenerTodosProductos"];
        public string EndpointObtenerLogs => Configuration["ServiciosAdministracion:ObtenerLogs"];
        public string EndpointObtenerLogsProductos => Configuration["ServiciosAdministracion:ObtenerLogsProductos"];



    }
}
