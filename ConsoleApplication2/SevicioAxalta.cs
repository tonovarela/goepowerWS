using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenService;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Row = DocumentFormat.OpenXml.Spreadsheet.Row;

namespace ConsoleApplication2
{
    public class ServicioAxalta:Servicio,IProduccion
    {


        public ServicioAxalta()
        {            
            this._nombreTienda = "axalta";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Axalta();
            this._parametroOrden = new AuthHeaderOrder()
            {
                MasterKey = _conexion.MasterKey,
                CompanyID = _conexion.CompanyID,
                Username = _conexion.Username,
                ProducerID = _conexion.ProducerID
            };
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
                       CustomData = job.CustomDatas.Select(x => new CustomData { Name = x.Name, Value = x.Value.Replace("<nextline>", "  ") }).ToList(),
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



        public  void DescargarArchivos()
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
                try
                {
                    orden.CustomData.ForEach(x => cfile.AgregarRow(x.Name, x.Value));
                    string urlLogoDistribuidor = orden.CustomData
                                                      .Where(x => x.Name.Contains("Logo"))
                                                       .Select(x => x.Value).FirstOrDefault();
                    string extension = Path.GetExtension(urlLogoDistribuidor);
                    this.DownloadFile(urlLogoDistribuidor, _ordenFolder, $"{orden.NombreArchivo}{extension}");

                    Row cabecera = new Row();
                    cabecera.Append(
                        cfile.ConstructCell("Valor", CellValues.String),
                        cfile.ConstructCell("Campo", CellValues.String)
                        );


                    cfile.SalvarExcel($"{_ordenFolder}\\{orden.NombreArchivo}.xlsx", "Michelin", cabecera);

                }
                catch (Exception e)
                {
                    Console.WriteLine("No se pudo crear el archivo excel");
                }

                this.DownloadFile(orden.FilePdfURL, _ordenFolder, $"{orden.NombreArchivo}.pdf");




            });








        }


    }
}
