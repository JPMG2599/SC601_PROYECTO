using SC601_V1.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC601_V1.Models
{
    public class RegistroErrores
    {
        public void RegistrarError(string Mensaje, string Origen)
        {
            using (var context = new KN_ProyectoEntities())
            {
                var IdUsuario = (HttpContext.Current.Session["IdUsuario"] != null ? HttpContext.Current.Session["IdUsuario"].ToString() : "0");

                context.RegistrarError(long.Parse(IdUsuario), Mensaje, Origen);
            }
        }
    }
}