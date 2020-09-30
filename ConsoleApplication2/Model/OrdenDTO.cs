using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication2.Model
{
   public class OrdenDTO
    {

        public int OrdenId { get; set; }


        public string TipoProducto { get; set; }

        public int JobId { get; set; }

        public String SKU { get; set; }

        public String FilePdfURL { get; set; }

        public String NombreArchivo { get; set; }
        public String FileExcelURL { get; set; }

        public String FileXMLURL { get; set; }

        //public Dictionary<string, string> customDatas;

        public List<CustomData> CustomData;


        public XElement Detalle { get; set; }

        public void Imprimir()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine($"Orden ID {this.OrdenId}");
            Console.WriteLine($"File Excel {this.FileExcelURL}");
           
            Console.WriteLine($"SKU  { this.SKU}");
            Console.WriteLine($"Pdf  { this.FilePdfURL}");
            Console.WriteLine($"Nombre Archivo {this.NombreArchivo}");
            Console.WriteLine($"Xml File {this.FileXMLURL}");
            Console.WriteLine("-----------------------");
        }
        

        
    }
}
