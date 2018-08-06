using ConsoleApplication2.Class;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
 public class ServicioMichelin:Servicio
    {
        public ServicioMichelin()
        {
            this._nombreTienda = "michelin";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Michelin();            
        }
        protected override List<OrdenDTO> GetOrdenesConArchivos()
        {
            List<OrdenDTO> _ordenes = new List<OrdenDTO>();          
            var data = this.client.GetOrdersWithProductionFiles(this._conexion);
            try
            {
                var orders = data.Orders.SelectMany(
                  orden => orden.Jobs.Where(job => job.ProductType == "Blocks")
                  .Select(job =>
                   new OrdenDTO
                   {
                       OrdenId = orden.OrderID,
                       JobId = job.JobID,
                       SKU = job.JobSKU,
                       FilePdfURL = job.CustomProductionFileUrl,
                       NombreArchivo = $"{orden.OrderID}_{job.JobID}_{job.JobSKU}",
                       customDatas = job.CustomDatas.Select(x => new { x.Name, x.Value })
                                                    .ToDictionary(x=>x.Name,x=>x.Value.Replace("<nextline>", "  ")),

                             
                   })).ToList();


                DateTime fechaOrden = data.Orders.FirstOrDefault().OrderDate;
                this.fullMonthName = this.UppercaseFirst(fechaOrden.ToString("MMMM",
                                                                             CultureInfo.CreateSpecificCulture("es")));


                _ordenes = orders;

            }
            catch (ArgumentNullException e)
            {
                //  Console.Write("No hay ordenes");
            }



            return _ordenes;


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

                string _ordenFolder = $"{ this._workspace}\\{ orden.OrdenId}";
                Console.WriteLine(_ordenFolder);
                orden.Imprimir();
                FileExcel cfile = new FileExcel();
                orden.CustomData.ForEach(x => cfile.AgregarRow(x.Name,x.Value));
                string urlLogoDistribuidor = orden.CustomData
                                                  .Where(x => x.Name == "Logo Distribuidor")
                                                   .Select(x=>x.Value).FirstOrDefault();
                //orden.customDatas.ToList().ForEach(x => cfile.AgregarRow(x.Key, x.Value));
                //string urlLogoDistribuidor = orden.customDatas.Where(x => x.Key == "Logo Distribuidor")
                //                                              .Select(x => x.Value)
                //                                              .FirstOrDefault();
                string extension =Path.GetExtension(urlLogoDistribuidor);
                this.DownloadFile(orden.FilePdfURL, _ordenFolder, $"{orden.NombreArchivo}.pdf");
                this.DownloadFile(urlLogoDistribuidor, _ordenFolder, $"{orden.NombreArchivo}{extension}");
                cfile.SalvarExcel($"{_ordenFolder}\\{orden.NombreArchivo}.xlsx");



            });








        }



    }
}
