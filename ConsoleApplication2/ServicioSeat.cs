using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication2
{
public  class ServicioSeat:Servicio
    {

        public ServicioSeat()
        {

            
            //this._workspace = "X:\\";   //192.168.3.252/preprensa//    
            this._nombreTienda = "seat";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Seat();
        }


        public void DescargarArchivos()
        {
            List<OrdenDTO> _ordenes = this.GetOrdenesConArchivos();
            if (_ordenes.Count == 0)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine(String.Format("No hay ordenes en la tienda {0} para descargar PDFs", this._nombreTienda.ToUpper()));
                Console.WriteLine("---------------------------------------------------------------------");

            }
            _ordenes.ForEach(orden =>
            {                
                string _ordenFolder = $"{this._workspace}\\{orden.OrdenId}";
                //Console.WriteLine(_ordenFolder);
                orden.Imprimir();
                this.DownloadFile(orden.FilePdfURL, _ordenFolder, orden.NombreArchivo + ".pdf");
            }
            );

            //_ordenes.GroupBy(orden => orden.OrdenId)
            //        .Select(orden => orden.FirstOrDefault())
            //        .ToList().ForEach(orden =>
            //        {
            //            XElement xelement = XElement.Load(orden.FileXMLURL);
            //            string _urlFileExcel = xelement.Element("OrderDetails").Element("InstructionFile").Value;
            //            string _ordenFolder = this._workspace + "\\" + orden.OrdenId;
            //            Console.WriteLine("Bajando los archivos Excel para la Tienda de Circulo K");
            //            this.DownloadFile(_urlFileExcel, _ordenFolder, orden.OrdenId + ".xlsx");
            //            orden.Imprimir();

            //        });


        }
    }
}
