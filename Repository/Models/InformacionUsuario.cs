using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Repository.Models
{
    public class InformacionUsuario
    {
        [Required(ErrorMessage ="User Email is required")]
        public string Email { get; set; }
    }
}
