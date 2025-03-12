using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SC601_V1.Models
{
    public class UsuarioModel
    {
        // Mismos atributos que en la BD
        public long ID_Usuario { get; set; }
        public long ID_Rol {  get; set; }   
        public string Cedula{ get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
        public int Estado { get; set; }
    }
}