//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemSAAM
    {
        public int id_item { get; set; }
        public Nullable<int> id_producto { get; set; }
        public Nullable<int> numero_orden { get; set; }
        public Nullable<int> cantidad { get; set; }
        public string nombre { get; set; }
        public Nullable<int> records { get; set; }
        public Nullable<int> setSize { get; set; }
        public Nullable<decimal> ImporteTotal { get; set; }
    
        public virtual OrdenSAAM Orden { get; set; }
    }
}
