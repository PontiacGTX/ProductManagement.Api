using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Producto
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public double  Precio { get; set; }
        [Required]
        public long Existencia { get; set; }
        public IList<ProductoRegistroActividad> RegistroActividadProducto { get; set; }
        public bool Habilitado { get; set; }

    }
}
