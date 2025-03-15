using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SC601_V1.BaseDatos;
using SC601_V1.Models;

namespace SC601_V1.Controllers
{
    public class CategoriaController : Controller
    {
        RegistroErrores error = new RegistroErrores();

        [HttpGet]
        public ActionResult ConsultarCategorias()
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    var info = context.SP_ConsultarCategorias().ToList();
                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ConsultarCategorias");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AgregarCategoria()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get AgregarCategoria");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AgregarCategoria(CategoriaModel model)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    Categoria tabla = new Categoria();
                    tabla.Nombre = model.Nombre;
                    tabla.Descripcion = model.Descripcion;
                    tabla.Imagen = model.Imagen;

                    context.Categoria.Add(tabla);
                    var result = context.SaveChanges();

                    if (result > 0)
                        return RedirectToAction("AgregarCategoria", "Categoria");
                    else
                    {
                        ViewBag.Mensaje = "La información no se ha podido registrar correctamente";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get AgregarCategoria");
                return View("Error");
            }
        }
    }
}
