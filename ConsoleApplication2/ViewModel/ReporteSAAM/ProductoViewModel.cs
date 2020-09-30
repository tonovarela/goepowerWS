using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.ViewModel.ReporteSAAM
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }

        public int  Inventario { get; set; }
        public string  Proyeccion { get; set; }

        public string Nombre { get; set; }

        public string SKU { get; set; }

        public int PiezasPorPaquete { get; set; }

        public string Miniatura { get; set; }
    }
}
