using Microsoft.AspNetCore.Mvc;
using ProductManagement.Web.Models;
using ProductManagement.Web.Servicios;
using System.Threading.Tasks;
using Repository.Entities;
namespace ProductManagement.Web.Controllers
{
    public class AdministracionController : Controller
    {
        public UnidadServicios UnidadServicios { get; set; }
        public AdministracionController(UnidadServicios unidadServicios)
        {
            UnidadServicios=   unidadServicios; 
        }


        private string GetEmailOrDefault()
        {
            TempData.TryGetValue("User", out object user);
            string email = null;
            if (user is not null)
            {
                email = user.ToString();
            }
            if (email is null)
            {
                email = "admin@mail.com";
            }
            return email;
        }
        [HttpGet]
        public async Task<IActionResult> EditarProducto(int ID)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string email = GetEmailOrDefault();
                    Producto producto =await   UnidadServicios.ProductosServicios.ObtenerProducto(new Repository.Models.Requests.ObtenerProductoModel
                    {
                        IdProducto = ID,
                        Usuario = new Repository.Models.InformacionUsuario { Email = email }
                    });
                    return View(new ProductoViewModel(producto));
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return await Index();
        }
        [HttpPost]
        public async Task<IActionResult> EditarProducto([FromForm] ProductoViewModel producto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var email = GetEmailOrDefault();
                    Producto prod = null;
                    try
                    {
                       prod = await UnidadServicios.ProductosServicios.ActualizarProducto(new Repository.Models.Requests.ActualizarProductoModel
                        {
                            Producto = new Producto
                            {
                                Existencia = producto.CantidadDisponible,
                                Descripcion = producto.Descripcion,
                                Habilitado = producto.Habilitado,
                                Precio = producto.Precio
                            },
                            IDProducto = producto.ID,
                            Usuario = new Repository.Models.InformacionUsuario { Email = email }
                        });
                    }
                    catch (System.Exception)
                    {
                    }
                    if(prod is null)
                    {
                        ViewBag.ErrorPeticion = "err";
                    }
                    return RedirectToAction("ProductoDetalles", new { ID = producto.ID });
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return View(producto);
        }
        [HttpGet]
        public async Task<IActionResult> AgregarProducto()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ProductoDetalles(int ID)
        {
            if(ModelState.IsValid)
            {
                try
                {
                   var email = GetEmailOrDefault();
                   var producto =  await UnidadServicios.ProductosServicios.ObtenerProducto(new Repository.Models.Requests.ObtenerProductoModel
                    {
                        IdProducto = ID,
                        Usuario = new Repository.Models.InformacionUsuario { Email = email }
                    });

                    return View(new ProductoViewModel(producto));
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return RedirectToAction("error/500");
        }
        [HttpPost]
        public async Task<IActionResult> AgregarProducto([FromForm]ProductoViewModel modelo)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var email = GetEmailOrDefault();
                    Producto producto = await  UnidadServicios.ProductosServicios
                        .CrearProducto(new Repository.Models.Requests.CrearProductoModel
                        {
                            Producto = new Repository.Entities.Producto
                            {
                                Descripcion = modelo.Descripcion, 
                                Existencia = modelo.CantidadDisponible,
                                Habilitado = modelo.Habilitado,
                                Precio = modelo.Precio 
                            },
                            Usuario =  new Repository.Models.InformacionUsuario
                            {
                                 Email =email
                            }

                        });
                    return RedirectToAction("Index");
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return View(modelo);
        }
        [HttpGet]
        public async Task<IActionResult> EliminarProducto(int ID)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var email = GetEmailOrDefault();
                   bool eliminado =await  UnidadServicios.ProductosServicios.EliminarProducto(new Repository.Models.Requests.EliminarProductoModel { IdProducto = ID, Usuario = new Repository.Models.InformacionUsuario { Email = email } });

                    if(!eliminado)
                    {
                        ViewBag.ErrorPeticion = $"No pudo eliminar el articulo con ID {ID} intentelo nuevamente";
                        return RedirectToAction("ProductoDetalles", new  { ID =ID });
                    }
                    return await Index();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return RedirectToAction("ProductoDetalles", new { ID = ID });
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdministracionIndexViewModel items = new AdministracionIndexViewModel();
            var email = GetEmailOrDefault();
            items.Productos =await UnidadServicios.ProductosServicios
                .ObtenerTodosProductos(new Repository.Models.Requests.ObtenerTodosProductosModel {
                    UsuarioPeticion= new Repository.Models.InformacionUsuario { Email =email  } });
            return View("Index",items);
        }
    }
}
