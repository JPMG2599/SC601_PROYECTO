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
        public ActionResult AgregarP(ProductoModel model, IFormFile Imagen)
        {
            try
            {
                if (Imagen != null && Imagen.Length > 0)
                {
                    // Define la ruta de la carpeta 'Template\img' donde se almacenarán las imágenes
                    string uploadPath = Path.Combine(Server.MapPath("~/Template/img"));

                    // Crea la carpeta si no existe
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Genera un nombre único para la imagen (puedes usar el GUID o el nombre original)
                    string imageName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);

                    // Define la ruta completa donde se guardará la imagen
                    string filePath = Path.Combine(uploadPath, imageName);

                    // Guarda la imagen en la carpeta
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Imagen.CopyTo(stream);
                    }

                    // Guarda solo la ruta relativa de la imagen en la base de datos
                    model.Imagen = "/Template/img/" + imageName;  // Usamos la ruta relativa que será accesible desde la web
                }

                // Guardar el resto del producto en la base de datos
                using (var context = new KN_ProyectoEntities())
                {
                    context.Producto.Add(new Producto
                    {
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        Precio = model.Precio,
                        Imagen = model.Imagen,
                        ID_Categoria = model.ID_Categoria
                    });

                    context.SaveChanges();
                }

                return RedirectToAction("ConsultarP"); // Redirige después de guardar el producto
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al guardar el producto: " + ex.Message;
                return View();
            }
        }
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

                    // Crear el SelectList con el modelo CategoriaModel
                    ViewBag.Categoria = new SelectList(categorias, "ID_Categoria", "Nombre", producto.ID_Categoria);

                    // Convertir el producto a ProductoModel
                    var productoModel = new ProductoModel
                    {
                        ID_Producto = producto.ID_Producto,
                        Nombre = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        Precio = producto.Precio,
                        ID_Categoria = producto.ID_Categoria
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





        [HttpPost]
        public ActionResult ActualizarP(ProductoModel model)
        {
            try
            {
                using (var context = new KN_ProyectoEntities())
                {
                    var producto = context.Producto.Find(model.ID_Producto);
                    if (producto == null) return HttpNotFound();

                    producto.Nombre = model.Nombre;
                    producto.Descripcion = model.Descripcion;
                    producto.Precio = model.Precio;

                    // Actualiza imagen solo si se sube una nueva
                    if (model.Imagen != null)
                    {
                        producto.Imagen = model.Imagen;
                    }

                    context.SaveChanges();
                }
                return RedirectToAction("ConsultarP");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al actualizar el producto: " + ex.Message;
                return View(model);
            }
        }


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


    }

}

