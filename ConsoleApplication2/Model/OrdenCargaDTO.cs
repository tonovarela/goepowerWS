using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Model
{
    public class OrdenCargaDTO
    {

        public string numero_orden { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int Id_BillingMethod { get; set; }
        public decimal ShipingPrice { get; set; }

        
        public string City { get; set; }
        public List<JobCargarDTO> jobs { get; set; }
         
    }
}
