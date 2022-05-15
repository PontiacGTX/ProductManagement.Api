using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("error/{statuscode}")]
        public IActionResult HTTPStatusCodeHandler(int statuscode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            TempData.TryGetValue("errorDetails", out object detalles);
                string errorDetails = null;
                if (detalles is not null)
                {
                    errorDetails = detalles.ToString();
                }
            switch(statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = $"Hubo un error en la peticion no pudo encontrar el articulo {errorDetails}";
                        break;
                default:
                    ViewBag.ErrorMessage = $"Hubo un error en inesperado {errorDetails}";
                    break;
            }
            return View();
        }
    }
}
