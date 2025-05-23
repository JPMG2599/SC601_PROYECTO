﻿using Newtonsoft.Json.Linq;
using SC601_V1.BaseDatos;
using SC601_V1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace SC601_V1.Controllers
{
    public class UsuarioController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();

        #region IniciarSesion
        // GET: Mostrar Inicio de Sesión
        public ActionResult IniciarSesion()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get IniciarSesion");
                return View("Error");
            }
        }

        // POST: Iniciar Sesión validando la información en la base de datos
        [HttpPost]
        public ActionResult IniciarSesion(UsuarioModel model)
        {
            try
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
                        Session["Correo"] = info.Correo;

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
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post IniciarSesion");
                return View("Error");
            }

        }
        #endregion

        #region RecuperarContraseña
        // GET: Mostrar formulario Recuperar Contraseña
        public ActionResult RecuperarContrasena()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RecuperarContrasena");
                return View("Error");
            }
        }

        // POST: Comprueba que el correo exista en la BD y envía el correo
        [HttpPost]
        public ActionResult RecuperarContrasena(UsuarioModel model)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    // Verificar si el correo existe antes de ejecutar el SP
                    var existeCorreo = context.Usuario.Any(u => u.Correo == model.Correo);

                    if (!existeCorreo)
                    {
                        ViewBag.Error = "El correo ingresado no se encuentra registrado.";
                        return View(model);
                    }

                    // Si existe, ejecuta el SP
                    var info = context.SP_ResetearContrasena(model.Correo).FirstOrDefault();

                    if (info != null)
                    {
                        Session["Correo"] = model.Correo;
                        string mensaje = $"Hola {info.Nombre}, por favor utilice el siguiente código para ingresar al sistema: {info.NuevaContrasena}";
                        var notificacion = util.EnviarCorreo(info.Correo, mensaje, "Recuperar Contraseña");

                        if (notificacion)
                        {
                            return RedirectToAction("CambiarContrasena", "Usuario");
                        }
                        else
                        {
                            ViewBag.Error = "No se pudo enviar el correo. Por favor, intente nuevamente.";
                            return View(model);
                        }
                    }

                    // Como control extra (por si info fuera null)
                    ViewBag.Error = "No se pudo generar la nueva contraseña. Inténtelo más tarde.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post RecuperarContrasena");
                ViewBag.Error = "Ocurrió un error inesperado. Por favor, intente más tarde.";
                return View(model);
            }
        }


        #endregion

        #region CambiarContraseña
        // GET: Mostrar formulario Cambiar Contraseña
        public ActionResult CambiarContrasena()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get CambiarContrasena");
                return View("Error");
            }
        }

        // POST: Comprueba que el correo exista en la BD y envía el correo
        [HttpPost]
        public ActionResult CambiarContrasena(CambiarContrasenaModel model)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    // Si la contraseña nueva y confirmar no coinciden, mostramos un error en la vista
                    if (!ModelState.IsValid) return View(model);

                    // De lo contrario, enviamos la solicitud a la BD para que válide la contraseña actual
                    var correo = Session["Correo"];
                    var info = context.SP_ActualizarContrasena((string)correo, model.ContrasenaActual, model.ContrasenaNueva);

                    // Cambio exitoso y NO ha iniciado sesión
                    if (info > 0 && Session["IdUsuario"] == null) return RedirectToAction("IniciarSesion", "Usuario");
                    // Cambio exitoso y SÍ había iniciado sesión
                    else if (info > 0 && Session["IdUsuario"] != null) return RedirectToAction("Index", "Home");
                    // Mostramos error si falla
                    else if (info <= 0)
                    {
                        ViewBag.Error = "Su información no se ha podido validar correctamente";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post CambiarContrasena");
                return View("Error");
            }
        }
        #endregion

        #region RegistrarUsuario
        // GET: Mostrar Formulario Crear cuenta
        public ActionResult RegistrarUsuario()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RegistrarCuenta");
                return View("Error");
            }
        }

        // POST: Crear un Usuario
        [HttpPost]
        public ActionResult RegistrarUsuario(UsuarioModel model)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    var result = context.SP_RegistrarUsuario(model.Cedula, model.Nombre, model.Correo, model.Contrasena, model.Telefono);

                    // Se registro el usuario
                    if (result > 0) return RedirectToAction("IniciarSesion", "Usuario");
                    // Mostramos error si falla
                    else if (result <= 0)
                    {
                        ViewBag.Error = "Su información no se ha podido validar correctamente";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post RegistrarUsuario");
                return View("Error");
            }
        }
        #endregion

        #region AdministrarUsuarios
        // GET: Mostrar Tabla de Usuarios Registrados
        public ActionResult AdministrarUsuarios()
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    var info = context.SP_ListarUsuarios().ToList();
                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get AdministrarUsuarios");
                return View("Error");
            }
        }
        #endregion

        #region ActualizarUsuario
        // GET: Mostrar Tabla de Usuarios Registrados
        public ActionResult ActualizarUsuario(long q)
        {
            try
            {
                cargarComboRoles();
                cargarComboEstado();

                using (var context = new KN_ProyectoEntities())
                {
                    var info = context.Usuario.Where(x => x.ID_Usuario == q).FirstOrDefault();
                    var model = new UsuarioModel
                    {
                        ID_Usuario = info.ID_Usuario,
                        ID_Rol = info.ID_Rol,
                        Cedula = info.Cedula,
                        Nombre = info.Nombre,
                        Correo = info.Correo,
                        Telefono = info.Telefono,
                        Estado = info.Estado ? 1 : 0
                    };

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ActualizarUsuario");
                return View("Error");
            }
        }

        // POST: Crear un Usuario
        [HttpPost]
        public ActionResult ActualizarUsuario(UsuarioModel model)   
        {
            try
            {
                cargarComboRoles();
                cargarComboEstado();

                using (var context = new KN_ProyectoEntities())
                {
                    var result = context.SP_ActualizarUsuario(model.ID_Usuario, model.ID_Rol, model.Cedula, model.Nombre, model.Correo, model.Telefono, model.Estado == 1 ? true : false);

                    if (result > 0)
                    {
                        if (Session["Rol"].ToString().Trim() == "Administrador") return RedirectToAction("AdministrarUsuarios", "Usuario");
                        else return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = "La información no se ha podido actualizar correctamente";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post ActualizarUsuario");
                return View("Error");
            }
        }
        #endregion


        [HttpGet]
        public ActionResult CerrarSesion()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("IniciarSesion", "Usuario");
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get Cerrar Sesion");
                return View("Error");
            }
        }


        private void cargarComboRoles()
        {
            using (var context = new KN_ProyectoEntities())
            {
                var info = context.SP_ListarRoles().ToList();
                var rolesCombo = new List<SelectListItem>();

                foreach (var item in info)
                {
                    rolesCombo.Add(new SelectListItem() { Value = item.ID_Rol.ToString(), Text = item.Nombre_rol });
                }
                ViewBag.RolesCombo = rolesCombo ?? new List<SelectListItem>();
            }
        }

        private void cargarComboEstado()
        {

            var estadoCombo = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "1"},
                new SelectListItem { Text = "Inactivo", Value = "0"}
            };

            ViewBag.EstadoCombo = estadoCombo;

        }

    }
}