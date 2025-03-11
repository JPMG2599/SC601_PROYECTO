using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SC601_V1.Models
{
    public class CambiarContrasenaModel
    {
        [Display(Name = "Contraseña Actual")]
        public string ContrasenaActual { get; set; }
        
        [Display(Name = "Nueva Contraseña")]
        public string ContrasenaNueva { get; set; }

        [Compare("ContrasenaNueva")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarContrasena { get; set; }
    }
}