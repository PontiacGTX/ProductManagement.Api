using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class ApplicationUser:IdentityUser
    {
       
        public IList<RegistroActividad> RegistroActividades { get; set; }
        public bool Habilitado { get; set; }

        public ApplicationUser DeepCopy()
        {
            ApplicationUser appUserCpy = (ApplicationUser)this.MemberwiseClone();
            appUserCpy.Habilitado = Habilitado;
            appUserCpy.RegistroActividades = RegistroActividades;
            return appUserCpy;
        }
    }
}
