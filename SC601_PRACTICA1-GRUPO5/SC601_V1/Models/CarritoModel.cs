using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC601_V1.Models
{
    public class CarritoModel
    {
        public long Producto_ID { get; set; }
        public string Nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}