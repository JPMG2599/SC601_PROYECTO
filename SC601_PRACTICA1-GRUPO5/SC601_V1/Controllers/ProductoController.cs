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

        private KN_ProyectoEntities db = new KN_ProyectoEntities();

        #region Producto
        [HttpGet]
        public ActionResult ConsultarP()
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    // Obtener los resultados del procedimiento almacenado
                    var info = context.sp_ConsultaProd().ToList();

                    // Convertir el resultado a una lista de ProductoModel
                    var productos = info.Select(p => new ProductoModel
                    {
                        ID_Producto = p.ID_Producto,        // Asegúrate de que estos campos coincidan
                        ID_Categoria = p.ID_Categoria,      // con los nombres de las propiedades
                        Nombre = p.Nombre,                  // en ProductoModel
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Imagen = p.Imagen
                    }).ToList();



                    return View(productos); // Pasar los productos convertidos a la vista
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (opcional, puedes loguear el error)
                return View();
            }
        }

        [HttpGet]
        public ActionResult AgregarP()
        {
            using (var context = new KN_ProyectoEntities())
            {
                // Obtener las categorías desde la base de datos
                var categorias = context.Categoria.ToList(); // Asegúrate de que 'Categorias' es la tabla correcta

                // Si hay categorías, pasarlas a ViewBag
                ViewBag.Categoria = new SelectList(categorias, "ID_Categoria", "Nombre");

                return View();
            }
        }

        [HttpPost]
        public ActionResult AgregarP(ProductoModel model, HttpPostedFileBase Imagen)
        {
            try
            {
                // Guardar el resto del producto en la base de datos
                using (var context = new KN_ProyectoEntities())
                {
                    Producto prod = new Producto();
                    prod.Nombre = model.Nombre;
                    prod.Descripcion = model.Descripcion;
                    prod.Precio = model.Precio;
                    prod.Imagen = model.Imagen;
                    prod.ID_Categoria = model.ID_Categoria;

                    context.Producto.Add(prod);

                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        // Guardar la Imagen
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
                ViewBag.Error = "Error al guardar el producto: " + ex.Message;
                return View();
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
                    // Obtener el producto por ID
                    var producto = context.Producto.FirstOrDefault(p => p.ID_Producto == id);

                    if (producto == null)
                    {
                        return HttpNotFound();
                    }

                    // Obtener las categorías de la base de datos y convertirlas a CategoriaModel
                    var categorias = context.Categoria
                                            .Select(c => new CategoriaModel
                                            {
                                                IdCategoria = c.ID_Categoria,
                                                Nombre = c.Nombre
                                            })
                                            .ToList();

                    cargarComboCategorias(producto.ID_Categoria);

                    // Convertir el producto a ProductoModel
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
                // Manejo de errores
                return View();
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
                    producto.Activo = model.Activo;

                    // Si debemos actualizar la imagen
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

    }

}

