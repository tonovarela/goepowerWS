using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using System;
using System.Collections.Generic;

namespace ConsoleApplication2
{
    public class ServicioKumon : Servicio
    {
        public ServicioKumon()
        {
            this._nombreTienda = "kumon";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Kumon();

         
        }


   



        public new void CambiarEstatusOrdenes(string ordenes)
        {
            base.CambiarEstatusOrdenes(ordenes);
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
                string _ordenFolder = $"{ this._workspace}\\produccion\\{ this.fullMonthName}\\{ orden.OrdenId}";
                Console.WriteLine(_ordenFolder);
                orden.Imprimir();
                this.DownloadFile(orden.FilePdfURL, _ordenFolder, orden.NombreArchivo + ".pdf");
                this.DownloadFile(orden.FileExcelURL, _ordenFolder, orden.NombreArchivo + ".xlsx");

            });
        }

       
        

       





    }
}
