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
    
    public partial class ClienteSAAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClienteSAAM()
        {
            this.Orden = new HashSet<OrdenSAAM>();
        }
    
        public int id_cliente { get; set; }
        public string concesionaria { get; set; }
        public string fullname { get; set; }
        public string direccion { get; set; }
        public string codigo_postal { get; set; }
        public Nullable<decimal> saldo { get; set; }
        public Nullable<int> numero_dealer { get; set; }
        public Nullable<decimal> credit_limit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenSAAM> Orden { get; set; }
    }
}
