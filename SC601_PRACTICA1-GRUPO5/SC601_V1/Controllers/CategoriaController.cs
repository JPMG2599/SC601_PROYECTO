using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
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
        public ActionResult AgregarCategoria(CategoriaModel model, HttpPostedFileBase ImagenCategoria)
        {
            try
            {
                if (ImagenCategoria == null || ImagenCategoria.ContentLength == 0)
                {
                    ViewBag.Mensaje = "Debe seleccionar una imagen válida.";
                    return View();
                }

                using (var context = new KN_ProyectoEntities())
                {
                    Categoria tabla = new Categoria
                    {
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        Activo = true,
                        Imagen = "pendiente" 
                    };

                    
                    context.Categoria.Add(tabla);
                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
                        string carpeta = Path.Combine(rutaBase, "ImagenesCategorias");

                        if (!Directory.Exists(carpeta))
                            Directory.CreateDirectory(carpeta);

                        string extension = Path.GetExtension(ImagenCategoria.FileName);
                        string ruta = Path.Combine(carpeta, tabla.ID_Categoria + extension);
                        ImagenCategoria.SaveAs(ruta);

                        tabla.Imagen = "/ImagenesCategorias/" + tabla.ID_Categoria + extension;
                        context.SaveChanges(); 

                        return RedirectToAction("ConsultarCategorias", "Categoria");
                    }
                    else
                    {
                        ViewBag.Mensaje = "La información no se ha podido registrar correctamente.";
                        return View();
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        error.RegistrarError($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}", "Post AgregarCategoria");
                    }
                }

                var firstError = ex.EntityValidationErrors
                                  .SelectMany(e => e.ValidationErrors)
                                  .Select(e => $"Campo: {e.PropertyName}, Error: {e.ErrorMessage}")
                                  .FirstOrDefault();

                ViewBag.Mensaje = "Error al guardar: " + firstError;
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post AgregarCategoria");
                ViewBag.Mensaje = "Error general: " + ex.Message;
                return View();
            }
        }


        [HttpGet]
        public ActionResult ActualizarCategoria(long q)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    var info = context.Categoria.Where(x => x.ID_Categoria == q).FirstOrDefault();
                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ActualizarCategoria");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ActualizarCategoria(Categoria model, HttpPostedFileBase ImagenCategoria)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    var info = context.Categoria.FirstOrDefault(x => x.ID_Categoria == model.ID_Categoria);

                    if (info == null)
                    {
                        ViewBag.Mensaje = "La categoría no existe.";
                        return View("Error");
                    }

                    info.Nombre = model.Nombre;
                    info.Descripcion = model.Descripcion;

                    if (ImagenCategoria != null && ImagenCategoria.ContentLength > 0)
                    {
                        string extension = Path.GetExtension(ImagenCategoria.FileName);
                        string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImagenesCategorias");
                        string nuevaRuta = Path.Combine(folder, model.ID_Categoria + extension);

                        // Asegurarse que exista la carpeta
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);

                        // Eliminar imagen anterior si existe físicamente
                        if (!string.IsNullOrEmpty(info.Imagen))
                        {
                            string rutaAnterior = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, info.Imagen.TrimStart('/').Replace("/", "\\"));
                            if (System.IO.File.Exists(rutaAnterior))
                                System.IO.File.Delete(rutaAnterior);
                        }

                        // Guardar nueva imagen
                        ImagenCategoria.SaveAs(nuevaRuta);

                        // Actualizar ruta relativa para base de datos
                        info.Imagen = "/ImagenesCategorias/" + model.ID_Categoria + extension;
                    }

                    var result = context.SaveChanges();

                    if (result > 0)
                        return RedirectToAction("ConsultarCategorias", "Categoria");
                    else
                    {
                        ViewBag.Mensaje = "La información no se ha podido actualizar correctamente";
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception realError = ex;
                while (realError.InnerException != null)
                    realError = realError.InnerException;

                error.RegistrarError(ex.Message, "Post ActualizarCategoria");
                ViewBag.Error = "Error al actualizar la categoría: " + ex.Message + " | Detalle real: " + realError.Message;
                return View("Error");
            }
        }


        //[HttpPost]
        //public ActionResult ActualizarCategoria(Categoria model, HttpPostedFileBase ImagenCategoria)
        //{
        //    try
        //    {
        //        using (var context = new KN_ProyectoEntities())
        //        {
        //            var info = context.Categoria.Where(x => x.ID_Categoria == model.ID_Categoria).FirstOrDefault();

        //            if (info == null)
        //            {
        //                ViewBag.Mensaje = "La categoría no fue encontrada.";
        //                return View(model);
        //            }

        //            info.Nombre = model.Nombre;
        //            info.Descripcion = model.Descripcion;

        //            if (ImagenCategoria != null && ImagenCategoria.ContentLength > 0)
        //            {
        //                string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
        //                string carpeta = Path.Combine(rutaBase, "ImagenesCategorias");
        //                if (!Directory.Exists(carpeta))
        //                    Directory.CreateDirectory(carpeta);

        //                string extension = Path.GetExtension(ImagenCategoria.FileName);
        //                string nuevaRutaRelativa = "/ImagenesCategorias/" + model.ID_Categoria + extension;
        //                string rutaCompleta = Path.Combine(carpeta, model.ID_Categoria + extension);

        //                // Borrar imagen anterior si existe
        //                string rutaAnteriorCompleta = Path.Combine(rutaBase, info.Imagen?.TrimStart('/').Replace("/", "\\") ?? "");
        //                if (System.IO.File.Exists(rutaAnteriorCompleta))
        //                    System.IO.File.Delete(rutaAnteriorCompleta);

        //                ImagenCategoria.SaveAs(rutaCompleta);
        //                info.Imagen = nuevaRutaRelativa;
        //            }

        //            context.SaveChanges();

        //            return RedirectToAction("ConsultarCategorias", "Categoria");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error.RegistrarError(ex.Message, "Post ActualizarCategoria");
        //        ViewBag.Mensaje = "Error: " + ex.Message;
        //        return View(model);
        //    }
        //}




    }
}
