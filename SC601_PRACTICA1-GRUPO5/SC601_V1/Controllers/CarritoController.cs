using SC601_V1.BaseDatos;
using SC601_V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC601_V1.Controllers
{
    public class CarritoController : Controller
    {
        private KN_ProyectoEntities db = new KN_ProyectoEntities();
        RegistroErrores error = new RegistroErrores();


        #region AgregarItem
        [HttpPost]
        public JsonResult AgregarItem(CarritoModel model)
        {
            try
            {
                var producto = db.Producto.Find(model.Producto_ID);

                // Si el producto NO existe
                if (producto == null)
                {
                    return Json(new { success = false });
                }

                // Buscamos o creamos la lista
                var carrito = Session["Carrito"] as List<CarritoModel> ?? new List<CarritoModel>();

                var existente = carrito.FirstOrDefault(p => p.Producto_ID == model.Producto_ID);

                // Si el producto a agregar al carrito YA existía, se actualiza el valor de cantidad
                if (existente != null)
                {
                    existente.cantidad += model.cantidad;
                }
                else
                {
                    carrito.Add(new CarritoModel
                    {
                        Producto_ID = model.Producto_ID,
                        Nombre = producto.Nombre, // esto viene de la BD
                        precio = producto.Precio, // este precio viene de la BD para evitar que lo modifiquen en el navegador
                        cantidad = model.cantidad
                    });
                }

                Session["Carrito"] = carrito;
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post AgregarItem");
                return Json(new { redirectToError = true });
            }

        }
        #endregion

        #region VerCarrito
        [HttpGet]
        public ActionResult VerCarrito()
        {
            try
            {
                var carrito = Session["Carrito"] as List<CarritoModel> ?? new List<CarritoModel>();
                return View(carrito);

            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get VerCarrito");
                return View("Error");
            }
        }
        #endregion

        #region EliminarItem
        [HttpGet]
        public ActionResult EliminarItem(long q)
        {
            try
            {
                var carrito = Session["Carrito"] as List<CarritoModel> ?? new List<CarritoModel>();
             
                var item = carrito.FirstOrDefault(p => p.Producto_ID == q);
                
                if (item != null)
                    carrito.Remove(item);

                
                Session["Carrito"] = carrito;
                
                return RedirectToAction("VerCarrito");
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get EliminarItemCarrito");
                return View("Error");
            }
        }
        #endregion
    }
}