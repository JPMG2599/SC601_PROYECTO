using SC601_V1.BaseDatos;
using SC601_V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC601_V1.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Mostrar Inicio de Sesión
        public ActionResult IniciarSesion()
        {
            return View();
        }

        // POST: Iniciar Sesión validando la información en la base de datos
        [HttpPost]
        public ActionResult IniciarSesion(UsuarioModel model)
        {
            using (var context = new KN_ProyectoEntities())
            {
                var info = context.SP_IniciarSesion(model.Correo, model.Contrasena).FirstOrDefault();

                // Si encuentra algo, iniciamos sesión
                if (info != null)
                {
                    // Variables de sesión
                    Session["NombreUsuario"] = info.Nombre;
                    Session["Rol"] = info.Nombre_rol;
                    Session["IdUsuario"] = info.ID_Usuario;

                    return RedirectToAction("Index", "Home");
                }
                // Si no encuentra nada, mostramos mensaje de error
                else if (info == null)
                {
                    ViewBag.Error = "Su información no se ha podido validar correctamente";
                    return View();
                }
            }

            return View();

        }
        // -------------------------------------------------------------------

        // GET: Mostrar formulario Recuperar Contraseña
        public ActionResult RecuperarContrasena()
        {
            return View();
        }

        // POST: Comprueba que el correo exista en la BD y envía el correo
        [HttpPost]
        public ActionResult RecuperarContrasena(UsuarioModel model)
        {
            using (var context = new KN_ProyectoEntities())
            {
                // Verificamos que el correo exista en la base de datos
                // Si existe, genera una contraseña y la actualiza
                var info = context.SP_VerificarCorreo(model.Correo).FirstOrDefault();

                // Si encuentra algo, se envía el correo para restablecer la contraseña
                if (info != null)
                {
                    Session["Correo"] = model.Correo;
                    return RedirectToAction("CambiarContrasena", "Usuario");
                }
                // Si no encuentra nada, mostramos mensaje de error

                else if (info == null)
                {
                    ViewBag.Error = "Su información no se ha podido validar correctamente";
                    return View();
                }


            }
            return View();
        }
        // -------------------------------------------------------------------

        // GET: Mostrar formulario Cambiar Contraseña
        public ActionResult CambiarContrasena()
        {
            return View();
        }

        // POST: Comprueba que el correo exista en la BD y envía el correo
        [HttpPost]
        public ActionResult CambiarContrasena(CambiarContrasenaModel model)
        {
            using (var context = new KN_ProyectoEntities())
            {
                // Si la contraseña nueva y confirmar no coinciden, mostramos un error en la vista
                if (!ModelState.IsValid) return View(model);

                // De lo contrario, enviamos la solicitud a la BD para que válide la contraseña actual
                var correo = Session["Correo"];
                var info = context.SP_ActualizarContrasena((string)correo, model.ContrasenaActual, model.ContrasenaNueva);

                // Cambio exitoso
                if (info > 0) return RedirectToAction("IniciarSesion", "Usuario");
                // Mostramos error si falla
                else if (info <= 0)
                {
                    ViewBag.Error = "Su información no se ha podido validar correctamente";
                    return View();
                }
            }
            return View();
        }
        // -------------------------------------------------------------------


        // GET: Mostrar Formulario Crear cuenta
        public ActionResult RegistrarUsuario()
        {
            return View();
        }
        // -------------------------------------------------------------------

    }
}