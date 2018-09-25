using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using System;
using System.Collections.Generic;

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
                orden.Imprimir();
                this.DownloadFile(orden.FilePdfURL, _ordenFolder, orden.NombreArchivo + ".pdf");
            }
            );

           

        }
    }
}
