//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SC601_V1.BaseDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        public long ID_Venta { get; set; }
        public long ID_Factura { get; set; }
        public long ID_Producto { get; set; }
        public decimal Precio { get; set; }
        public long Cantidad { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
