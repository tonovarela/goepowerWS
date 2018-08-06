using ConsoleApplication2.DAO;
using ConsoleApplication2.DAO.Goepower;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenChangeStatus;
using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace ConsoleApplication2.Class
{
    public class Servicio
    {        
        protected string _nombreTienda;
        protected string _workspace = "Z:\\";        
        protected string fullMonthName = String.Empty;

        protected OrderInfoSoapClient client;
        protected productioncallsSoapClient client_production;
        protected AuthHeaderOrders _conexion;
        protected AuthHeaderOrder _parametroOrden;

        public Servicio()
        {
            this.client_production = new productioncallsSoapClient();
            this.client = new OrderInfoSoapClient();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            this.client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindBySubjectName, "Starfield Root Certificate Authority");
            this.client_production.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindBySubjectName, "Starfield Root Certificate Authority");                        
        }        
        protected virtual List<OrdenDTO> GetOrdenesConArchivos()
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
                       FileExcelURL = job.CustomDatas.First().Value,
                       FileXMLURL = job.JobTicketXML,
                       CustomData = job.CustomDatas.ToList()
                   })).ToList();                
                this.fullMonthName= this.UppercaseFirst(DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")));                
                _ordenes = orders;
            }
            catch (ArgumentNullException e)
            {
                //  Console.Write("No hay ordenes");
            }
            return _ordenes;
        }
        protected void DownloadFile(string origenArchivo, string destinoCarpeta, string nombreArchivo)
        {
            WebClient client = new WebClient();
            try
            {
                Uri Uri = new Uri(origenArchivo);
                Download d = new Download();
                if (!Directory.Exists(destinoCarpeta))
                    Directory.CreateDirectory(destinoCarpeta);
                Descarga f = new Descarga();
                f.DownloadFile(origenArchivo, destinoCarpeta + "\\" + nombreArchivo);

            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("No se puede descargar el archivo por la siguiente razon {0}", e.Message));
            }

        }        
        protected string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        public virtual void ProcesarOrden(int idOrden)
        {
            this._parametroOrden = new AuthHeaderOrder()
            {
                CompanyID = this._conexion.CompanyID,
                MasterKey = this._conexion.MasterKey,
                OrderID = idOrden,
                ProducerID = this._conexion.ProducerID,
                Username = this._conexion.Username
            };

            //this._parametroOrden.OrderID = idOrden;          
            AuthReturnOrder result = this.client.GetOrder(this._parametroOrden);
            Order orden = result.Order;
            Job[] jobs = result.Jobs;
            VentaDAO venta_dao = new VentaDAO();

            bool existeVenta = venta_dao.Buscar(orden.OrderID.ToString());
            
            if (existeVenta)
            {
                Console.WriteLine("La orden {0} ya existe", orden.OrderID.ToString());
                return;
            }
            ClienteDAO cliente_dao = new ClienteDAO();
            //CteHonda cte = cliente_dao.BuscarCliente(orden.WebUserID.ToString());
            CtoCampoExtra cte = cliente_dao.BuscarClienteCampoExtra(orden.WebUserID.ToString());

            DateTime time = DateTime.Now;
            DateTime timeVencimiento = time.AddDays(7);
            #region Llenado de Venta
            Venta venta = new Venta()
            {
                Empresa = "LITO",
                Mov = "Factura Electronica",
                FechaEmision = new DateTime(time.Year, time.Month, time.Day),
                Concepto = $"SAAM {this._nombreTienda.ToUpper()}",
                Moneda = "Pesos",
                TipoCambio = 1.0,
                Usuario = "MTOVAR",
                Referencia = orden.OrderID.ToString(),
                OrdenCompra = "",
                Estatus = "SINAFECTAR",
                Cliente = cte != null ? cte.Clave : "16776",
                Almacen = "AL PT",
                Observaciones = $"SAAM {this._nombreTienda.ToUpper()}",
                Condicion = "7 DIAS",
                Vencimiento = new DateTime(timeVencimiento.Year, timeVencimiento.Month, timeVencimiento.Day),
                Agente = "A.L.P.",
                //Importe = Decimal.Parse((orden.TotalPrice + orden.ShippingPrice).ToString()),
                //Impuestos = Decimal.Parse(orden.Tax1.ToString()),
                Sucursal = 0,
                SucursalOrigen = 0,
                Atencion = "",
                AtencionTelefono = "",
                Clase = "",
                Directo = true,
                OrdenID = orden.OrderID.ToString()

            };
            #endregion
            
            int id_venta = venta_dao.Insertar(venta);
            int x = 1;

            #region  Llenado de VentaD
            foreach (Job job in jobs)
            {
                VentaD ventad = new VentaD()
                {
                    ID = id_venta,
                    Renglon = x * 2048,
                    RenglonID = x++,
                    
                    Cantidad = job.Quantity,
                    Almacen = "AL PT",
                    Articulo = job.SKU,
                    Unidad = "pza",
                    Precio = (job.TotalPrice / job.Quantity),
                    Impuesto1 = 16,
                    DescripcionExtra = job.JobName,
                    RenglonTipo = 'N'
                };
                venta_dao.InsertarDetalle(ventad);
            }
            #endregion

            #region Envio
            if (orden.ShippingPrice > 0)
            {
                VentaD detalleEnvio = new VentaD()
                {
                    ID = id_venta,
                    DescripcionExtra = "Gastos de Envio",
                    RenglonID = x,
                    RenglonTipo='N',
                    Renglon = x * 2048,
                    Articulo = "EN",
                    Precio = orden.ShippingPrice,
                    Cantidad = 1,
                    Impuesto1 = 16,
                    Unidad = "Servicio",
                    Almacen="AL PT"
                };
                venta_dao.InsertarDetalle(detalleEnvio);
            }
            #endregion

        }
        protected  void CambiarEstatusOrdenes(string ordenes)
        {
            AuthHeader header = null;
            switch (this._nombreTienda.ToUpper())
            {
                case "KUMON":
                    AuthHeaderOrders credenciales = Credenciales.Kumon();
                    header = new AuthHeader()
                    {
                        CompanyID = credenciales.CompanyID,
                        MasterKey = credenciales.MasterKey,
                        ProducerID = credenciales.ProducerID,
                        Status = ePowerOrderStatus.Release,
                        OrderIDs = ordenes                         
                    };
                    break;
                case "CIRCLEK":
                    AuthHeaderOrders credencialesk = Credenciales.CirculoK();
                    header = new AuthHeader()
                    {
                        CompanyID =credencialesk.CompanyID,
                        MasterKey = credencialesk.MasterKey,
                        ProducerID = credencialesk.ProducerID,
                        Status = ePowerOrderStatus.Release,
                        OrderIDs = ordenes
                    };
                    break;
            }
            var result = client_production.ChangeStatus(header);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Se actualizo {result.IsSuccessful}");
            Console.WriteLine($" Mensaje  {result.Message}");
            Console.WriteLine("----------------------------------------------");
        }
        protected int[] GetListaOrdenes(OrderStatuses status)
        {            
            this._conexion.OrderStatus =status;
            int[] ordenes_id = this.client.GetOrdersList(_conexion).OrdersList;
            return ordenes_id;
        }

       

        public void LlenarInfoDB()
        {
            int[] ordenes_id = this.GetListaOrdenes(OrderStatuses.Pending);
            if (ordenes_id == null)
            {
                Console.WriteLine($"No hay ordenes por procesar en la tienda {this._nombreTienda}");
                return;
            }
            Console.WriteLine($"Total de ordenes {ordenes_id.Length} en la tienda {this._nombreTienda}");
            foreach (int orden_id in ordenes_id)
            {
                Console.WriteLine("Procesando la orden " + orden_id);
                this.ProcesarOrden(orden_id);
            }

         
        }


        public void LlenarInfoGoePower()
        {
            //this._conexion.StartDate = DateTime.Today.AddDays(-50);
            this._conexion.StartDate = new DateTime(2018, 1, 1);
            this._conexion.EndDate = DateTime.Now;
            int[] ordenes = this.GetListaOrdenes(OrderStatuses.All);
            if (ordenes == null)
            {
                Console.WriteLine($"No hay ordenes por procesar en la tienda {this._nombreTienda}");
                return;
            }
            Console.WriteLine($"Total de ordenes {ordenes.Length} en la tienda {this._nombreTienda}");
            
            foreach (int orden in ordenes)
            {
                OrdenDAO ordenDao = new OrdenDAO();
                ItemDAO itemDao = new ItemDAO();

                string __ordenID = $"{this._nombreTienda}_{orden}";



                this._parametroOrden = new AuthHeaderOrder()
                {
                    CompanyID = this._conexion.CompanyID,
                    MasterKey = this._conexion.MasterKey,
                    OrderID = orden,
                    ProducerID = this._conexion.ProducerID,
                    Username = this._conexion.Username
                };


                AuthReturnOrder result = this.client.GetOrder(this._parametroOrden);
                Order order = result.Order;
                Job[] jobs = result.Jobs;

                Orden ordendDto = new Orden()
                {
                    tienda = this._nombreTienda,
                    ordenID = __ordenID,
                    status = order.OrderStatus,
                    webUserID = order.WebUserID,
                    completeDate = order.CompletedDate,
                    orderDate = order.OrderDate,
                    releaseDate = order.ReleaseDate,
                    shippingDate = order.ShippingDate,
                    Total= (float)order.TotalPrice
                  

                };

                var o = ordenDao.existe(__ordenID);
                if (o!=null )
                {

                    if (o.status!= order.OrderStatus)
                    {
                        Console.WriteLine("Es diferente");
                        Console.WriteLine($"La orden {__ordenID} {o.status}  {order.OrderStatus} ya existe y cambio de estatus");
                        ordenDao.Actualiza(ordendDto);
                        
                    }                    
                    
                    continue;
                }
                
                Console.WriteLine($"Insertando la orden {__ordenID}");
                ordenDao.Agregar(ordendDto);
                foreach (Job job in jobs)
                {
                    Console.WriteLine($"Insertando el job {job.JobID}");
                    int cantidad = job.Quantity == job.Records ? job.Quantity : job.Records;
                    Item itemDto = new Item()
                    {
                        cantidad = cantidad,
                        descripcion = job.JobName,
                        ordenID = __ordenID,
                        precio = (job.TotalPrice / cantidad),
                        sku = job.SKU,

                    };
                    itemDao.Agregar(itemDto);
                }
                if (order.ShippingPrice > 0)
                {
                    Console.WriteLine($"Insertando el envio");
                    Item envio = new Item()
                    {
                        sku = "EN",
                        descripcion = "Gastos de Envio",
                        cantidad = 1,
                        precio = order.ShippingPrice,
                        ordenID = __ordenID
                    };
                    itemDao.Agregar(envio);
                }





            }

        }







    }
}
