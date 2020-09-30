using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenChangeStatus;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication2
{
    public  class ServicioSeat:Servicio
    {
        public ServicioSeat()
        {                       
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
                FileExcel cfile = new FileExcel();
                Row cabecera = new Row();
                cabecera.Append(cfile.ConstructCell("Valor", CellValues.String),cfile.ConstructCell("Campo", CellValues.String));
                orden.CustomData.ForEach(x => cfile.AgregarRow(x.Name, x.Value));                
                cfile.SalvarExcel($"{_ordenFolder}\\{orden.NombreArchivo}.xlsx", this._nombreTienda, cabecera);
            }
            );

           

        }

        public void LiberarOrdenesReleaseToProduccion()
        {

            Console.WriteLine(this._conexion.StartDate);
            Console.WriteLine(this._conexion.EndDate);
            int[] numero_ordenes=this.GetListaOrdenes(OrdenService.OrderStatuses.Released);
            //Cambiar ordenes de Pending a Produccion
            if (numero_ordenes!=null)
            {
                this.CambiarEstatusOrdenes(string.Join(",", numero_ordenes), ePowerOrderStatus.Release);
                this.CambiarEstatusOrdenes(string.Join(",", numero_ordenes), ePowerOrderStatus.ToProduction);
            }
            else
            {
                Console.Write("No hay ordenes para cambiar de pending a produccion");
            }

            


        }
    }
}
