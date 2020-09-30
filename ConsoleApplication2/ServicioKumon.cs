using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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

        protected override List<OrdenDTO> GetOrdenesConArchivos()
        {
            List<OrdenDTO> _ordenes = new List<OrdenDTO>();
             
            var data = this.client.GetOrdersWithProductionFiles(this._conexion);            
            try
            {
                var orders = data.Orders.SelectMany(
                  orden => orden.Jobs
                  //.Where(job => job.ProductType == "Blocks" || job.ProductType=="Static")
                  .Select(job =>
                   new OrdenDTO
                   {
                       OrdenId = orden.OrderID,
                       JobId = job.JobID,
                       TipoProducto= job.ProductType,
                       SKU = job.JobSKU,
                       FilePdfURL = job.CustomProductionFileUrl,
                       NombreArchivo = $"{orden.OrderID}_{job.JobID}_{job.JobSKU}",
                       FileExcelURL = job.ProductType == "Blocks"? job.CustomDatas.First().Value:" ",
                       FileXMLURL = job.JobTicketXML,
                       CustomData = job.CustomDatas.ToList(),                        
                       Detalle = orden.ToXElement<OrdenService.AuthReturnOrderWithProductionFiles>()
                   })).ToList();
                this.fullMonthName = this.UppercaseFirst(DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")));



                _ordenes = orders;
                // Registro Bitacora 
                this.RegistrarBitacoraPrintTemplate(_ordenes);
            }
            catch (ArgumentNullException e)
            {
                //  Console.Write("No hay ordenes");
            }
            return _ordenes;

        }
        //public new void CambiarEstatusOrdenes(string ordenes)
        //{
        //    base.CambiarEstatusOrdenes(ordenes);
        //}

       private void DescargaNormal(OrdenDTO orden)
        {
            string _ordenFolder = $"{ this._workspace}\\produccion\\{ this.fullMonthName}\\{ orden.OrdenId}";
            
            Console.WriteLine(_ordenFolder);
            orden.Imprimir();
            if (orden.TipoProducto == "Blocks")
            {
                this.DownloadFile(orden.FilePdfURL, _ordenFolder, orden.NombreArchivo + ".pdf");
                // Se descarga el Documento Excel
                this.DownloadFile(orden.FileExcelURL, _ordenFolder, orden.NombreArchivo + ".xlsx");
                FileExcel excel = new FileExcel();
                // Se crea un nuevo Excel unicamente con la informacion de la primera columna (el archivo original es borrado)
                excel.CrearArchivoPrimerColumna(_ordenFolder + "\\" + orden.NombreArchivo + ".xlsx");
            }
            else
            {

                XElement xelement = XElement.Load(orden.FileXMLURL);
                string _urlFileTemplate = xelement.Element("Job").Element("Product").Element("ProductDetails").Element("ProductPreviewFile").Value;
                if (_urlFileTemplate.Length != 0)
                {
                    this.DownloadFile(_urlFileTemplate, _ordenFolder, orden.NombreArchivo + ".pdf");
                    Console.WriteLine($"URL ARCHIVO NO PERSONALIZADO {_urlFileTemplate}");
                }

            }
        }

       public void DescargaEspecial(OrdenDTO orden)
        {
            string _ordenFolder = $"{ this._workspace}\\produccion\\{ this.fullMonthName}\\{ orden.OrdenId}";
            if (Directory.Exists(_ordenFolder))
            {
                return;
            }
            Console.WriteLine(_ordenFolder);
            orden.Imprimir();
            if (orden.TipoProducto == "Blocks")
            {
                this.DownloadFile(orden.FilePdfURL, _ordenFolder, orden.NombreArchivo + ".pdf");                
                FileExcel excel = new FileExcel();                                
                orden.CustomData.ForEach(data =>
                {
                    excel.AgregarRow(data.Name, data.Value.Replace("<nextline>", " "));
                    Console.WriteLine($"{data.Name} {data.Value}");
                });
                excel.SalvarExcel(_ordenFolder + "\\" + orden.NombreArchivo + ".xlsx","Kumon");                
            }

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
                String[] especiales = { "KU000036", "KU000037", "KU000038", "KU000039" };
                if (especiales.Contains(orden.SKU))
                {
                    Console.WriteLine("Se procesa diferente");
                    this.DescargaEspecial(orden);
                }
                else
                {
                    Console.WriteLine("Se procesa normal");
                    this.DescargaNormal(orden);                    
                }                                               

            });
        }

       public void CrearOrden(OrdenCargaDTO orden)
        {
            CreateOrden.CreateAPISoapClient c = new CreateOrden.CreateAPISoapClient();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            
            c.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindBySubjectName, "Starfield Root Certificate Authority");
            List<CreateOrden.ItemWithExtras> items = new List<CreateOrden.ItemWithExtras>();
            orden.jobs.ForEach(j =>
            {
                items.Add(new CreateOrden.ItemWithExtras()
                {
                    Quantity = j.Quantity,
                    ProductID = j.ProductID,
                    JobName = j.Name
                });
            });
            
            

            var response1 = c.CreateOrderWithMethods(new CreateOrden.APIOrderWithMethods()
            {
                Username = orden.Username,
                ProducerID = this._conexion.ProducerID,
                CompanyID = this._conexion.CompanyID,
                MasterKey = this._conexion.MasterKey,

                Address1 = orden.Address1,
                Address2 = orden.Address2,
                Address3 = orden.Address3,

                //Address1 = "Este es un texto", //calle + numero
                //Address2 = "Este es un texto 2",// municipio
                //Address3 = "Este es un texto 3",
                //CompanyName = "Varela Incoporacion",
                Email = orden.Email,
                FirstName = orden.Firstname,
                LastName = orden.Lastname,
                Items = items.ToArray(),
                PostalCode = orden.PostalCode,
                Country = "MX",
                City= orden.City,       
                BillingMethodID=7271,                    
                SendNotifications=false
                //ShippingMethodID=6543,
                //ShippingPrice=500,
                //UseDefaultAddress= true,
                //UseFirstStoreCreditBillingMethod= true,
                //CostCentreID=0,
                //ShippingMethodName="RedPack",
                //BillingMethodID=5845,
                //BillingMethodName="Facturacion por pedido",
                  // SendNotifications= true                    
            });
            
            
            Console.WriteLine(response1.OrderID);

        }














    }
}
