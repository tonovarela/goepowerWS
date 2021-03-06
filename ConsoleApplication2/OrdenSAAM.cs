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
    
    public partial class OrdenSAAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrdenSAAM()
        {
            this.Item = new HashSet<ItemSAAM>();
        }
    
        public int numero_orden { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<bool> esCampana { get; set; }
        public string BillingMethod { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string estatus { get; set; }
        public Nullable<int> id_cliente { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<System.DateTime> BillingDate { get; set; }
        public Nullable<System.DateTime> CompleteDate { get; set; }
        public Nullable<System.DateTime> GateAction { get; set; }
    
        public virtual ClienteSAAM Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemSAAM> Item { get; set; }
    }
}
