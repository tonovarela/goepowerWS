using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Model
{
    public class TransaccionDTO
    {

        public String WorkOrden { get; set; }
        public String Tienda { get; set; }
        public int  IdTienda { get; set; }

        public bool  EsTransferencia { get; set; }

    }
}
