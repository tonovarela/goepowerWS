using ConsoleApplication2.Class;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.DAO.ReporteSAAM;
using ConsoleApplication2.Entity;
using ConsoleApplication2.OrdenService;
using ConsoleApplication2.ProductosService;
using ConsoleApplication2.ViewModel.ReporteSAAM;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class ServicioHonda : Servicio
    {

        public ServicioHonda()
        {
            this._nombreTienda = "honda";
            this._conexion = Credenciales.Honda();
            this._parametroOrden = new AuthHeaderOrder()
            {
                MasterKey = _conexion.MasterKey,
                CompanyID = _conexion.CompanyID,
                Username = _conexion.Username,
                ProducerID = _conexion.ProducerID
            };

            
        }
        
        //public void RegistrarProductosEnReporteSAAM()
        //{
        //    List<ProductoViewModel> lproductos = new List<ProductoViewModel>();
        //    var clientProductos = new InformationAPISoapClient();
        //    var productos=clientProductos.CompanyProductsByCompanyID(new APILogin()
        //    {
        //        MasterKey = this._conexion.MasterKey,
        //        CompanyID = this._conexion.CompanyID,
        //        ProducerID = this._conexion.ProducerID
        //    }, this._conexion.CompanyID,true);
        //    productos.Products.ToList().ForEach(producto =>
        //    {
        //        var p = new ProductoViewModel()
        //        {
        //            Descripcion = producto.Description,
        //            Inventario = producto.Inventory,
        //            Miniatura = producto.ThumbnailSmall,
        //            Proyeccion = producto.NoteToPrinter,
        //            PiezasPorPaquete= producto.SetSize,
        //            Nombre=producto.ProductName,
        //            SKU= producto.SKU,
        //            IdProducto=producto.ProductID
        //        };
        //        lproductos.Add(p);                
        //    });
        //    ProductoDAO productoDAO = new ProductoDAO();
        //    productoDAO.EliminarTodos();
        //    lproductos.Where(p=>p.SKU.Length>0 && p.SKU !="HI")
        //              .Select(p=>p).ToList()
        //              .ForEach(x=>
        //              {
        //                        productoDAO.Agregar(new ProductoSAAM()
        //                        {
        //                            id_tienda=1,
        //                            descripcion=x.Descripcion,
        //                            img_miniatura=x.Miniatura,
        //                            inventario= x.Inventario,
        //                            nombre=x.Nombre,
        //                            piezas_paquete=x.PiezasPorPaquete,
        //                            proyeccion=x.Proyeccion,
        //                            sku=x.SKU ,
        //                            id_producto=x.IdProducto,
                                    
        //                        });
        //               });
            

        //}

        //public void RegistrarOrdenesEnReporteSAAM()
        //{
        //    this._conexion.StartDate = new DateTime(2018,1,1);            
        //    //this._conexion.StartDate = DateTime.Now.ad(-5);            
        //    var ordenes = this.GetListaOrdenes(OrderStatuses.All);
        //    List<OrdenSAAM> ordens = new List<OrdenSAAM>();
        //    List<ItemSAAM> items = new List<ItemSAAM>();
        //    OrdenDAO ordenDAO = new OrdenDAO();
        //    ItemDAO itemDAO = new ItemDAO();
        //    DAO.ReporteSAAM.ClienteDAO clienteDAO = new DAO.ReporteSAAM.ClienteDAO();
        //    if (ordenes != null)
        //    {
        //        itemDAO.BorrarTodo();
        //        ordenDAO.BorrarTodo();
        //        ordenes.ToList().ForEach(numero_orden =>
        //        {
        //            AuthReturnOrder result = this.TraerInfo(numero_orden);
        //            Order orden = result.Order;
        //            OrdenSAAM ordenSAAM = new OrdenSAAM();
                    
        //            ordenSAAM.numero_orden = orden.OrderID;
        //            ordenSAAM.total = (decimal)orden.TotalPrice;
        //            ordenSAAM.esCampana = orden.BillingMethodName.ToLowerInvariant().Contains("campaña");
        //            ordenSAAM.BillingMethod = orden.BillingMethodName;
        //            ordenSAAM.estatus = orden.OrderStatus;

        //            ordenSAAM.OrderDate = orden.OrderDate;
        //            ordenSAAM.BillingDate = orden.BillingDate;
        //            ordenSAAM.CompleteDate = orden.CompletedDate;
        //            ordenSAAM.GateAction = orden.GatekeeperActionDate;
        //            ordenSAAM.ReleaseDate = orden.ReleaseDate;
        //            ordenSAAM.id_cliente = result.UserProfile.WebUserID;
                                            
        //            // ordens.Add(ordenSAAM);
        //            Console.WriteLine($"Registrando la orden {ordenSAAM.numero_orden}");
        //            if (!clienteDAO.Existe(result.UserProfile.WebUserID))
        //            {
        //                var cliente = result.UserProfile;
        //                clienteDAO.Registrar(new ClienteSAAM()
        //                {
        //                    concesionaria = cliente.FirstName + cliente.LastName,
        //                    fullname = cliente.FullName,
        //                    codigo_postal = cliente.PostalCode,
        //                    direccion = cliente.Address1,
        //                    id_cliente = cliente.WebUserID,
        //                    credit_limit = (decimal)cliente.CreditLimit,
        //                    //numero_dealer = Int32.Parse(cliente.CompanyName)

        //                });
        //            }
        //            ordenDAO.Agregar(ordenSAAM);
                    


        //            result.Jobs.ToList().ForEach(job =>
        //            {
        //                ItemSAAM item = new ItemSAAM();
        //                item.id_producto = job.ProductID;
        //                item.cantidad = job.Quantity;
        //                item.id_item = job.JobID;
        //                item.ImporteTotal =(decimal)job.TotalPrice;
        //                item.setSize = job.SetSize;
        //                item.nombre = job.JobName;
        //                item.records = job.Records;
        //                item.numero_orden = orden.OrderID;
        //                itemDAO.Registrar(item);
        //                //items.Add(item);
        //            });

        //        });

                
                

        //        //ordens.ForEach(x => ordenDAO.Agregar(x));
        //        //items.ForEach(x => itemDAO.Registrar(x));
        //        Console.WriteLine($"Total de ordenes:  {ordens.Count}");
        //        Console.WriteLine($"Total de Jobs:{items.Count}");

        //    }
            
        //}


        public void RegistrarOrdenesEnEstadoCuentaSAAM(int id_tienda) {
            this._conexion.StartDate = new DateTime(2019, 05, 14, 15,0,0);            
            var ordenes = this.GetListaOrdenes(OrderStatuses.All);            
            ordenes.ToList().ForEach(numero_orden =>
            {
                AuthReturnOrder result = this.TraerInfo(numero_orden);
                Order orden = result.Order;
                OrdenEstadoCuenta ordenSAAM = new OrdenEstadoCuenta();                              
                ordenSAAM.numero_orden = orden.OrderID;
                ordenSAAM.id_tienda = id_tienda;
                ordenSAAM.total = (decimal)orden.TotalPrice;
                ordenSAAM.esCampana = orden.BillingMethodName.ToLowerInvariant().Contains("campaña");
                ordenSAAM.BillingMethod = orden.BillingMethodName;
                ordenSAAM.estatus = orden.OrderStatus;
                ordenSAAM.OrderDate = orden.OrderDate;
                ordenSAAM.BillingDate = orden.BillingDate;                
                ordenSAAM.ReleaseDate = orden.ReleaseDate;
                ordenSAAM.webUserID = result.UserProfile.WebUserID;
                ordenSAAM.fecha_registro = DateTime.Now;
                
                DAO.EstadodeCuentaSAAM.OrdenDAO ordenDAO = new DAO.EstadodeCuentaSAAM.OrdenDAO();                

               
                var _orden = ordenDAO.obtenerOrden(orden.OrderID);

                if (_orden != null && (_orden.estatus != ordenSAAM.estatus))
                {
                    Console.WriteLine($"Se actualiza la orden {_orden.numero_orden} ");
                    ordenDAO.ActualizarEstatus(_orden.numero_orden, ordenSAAM.estatus);
                }

                if (_orden ==null)
                {
                    Console.WriteLine($"Registrando la orden {ordenSAAM.numero_orden}");
                    ordenDAO.Agregar(ordenSAAM);
                }
               


            });




        }


        public async void enviarReporteCorporativo()
        {
            var client = new RestClient("http://192.168.2.209/");
            var request = new RestRequest("goepower/enviarReporteCorporativoAlerta", Method.GET);

            Task<IRestResponse> t = client.ExecuteGetTaskAsync(request);
            t.Wait();
            var restResponse = await t;
            Console.WriteLine(restResponse.Content);

            //client.ExecuteAsync(request, response => {
            //     Console.WriteLine(response.Content);
            //});
        }

        public async void enviarNotificacionSaldoporVencer()
        {

            var client = new RestClient("http://192.168.2.209/");
            var request = new RestRequest("goepower/enviarNotificacionSaldoPorVencer", Method.GET);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            Task<IRestResponse> t = client.ExecuteGetTaskAsync(request);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            t.Wait();
            var restResponse = await t;
            Console.WriteLine(restResponse.Content);            
        }

        protected new void ProcesarOrden(int idOrden)
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

            if (!this.esFacturable(result))
            {
                return;
            }
            //if (!this.CumplePoliticas(result))
            //{
            //    return;
            //}

            Order orden = result.Order;
            Job[] jobs = result.Jobs;
            VentaDAO venta_dao = new VentaDAO();

            bool existeVenta = venta_dao.Buscar(orden.OrderID.ToString());

            if (existeVenta)
            {
                Console.WriteLine("La orden {0} ya existe", orden.OrderID.ToString());
                return;
            }
            DAO.Lito.ClienteDAO cliente_dao = new DAO.Lito.ClienteDAO();
            //CteHonda cte = cliente_dao.BuscarCliente(orden.WebUserID.ToString());
            CtoCampoExtra cte = cliente_dao.BuscarClienteCampoExtra(orden.WebUserID.ToString());

                       
            this.CargarDetalleCliente(cte != null?cte.Clave: "16776");                           
            
            DateTime time = DateTime.Now;
            DateTime timeVencimiento = time.AddDays(this.Cliente.DiasVencimiento);

            #region RegistrarEnBitacoraFacturacion
            this.registrarEnBitacoraFacturacion(result);
            #endregion

            #region Llenado de Venta
            Venta venta = new Venta()
            {
                Empresa = "LITO",
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
                Cliente = cte != null ? cte.Clave : "16776",
                Almacen = "AL PT",
                //Observaciones = $"SAAM {this._nombreTienda.ToUpper()}",
                Observaciones = this.Cliente.Concepto,
                //Condicion = this.condiciones,
                Condicion= this.Cliente.Condicion,
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
                    SubCuenta = $"P{x++}",
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

        }

        public   void RegistrarPrefacturacioIntelisis(string condiciones, string agente, int? idOrden = null, double? agenteComision = null, OrderStatuses? status = OrderStatuses.Pending)
        {
            this.condiciones = condiciones;
            this.agente = agente;
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


        public void EnviarCorreosEstadoCuenta()
        {
            DAO.EstadodeCuentaSAAM.ClienteDAO clienteDAO = new DAO.EstadodeCuentaSAAM.ClienteDAO();
            List<int> webusers = clienteDAO.getPendientesMandarCorreo();
            Console.WriteLine(webusers.Count);
            foreach (var webUser in webusers)
            {
                try
                {
                    WebClient client = new WebClient();
                    string reply = client.DownloadString("http://192.168.2.209/goepower/enviarEstadoCuenta/" + webUser);
                    clienteDAO.marcarCorreoEnviado(webUser);
                    Console.WriteLine(reply);                                        
                }
                catch (Exception ex)
                {

                }
            }
            
        }

       
    }
}
