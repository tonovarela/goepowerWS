using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApplication2
{
    public class ServicioKFC : Servicio
    { 
        public ServicioKFC()
        {
            this._nombreTienda = "kfc";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.KFC();                     
        }

       

        //public new void CambiarEstatusOrdenes(string ordenes)
        //{
        //    base.CambiarEstatusOrdenes(ordenes);
        //}
        //private  bool IsBetween(int val, int low, int high)
        //{
        //    return val >= low && val <= high;
        //}

        //public void DescargarArchivos()
        //{         
        //    List<OrdenDTO> _ordenes = this.GetOrdenesConArchivos();
        //    if (_ordenes.Count == 0)
        //    {
        //        Console.WriteLine("---------------------------------------------------------------------");
        //        Console.WriteLine(String.Format("No hay ordenes en la tienda {0} para descargar PDFs", this._nombreTienda.ToUpper()));
        //        Console.WriteLine("---------------------------------------------------------------------");

        //    }           
        //    _ordenes.ForEach(orden=> 
        //    {
        //        int x = Int32.Parse(orden.SKU.Substring(orden.SKU.Length - 2, 2));
        //        string _folderCirculoK = String.Empty;                               
        //        if (this.IsBetween(x, 19, 23) || this.IsBetween(x,1,30) || this.IsBetween(x,60,70) )
        //        {                    
        //            _folderCirculoK = "Nuvera";
        //        }                
        //        if (this.IsBetween(x,31,58) || this.IsBetween(x,71,98))
        //        {                    
        //            _folderCirculoK = "Digital";
        //        }               
        //        string _ordenFolder = $"{this._workspace}\\{orden.OrdenId}\\{_folderCirculoK}";

        //        orden.Imprimir();                                
        //     this.DownloadFile(orden.FilePdfURL, _ordenFolder, orden.NombreArchivo + ".pdf");                
        //    }
        //    );

        //    _ordenes.GroupBy(orden => orden.OrdenId)
        //            .Select(orden => orden.FirstOrDefault())
        //            .ToList().ForEach(orden =>
        //    {
        //        XElement xelement = XElement.Load(orden.FileXMLURL);
        //        string _urlFileExcel = xelement.Element("OrderDetails").Element("InstructionFile").Value;
        //        string _ordenFolder = this._workspace + "\\" + orden.OrdenId;
        //        Console.WriteLine("Bajando los archivos Excel para la Tienda de Circulo K");
        //        this.DownloadFile(_urlFileExcel,_ordenFolder,orden.OrdenId+".xlsx");
        //        orden.Imprimir();

        //    });


        //}

    }
}
