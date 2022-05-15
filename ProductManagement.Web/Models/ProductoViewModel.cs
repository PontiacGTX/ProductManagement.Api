using System.ComponentModel.DataAnnotations;
using Repository.Entities;
namespace ProductManagement.Web.Models
{
   
    public class ProductoViewModel
    {
        public ProductoViewModel()
        {

        }
        public ProductoViewModel(Producto producto)
        {
            this.ID = producto.ID;
            this.Descripcion = producto.Descripcion;
            this.CantidadDisponible = producto.Existencia;
            this.Habilitado = producto.Habilitado;
            this.Precio = producto.Precio;
        }
        public int ID { get; set; }
        [Required]
        [Display(Name ="Nombre o Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name ="Cantidad Disponible")]
        [Required]
        public long CantidadDisponible { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public bool Habilitado { get; set; }
    }
}
