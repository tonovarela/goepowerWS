using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.ViewModel.Crece
{
    public class ItemViewModel
    {
        public int ProductoID { get; set; }
        public int ItemID { get; set; }

        public string sku { get; set; }

        public int CantidadOrdenada { get; set; }

        public int SetSize { get; set; }

        public int Records { get; set; }

        public string SourceFile { get; set; }

        public double totalPeso { get; set; }
    }
}
