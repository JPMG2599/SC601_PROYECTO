using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC601_V1.Models
{
    public class ProductoModel
    {
        public long ID_Producto { get; set; }
        public long ID_Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        // Guarda solo la ruta como string
        public string Imagen { get; set; }

    }

}