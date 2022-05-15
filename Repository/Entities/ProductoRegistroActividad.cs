using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class ProductoRegistroActividad
    {
        [Key]
        public int IDProductoRegistroActividad { get; set; }
        public int ID { get; set; }
        [ForeignKey("ID")]
        public Producto Producto { get; set; }
        public int IDRegistroActividad { get; set; }
        [ForeignKey("IDRegistroActividad")]
        public RegistroActividad RegistroActividad { get; set; }
    }
}
