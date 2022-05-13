using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
namespace Repository.Entities
{
    public class TipoActividad
    {
        [Key]
        public int IdTipoActividad { get; set; }
        public string Actividad { get; set; }

        public IList<RegistroActividad> RegistroActividades { get; set; }
    }
}
