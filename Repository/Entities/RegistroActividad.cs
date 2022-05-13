using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repository;

namespace Repository.Entities
{
    public class RegistroActividad
    {
        [Key]
        public int IDRegistroActividad  { get; set; }
        public int IdTipoActividad { get; set; }
        [ForeignKey("IdTipoActividad")]
        public TipoActividad TipoArchividad { get; set; }
        public string ActividadDescripcion { get; set; }
        public DateTime FechaActividad { get; set; }
        public string Id { get; set; }
        [ForeignKey("IdUsuario")]
        public ApplicationUser Usuario { get; set; }
        public string Detalles { get; set; }
        public IList<ProductoRegistroActividad> RegistroActividadProductos { get; set; }
    }
}
