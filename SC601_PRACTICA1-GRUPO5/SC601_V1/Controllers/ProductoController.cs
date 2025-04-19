using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using SC601_V1.BaseDatos;
using SC601_V1.Models;


namespace SC601_V1.Controllers
{

    public class ProductoController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();

        private KN_ProyectoEntities db = new KN_ProyectoEntities();

        #region Producto
        [HttpGet]
        public ActionResult ConsultarP()
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    
                    var info = context.sp_ConsultaProd().ToList();

                    
                    var productos = info.Select(p => new ProductoModel
                    {
                        ID_Producto = p.ID_Producto,        
                        ID_Categoria = p.ID_Categoria,      
                        Nombre = p.Nombre,                 
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Imagen = p.Imagen
                    }).ToList();



                    return View(productos);
                }
            }
            catch (Exception ex)
            {
                Exception realError = ex;
                while (realError.InnerException != null)
                    realError = realError.InnerException;

                ViewBag.Error = "Error al visualizar los producto: " + ex.Message + " | Detalle real: " + realError.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AgregarP()
        {
            using (var context = new KN_ProyectoEntities())
            {
                
                var categorias = context.Categoria.ToList(); 
                
                ViewBag.Categoria = new SelectList(categorias, "ID_Categoria", "Nombre");

                return View();
            }
        }

        [HttpPost]
        public ActionResult AgregarP(ProductoModel model, HttpPostedFileBase Imagen)
        {
            try
            {
                
                using (var context = new KN_ProyectoEntities())
                {
                    Producto prod = new Producto();
                    prod.Nombre = model.Nombre;
                    prod.Descripcion = model.Descripcion;
                    prod.Precio = model.Precio;
                    prod.Imagen = model.Imagen;
                    prod.ID_Categoria = model.ID_Categoria;
                    prod.Activo = true;

                    context.Producto.Add(prod);

                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        
                        string extension = Path.GetExtension(Imagen.FileName);
                        string ruta = Utilitarios.RutaProductos + prod.ID_Producto + extension;

                        Imagen.SaveAs(ruta);

                        prod.Imagen = "/Imagenes/Productos/" + prod.ID_Producto + extension;
                        context.SaveChanges();
                        return RedirectToAction("ConsultarP"); // Redirige después de guardar el producto
                    }
                    else
                    {
                        ViewBag.Mensaje = "La información no se ha podido registrar correctamente.";
                        return View();
                    }
                }

            }
            catch (Exception ex)
            {
                Exception realError = ex;
                while (realError.InnerException != null)
                    realError = realError.InnerException;

                ViewBag.Error = "Error al agregar el producto: " + ex.Message + " | Detalle real: " + realError.Message;
                return View("Error");
            }
        }

        // GET: Actualizar Producto
        [HttpGet]
        public ActionResult ActualizarP(int id)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    
                    var producto = context.Producto.FirstOrDefault(p => p.ID_Producto == id);

                    if (producto == null)
                    {
                        return HttpNotFound();
                    }

                   
                    var categorias = context.Categoria
                                            .Select(c => new CategoriaModel
                                            {
                                                IdCategoria = c.ID_Categoria,
                                                Nombre = c.Nombre
                                            })
                                            .ToList();

                    cargarComboCategorias(producto.ID_Categoria);

                    
                    var productoModel = new ProductoModel
                    {
                        ID_Producto = producto.ID_Producto,
                        Nombre = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        Precio = producto.Precio,
                        ID_Categoria = producto.ID_Categoria,
                        Imagen = producto.Imagen
                    };

                    return View(productoModel);
                }
            }
            catch (Exception ex)
            {
                Exception realError = ex;
                while (realError.InnerException != null)
                    realError = realError.InnerException;

                ViewBag.Error = "Error: " + ex.Message + " | Detalle real: " + realError.Message;
                return View("Error");
            }
        }

        // POST: Actualizar Producto
        [HttpPost]
        public ActionResult ActualizarP(Producto model, HttpPostedFileBase Imagen)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    var producto = context.Producto.Where(x => x.ID_Producto == model.ID_Producto).FirstOrDefault();

                    producto.ID_Categoria = model.ID_Categoria;
                    producto.Nombre = model.Nombre;
                    producto.Descripcion = model.Descripcion;
                    producto.Precio = model.Precio;
                    //producto.Activo = model.Activo;

                   
                    if (Imagen != null)
                    {
                        // Guardar la imagen
                        string extension = Path.GetExtension(Imagen.FileName);
                        string ruta = Utilitarios.RutaProductos + producto.ID_Producto + extension;

                        // Eliminamos la imagen existente para reemplazarla
                        if (producto.Imagen != null) 
                            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + producto.Imagen);
                        
                        // Guardamos la imagen en la carpeta
                        Imagen.SaveAs(ruta);

                        producto.Imagen = "/Imagenes/Productos/" + producto.ID_Producto + extension;
                    }

                    var result = context.SaveChanges();

                    return RedirectToAction("ConsultarP");
                    
                }
            }
            catch (Exception ex)
            {
                Exception realError = ex;
                while (realError.InnerException != null)
                    realError = realError.InnerException;

                ViewBag.Error = "Error al actualizar el producto: " + ex.Message + " | Detalle real: " + realError.Message;
                return View("Error");
            }

        }
        #endregion

        // GET: ELIMINAR
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            using (var context = new KN_ProyectoEntities())
            {
                // Buscar el producto por ID
                var producto = context.Producto.SingleOrDefault(p => p.ID_Producto == id);

                if (producto == null)
                {
                    return HttpNotFound(); // Si no se encuentra el producto, mostrar error
                }

                // Convertir el producto de Entity Framework a ProductoModel
                var productoModel = new ProductoModel
                {
                    ID_Producto = producto.ID_Producto,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    ID_Categoria = producto.ID_Categoria
                };

                return View(productoModel); // Pasar el ProductoModel a la vista
            }
        }


        // POST: Confirmar eliminación
        [HttpPost]
        public ActionResult EliminarConfirmado(long id)
        {
            using (var context = new KN_ProyectoEntities())
            {
                // Llamar al stored procedure directamente desde el contexto
                context.sp_EliminarProducto(id);

                // Redirigir después de la eliminación
                return RedirectToAction("ConsultarP");
            }
        }
        private void cargarComboCategorias(long? selectedId = null)
        {
            using (var context = new KN_ProyectoEntities())
            {
                var comboCategorias = context.Categoria
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID_Categoria.ToString(),
                        Text = c.Nombre,
                        Selected = (selectedId != null && c.ID_Categoria == selectedId)
                    })
                    .ToList();

                ViewBag.Categoria = comboCategorias ?? new List<SelectListItem>();
            }
        }
        // GET: CATALOGO
        [HttpGet]
        public ActionResult Catalogo()
        {
            try
            { 
            using (var context = new KN_ProyectoEntities())
            {
                var info = context.sp_ConsultaProd().ToList();

                var productos = info
                    .Where(p => p.Activo == true)
                    .Select(p => new ProductoModel
                    {
                        ID_Producto = p.ID_Producto,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Imagen = p.Imagen,
                        ID_Categoria = p.ID_Categoria
                    })
                    .ToList();

                return View(productos);
            }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get Catalogo");
                return View("Error");
            }
        }



    }

}

