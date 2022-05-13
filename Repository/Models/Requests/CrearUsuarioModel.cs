using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Requests
{
    public class CrearUsuarioModel
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string  Phone { get; set; }
    }
}
