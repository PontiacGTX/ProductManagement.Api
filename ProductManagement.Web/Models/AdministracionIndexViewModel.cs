using Repository.Entities;
using System.Collections.Generic;

namespace ProductManagement.Web.Models
{
    public class AdministracionIndexViewModel
    {
        public IList<Producto> Productos { get; set; }
        public IList<ApplicationUser> Usuarios { get; set; }
    }
}
