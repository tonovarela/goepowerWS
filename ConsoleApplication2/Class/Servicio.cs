
using ConsoleApplication2.DAO.Goepower;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.Entity;
using ConsoleApplication2.Model;
using ConsoleApplication2.Model.Crece;
using ConsoleApplication2.OrdenChangeStatus;
using ConsoleApplication2.OrdenService;
using ConsoleApplication2.ProductosService;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApplication2.Class
{
    public  class Servicio
    {
        protected string _nombreTienda;
        protected string _workspace = "Z:\\";
        protected string fullMonthName = String.Empty;
        protected OrderInfoSoapClient client;
        protected productioncallsSoapClient client_production;
        protected AuthHeaderOrders _conexion;
        protected AuthHeaderOrder _parametroOrden;
        public string condiciones;
        protected string agente;
        protected double? agenteComision;
        protected bool esFacturacionPedido;
        protected Cte Cliente;

        

        public bool esTransferencia=false;

        

        public int IdTienda() => this._conexion.CompanyID;
        public Servicio()
        {
            this.client_production = new productioncallsSoapClient();
            this.condiciones = "";
            this.client = new OrderInfoSoapClient();
            //var time = new TimeSpan(1,0, 0,0);
            //this.client.Endpoint.Binding.CloseTimeout = time;
            //this.client.Endpoint.Binding.OpenTimeout = time;
            //this.client.Endpoint.Binding.ReceiveTimeout = time;
            //this.client.Endpoint.Binding.SendTimeout = time;

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            this.client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindBySubjectName, "Starfield Root Certificate Authority");
            this.client_production.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindBySubjectName, "Starfield Root Certificate Authority");
        }
        protected void CargarDetalleCliente(string cte)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            this.Cliente = clienteDAO.ObtenerDetalle(cte);
            if (this.Cliente == null)
            {
                this.Cliente = new Cte()
                {
                    Concepto = "",
                    Agente = "",
                    Condicion = "",
                    DiasVencimiento = 0
                };
            }
            if (esTransferencia)
            {
                this.Cliente.Condicion = "Contado Transferencia";
            }
        }
        protected void RegistrarBitacoraPrintTemplate(List<OrdenDTO> orden)
        {
            OrdenDAO dao = new OrdenDAO();
            var ordenes = orden.GroupBy(x =>new{ x.OrdenId })
                               .Select(g => new { ordenID = g.Key.OrdenId,
                                                items = g.Count(),
                                                detalle = g.ToList().First().Detalle })
                                .ToList();
            try
            {
                ordenes.ForEach(_orden=>
                {
                    OrdenArchivos dto = new OrdenArchivos()
                    {
                        detalle = _orden.detalle,
                        ordenID = _orden.ordenID.ToString(),
                        plantillasDescargadas = (byte)orden.ToList().Count(),                         
                         tienda= this._nombreTienda,
                         fecha_descarga= DateTime.Now                        
                    };
                    dao.registrarOrdenconArchivos(dto);
                });
                
            }catch(Exception e)
            {
                Console.WriteLine("error en la insercion de la bitacora");
            }
           


        }
        protected virtual List<OrdenDTO> GetOrdenesConArchivos()
        {
            //is._conexion.StartDate = DateTime.Today.AddDays(-9);
            //Console.WriteLine(this._conexion.EndDate);
            List<OrdenDTO> _ordenes = new List<OrdenDTO>();
            Console.WriteLine("Ordenes desde  " + this._conexion.StartDate);
            Console.WriteLine("hasta " + this._conexion.EndDate);
            
            var data = this.client.GetOrdersWithProductionFiles(this._conexion);
            
            Console.WriteLine(data.Count);

            try
            {
                var orders = data.Orders.SelectMany(
                  orden => orden.Jobs
                  .Where(job => job.ProductType == "Blocks")
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
                       CustomData = job.CustomDatas.ToList(),
                       Detalle = orden.ToXElement<AuthReturnOrderWithProductionFiles>()
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
        protected void DownloadFile(string origenArchivo, string destinoCarpeta, string nombreArchivo)
        {
            WebClient client = new WebClient();
            try
            {
                Uri Uri = new Uri(origenArchivo);
                Download d = new Download();
                if (!Directory.Exists(destinoCarpeta))
                    Directory.CreateDirectory(destinoCarpeta);
                string destinoArchivo = destinoCarpeta + "\\" + nombreArchivo;
                if (File.Exists(destinoArchivo))
                {
                    Console.WriteLine($"El arhivo {destinoArchivo} ya existe no se descargara");
                }
                else
                {
                    Descarga f = new Descarga();
                    f.DownloadFile(origenArchivo, destinoArchivo);                    
                }                                              
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
        public void CambiarEstatusOrdenes(string ordenes, ePowerOrderStatus status)
        {
            AuthHeader header = null;
            AuthHeaderOrders credenciales = Credenciales.Kumon();
            header = new AuthHeader()
            {
                CompanyID = credenciales.CompanyID,
                MasterKey = credenciales.MasterKey,
                ProducerID = credenciales.ProducerID,
                Status = status,
                OrderIDs = ordenes
            };
               
                var result = client_production.ChangeStatus(header);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Se actualizo {result.IsSuccessful}");
            Console.WriteLine($" Mensaje  {result.Message}");
            Console.WriteLine("----------------------------------------------");
        }
        protected int[] GetListaOrdenes(OrderStatuses status)
        {
            this._conexion.OrderStatus = status;
            int[] ordenes_id = this.client.GetOrdersList(_conexion).OrdersList;
            return ordenes_id;
        }
        public void RegistrarPrefacturacionIntelisis(
            bool esFacturacionPedido = false,
            int? idOrden = null,
            double? agenteComision= null,
            OrderStatuses? status= OrderStatuses.Pending
            )
        {
            this.esFacturacionPedido = esFacturacionPedido;            
            this.agenteComision = agenteComision;            
            if (idOrden.HasValue)
            {
                this.ProcesarOrden(idOrden.Value);
                return;
            }            
            int[] ordenes_id = this.GetListaOrdenes(status.Value);
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
        protected virtual bool CumplePoliticas(AuthReturnOrder  result)
        {
            return true;
        }
        protected bool esFacturable(AuthReturnOrder result)
        {
            
            if (result.Order.TotalPrice > 0 && (result.Order.BillingMethodName.Contains("Convenio CIE") || result.Order.BillingMethodID==5845 || result.Order.BillingMethodID == 5708))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"{result.Order.OrderID} No se registra porque NO cuenta con el método de pago Convenio CIE");
                return false;
            }
            
        }
        protected void registrarEnBitacoraFacturacion(AuthReturnOrder result)
        {
            Order orden = result.Order;
            XElement detalleOrdenXML = result.ToXElement<AuthReturnOrder>();
            OrdenFacturacionDAO ordenFacturacionDAO = new OrdenFacturacionDAO();
            if (!ordenFacturacionDAO.Existe(orden.OrderID))
            {
                ordenFacturacionDAO.Registrar(new OrdenFacturacion()
                {
                    numero_orden = orden.OrderID,
                    fecha_registro = DateTime.Now,
                    Importe = Decimal.Parse((orden.TotalPrice + orden.ShippingPrice).ToString()),
                    tienda = this._nombreTienda,
                    detalle= detalleOrdenXML
                });
            }
        }

        private bool Existe(string orden)
        {
            VentaDAO ventaDAO = new VentaDAO();
            return ventaDAO.Buscar(orden);          
        }
        protected  void ProcesarOrden(int idOrden)
        {
            this._parametroOrden = new AuthHeaderOrder()
            {
                CompanyID = this._conexion.CompanyID,
                MasterKey = this._conexion.MasterKey,
                OrderID = idOrden,
                ProducerID = this._conexion.ProducerID,
                Username = this._conexion.Username
            };
           
                      
            AuthReturnOrder result = this.client.GetOrder(this._parametroOrden);

                   
            Order orden = result.Order;
            Job[] jobs = result.Jobs;
            VentaDAO venta_dao = new VentaDAO();

            if (this.esFacturacionPedido 
                && !orden.BillingMethodName.Contains("Facturación por pedido"))
            {
                Console.WriteLine($"La orden {orden.OrderID} no es Facturacion por pedido");
                return;
            }

            //bool existeVenta = venta_dao.Buscar(orden.OrderID.ToString());
            bool existeVenta =this.Existe(orden.OrderID.ToString());
            if (existeVenta)
            {
                Console.WriteLine("La orden {0} ya existe", orden.OrderID.ToString());
                return;
            }
            ClienteDAO clienteDAO = new ClienteDAO();
            CtoCampoExtra cte = clienteDAO.BuscarClienteCampoExtra(orden.WebUserID.ToString());
            string nombreContacto = string.Empty;


           
            string clave = String.Empty;
            switch (this._nombreTienda)
            {
                case "kfc":
                    cte = new CtoCampoExtra();
                    cte.Clave = "18933";
                    clave = cte.Clave;
                    CteCto contacto = clienteDAO.BuscarContacto(orden.WebUserID);
                    if (contacto != null)
                        nombreContacto = contacto.Nombre;
                    this.Cliente = new Cte()
                    {
                        Concepto = "SAAM KFC",
                        Agente = "C.R.C",
                        Condicion = "60 DIAS",
                        DiasVencimiento = 60,

                    };
                    break;
                case "lsm":                    
                    clave = cte != null ? cte.Clave : "23565";
                    this.CargarDetalleCliente(clave);
                    break;

                default:
                    clave = cte != null ? cte.Clave : "16776";
                    this.CargarDetalleCliente(clave);
                    break;
            }                       
            DateTime time = DateTime.Now;
            DateTime timeVencimiento = time.AddDays(this.Cliente.DiasVencimiento);

            #region RegistrarEnBitacoraFacturacion
            this.registrarEnBitacoraFacturacion(result);
            #endregion

            #region Llenado de Venta
            Venta venta = new Venta()
            {
                Empresa = "LITO",
                Contacto = orden.WebUserID,
                ContactoNombre = nombreContacto.Length > 0 ? nombreContacto : null,
                Mov = "Factura Electronica",
                FechaEmision = new DateTime(time.Year, time.Month, time.Day),
                //Concepto = $"SAAM {this._nombreTienda.ToUpper()}",
                Concepto = this.Cliente.Concepto,
                Moneda = "Pesos",
                TipoCambio = 1.0,
                Usuario = "MTOVAR",
                Referencia = orden.OrderID.ToString(),
                OrdenCompra = "",
                Estatus = "SINAFECTAR",
                Cliente = clave,
                Almacen = "AL PT",                
                Observaciones = this.Cliente.Concepto,                
                Condicion = this.esTransferencia ? "Contado Transferencia" : this.Cliente.Condicion,                
                Vencimiento = new DateTime(timeVencimiento.Year, timeVencimiento.Month, timeVencimiento.Day),
                AgenteComision = agenteComision.HasValue ? agenteComision : null,
                Agente = this.Cliente.Agente,
                //Agente = this.agente, //"A.L.P.",
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
            
            if (this.Existe(orden.OrderID.ToString()))
            {
                Console.WriteLine("La orden {0} ya existe", orden.OrderID.ToString());
                return;
            }

            int id_venta = venta_dao.Insertar(venta);
            int x = 1;

            #region  Llenado de VentaD
            foreach (Job job in jobs)
            {

                int _cantidad = job.Quantity;
                if (job.Records > 1)
                {
                    _cantidad = job.Records;
                }
                VentaD ventad = new VentaD()
                {
                    ID = id_venta,
                    Renglon = x * 2048,
                    RenglonID = x,
                    SubCuenta  =$"P{x++}",
                    Cantidad = _cantidad,
                    Almacen = "AL PT",
                    Articulo = job.SKU,
                    Unidad = "pza",
                    Precio = (job.TotalPrice / _cantidad),
                    Impuesto1 = 16,
                    DescripcionExtra = job.JobName,
                    RenglonTipo = 'N'
                };
                venta_dao.InsertarDetalle(ventad);
            }
            #endregion

            #region Costo por pedido cuando aplique 
            //if (orden.OrderFee > 0)
            //{
            //    VentaD detalleEnvio = new VentaD()
            //    {
            //        ID = id_venta,
            //        DescripcionExtra = "Costo por pedido",
            //        RenglonID = x,
            //        RenglonTipo = 'N',
            //        Renglon = x * 2048,
            //        SubCuenta = $"P{x++}",
            //        Articulo = "CostoPedido",
            //        Precio =  Math.Round(orden.OrderFee, 2),
            //        Cantidad = 1,
            //        Impuesto1 = 16,
            //        Unidad = "Servicio",
            //        Almacen = "AL PT"
            //    };
            //    venta_dao.InsertarDetalle(detalleEnvio);
            //}
            #endregion

            #region Envio
            if (orden.ShippingPrice > 0)
            {
                VentaD detalleEnvio = new VentaD()
                {
                    ID = id_venta,
                    DescripcionExtra = "Gastos de Envio",
                    RenglonID = x,
                    RenglonTipo = 'N',
                    Renglon = x * 2048,
                    SubCuenta = $"P{x}",
                    Articulo = "EN",
                    Precio = Math.Round(orden.ShippingPrice, 2),
                    Cantidad = 1,
                    Impuesto1 = 16,
                    Unidad = "Servicio",
                    Almacen = "AL PT"
                };
                venta_dao.InsertarDetalle(detalleEnvio);
            }
            #endregion

            DAO.EstadodeCuentaSAAM.BitacoraDAO bitacoraDAO = new DAO.EstadodeCuentaSAAM.BitacoraDAO();
            bitacoraDAO.Insertar(new BitacoraIntelisis()
            {
                fecha_registro= DateTime.Now,
                numero_orden= orden.OrderID,                               
                total= Decimal.Parse((orden.TotalPrice + orden.ShippingPrice).ToString())
            });
            //this.CambioStatusOrden(orden.OrderID.ToString());
           
        }


        private void CambioStatusOrden(string numero_orden)
        {
            #region Cambio de estatus en KFC
            if (this._nombreTienda == "kfc")
            {
                this.CambiarEstatusOrdenes(numero_orden, ePowerOrderStatus.ToProduction);
            }
            #endregion
        }

        public void LlenarInfoGoePower()
        {
            this._conexion.StartDate = DateTime.Today.AddDays(-320);
            //this._conexion.StartDate = new DateTime(2018, 1, 1);
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
                ClienteDAO clienteDAO = new ClienteDAO();

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

                var cte = clienteDAO.BuscarClienteCampoExtra(order.WebUserID.ToString());
                XElement detalleOrdenXML;
                // Convertir La respuesta en XML para Guardarlo en la Base de Datos
                try
                {
                     detalleOrdenXML = result.ToXElement<AuthReturnOrder>();
                }
                catch(Exception e)
                {
                    detalleOrdenXML = null;
                }
                

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
                    Total = (float)order.TotalPrice,
                    clienteIntelisis = cte != null ? cte.Clave : "16776",
                    detalle=detalleOrdenXML                   
                };


                var o = ordenDao.existe(__ordenID);
                if (o != null)
                {
                    if (o.status != order.OrderStatus)
                    {
                        Console.WriteLine("Es diferente");
                        Console.WriteLine($"La orden {orden} cambio de  {o.status}  a {order.OrderStatus}");
                        ordenDao.Actualiza(ordendDto);
                    }
                    continue;
                }

                                           
                Console.WriteLine($"Insertando la orden {orden} de la tienda {this._nombreTienda}");
                ordenDao.Agregar(ordendDto);
                

                foreach (Job job in jobs)
                {
                    Console.WriteLine($"Insertando el job {job.JobID}");
                    int cantidad = job.Quantity;
                    int _cantidad = cantidad;
                    if (job.Records > 1)
                    {
                        _cantidad = job.Records;
                    }
                    Item itemDto = new Item()
                    {
                        cantidad = job.Quantity,
                        descripcion = job.JobName,
                        ordenID = __ordenID,
                        precio = (job.Price / _cantidad),
                        records = job.Records,
                        sku = job.SKU,
                        setSize = job.SetSize

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
                        ordenID = __ordenID,
                        setSize = 1,
                        records = 1
                    };
                    itemDao.Agregar(envio);
                }





            }

        }    
        public AuthReturnOrder TraerInfo(int idOrden)
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
            //result.Jobs.ToList().ForEach(job =>
            //{
            //    var j=this.client.GetJob(new AuthHeaderJob()
            //    {
            //         CompanyID= this._conexion.CompanyID,
            //         JobID= job.JobID,
            //         MasterKey= this._conexion.MasterKey,
            //         ProducerID= this._conexion.ProducerID,
            //         Username= this._conexion.Username
            //    });
            //    if (j.JobExtras.Length > 0)
            //    {
            //        Console.WriteLine("Tiene configuracion extra");
            //    }
            //    else
            //    {
            //        Console.WriteLine("No tiene configuracion extra");
            //    }
                
            //});
            

            return result;
            //Order orden = result.Order;
            //Job[] jobs = result.Jobs;
            //Console.WriteLine($"Limite de credito:{result.UserProfile.CreditLimit}");
            //Console.WriteLine($"Own Credit:{result.UserProfile.OwnCredit}");
            //Console.WriteLine($"Billing Method {result.Order.BillingMethodName}");
            

            //jobs.ToList().ForEach(x => {
            //    Console.WriteLine(x.Quantity +"     " +x.SKU +"   "+x.JobName);
            //    });


        }
        






    }
}
