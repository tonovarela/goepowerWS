using ConsoleApplication2.Class;
using ConsoleApplication2.DAO.CuponesSAAM;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.OrdenService;
using ConsoleApplication2.ViewModel.Goepower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job = ConsoleApplication2.OrdenService.Job;
using Order = ConsoleApplication2.OrdenService.Order;

namespace ConsoleApplication2
{
    public class ServicioKIA:Servicio
    {

        public ServicioKIA()
        {
            this._nombreTienda = "KIA";
            this._conexion = Credenciales.KIA();
            this._parametroOrden = new AuthHeaderOrder()
            {
                MasterKey = _conexion.MasterKey,
                CompanyID = _conexion.CompanyID,
                Username = _conexion.Username,
                ProducerID = _conexion.ProducerID
            };

        }
        //protected new void ProcesarOrden(int idOrden)
        //{
        //    this._parametroOrden = new AuthHeaderOrder()
        //    {
        //        CompanyID = this._conexion.CompanyID,
        //        MasterKey = this._conexion.MasterKey,
        //        OrderID = idOrden,
        //        ProducerID = this._conexion.ProducerID,
        //        Username = this._conexion.Username
        //    };
        //    AuthReturnOrder result = this.client.GetOrder(this._parametroOrden);
        //    //if (!this.esFacturable(result))
        //    //{
        //    //    return;
        //    //}
        //    //if (!this.CumplePoliticas(result))
        //    //{
        //    //    return;s
        //    //}
        //    Order orden = result.Order;
        //    Job[] jobs = result.Jobs;
        //    VentaDAO ventaDAO = new VentaDAO();
        //    bool existeVenta = ventaDAO.Buscar(orden.OrderID.ToString());
        //    if (existeVenta)
        //    {
        //        Console.WriteLine("La orden {0} ya esta registrada en Intelisis", orden.OrderID.ToString());
        //        return;
        //    }
        //    if (orden.TotalPrice <= 0)
        //    {
        //        Console.WriteLine("Se omite el registro en Intelisis ya que el Importe total de la orden {0} es 0  ", orden.OrderID.ToString());
        //        return;
        //    }
        //    DAO.Lito.ClienteDAO cliente_dao = new DAO.Lito.ClienteDAO();
        //    CtoCampoExtra cte = cliente_dao.BuscarClienteCampoExtra(orden.WebUserID.ToString());
        //    this.CargarDetalleCliente(cte != null ? cte.Clave : "16776");

        //    DateTime time = DateTime.Now;
        //    DateTime timeVencimiento = time.AddDays(this.Cliente.DiasVencimiento);
        //    #region RegistrarEnBitacoraFacturacion
        //    //this.registrarEnBitacoraFacturacion(result);
        //    #endregion
        //    #region Llenado de Venta
        //    Venta venta = new Venta()
        //    {
        //        Empresa = "LITO",
        //        Mov = "Factura Electronica",
        //        FechaEmision = new DateTime(time.Year, time.Month, time.Day),
        //        //Concepto = $"SAAM {this._nombreTienda.ToUpper()}",
        //        Concepto = this.Cliente.Concepto,
        //        Moneda = "Pesos",
        //        TipoCambio = 1.0,
        //        Usuario = "MTOVAR",
        //        Referencia = orden.OrderID.ToString(),
        //        OrdenCompra = "",
        //        Estatus = "SINAFECTAR",
        //        Cliente = cte != null ? cte.Clave : "16776",
        //        Almacen = "AL PT",
        //        //Observaciones = $"SAAM {this._nombreTienda.ToUpper()}",
        //        Observaciones = this.Cliente.Concepto,
        //        //Condicion = this.condiciones,
        //        Condicion = this.Cliente.Condicion,
        //        Vencimiento = new DateTime(timeVencimiento.Year, timeVencimiento.Month, timeVencimiento.Day),
        //        AgenteComision = agenteComision.HasValue ? agenteComision : null,
        //        Agente = this.Cliente.Agente,
        //        //Agente = this.agente, //"A.L.P.",
        //        //Importe = Decimal.Parse((orden.TotalPrice + orden.ShippingPrice).ToString()),
        //        //Impuestos = Decimal.Parse(orden.Tax1.ToString()),
        //        Sucursal = 0,
        //        SucursalOrigen = 0,
        //        Atencion = "",
        //        AtencionTelefono = "",
        //        Clase = "",
        //        Directo = true,
        //        OrdenID = orden.OrderID.ToString()

        //    };
        //    #endregion
        //    int id_venta = ventaDAO.Insertar(venta);
        //    int x = 1;
        //    #region  Llenado de VentaD
        //    List<VentaD> _jobs = new List<VentaD>();
        //    foreach (Job job in jobs)
        //    {
        //        int _cantidad = job.Quantity;
        //        //if (job.Records > 1)
        //        //{
        //        //    _cantidad = job.Records;
        //        //}                
        //        VentaD ventad = new VentaD()
        //        {
        //            ID = id_venta,
        //            Renglon = x * 2048,
        //            RenglonID = x,
        //            SubCuenta = $"P{x++}",
        //            Cantidad = _cantidad,
        //            Almacen = "AL PT",
        //            Articulo = job.SKU,
        //            Unidad = "pza",
        //            Precio = (job.TotalPrice / _cantidad),
        //            Impuesto1 = 16,
        //            DescripcionExtra = job.JobName,
        //            RenglonTipo = 'N'
        //        };
        //        _jobs.Add(ventad);
        //    }
        //    #endregion            
        //    #region Envio
        //    if (orden.ShippingPrice > 0)
        //    {
        //        VentaD detalleEnvio = new VentaD()
        //        {
        //            ID = id_venta,
        //            DescripcionExtra = "Gastos de Envio",
        //            RenglonID = x,
        //            RenglonTipo = 'N',
        //            Renglon = x * 2048,
        //            SubCuenta = $"P{x}",
        //            Articulo = "EN",
        //            Precio = Math.Round(orden.ShippingPrice, 2),
        //            Cantidad = 1,
        //            Impuesto1 = 16,
        //            Unidad = "Servicio",
        //            Almacen = "AL PT"
        //        };
        //        _jobs.Add(detalleEnvio);
        //    }
        //    #endregion
        //    //if (orden.CouponAmount > 0)
        //    //{
        //    //    ServiceRest serviceRest = new ServiceRest();
        //    //    ResponseGPViewModel responseGPViewModel = serviceRest.ObtenerDetalleOrden(orden.OrderID);
        //    //    if (responseGPViewModel != null)
        //    //    {
        //    //        List<Coupon> cuponesUsadosEnOrden = responseGPViewModel.coupons;
        //    //        CuponDAO cuponDAO = new CuponDAO();
        //    //        cuponesUsadosEnOrden.ForEach(t =>
        //    //        {
        //    //            Cupon cuponInicial = cuponDAO.obtenerSKU(t.CouponName);
        //    //            if (cuponInicial != null)
        //    //            {
        //    //                VentaD v = _jobs.Where(g => g.Articulo == cuponInicial.sku && g.Cantidad > 0).FirstOrDefault();
        //    //                if (v != null)
        //    //                {
        //    //                    v.Cantidad -= 1;                                                                
        //    //                }
        //    //            }
        //    //        });
        //    //    }
        //    //}
        //    Console.WriteLine("-----------------------------------------------------------------------------------------");
        //    //Console.WriteLine($" [Orden]:{ orden.OrderID} [Total]:{orden.TotalPrice}  [MontoTotalCupones]:{orden.CouponAmount}");
        //    _jobs.Where(g => g.Cantidad > 0) //Registra todos los job cuyo Precio sean mayores a 0
        //             .ToList()
        //             .ForEach(j =>
        //             {
        //                 Console.WriteLine($"  [sku]:{j.Articulo}  Cantidad:{j.Cantidad}  [Descripcion]:{j.DescripcionExtra}  [Precio]:{j.Precio} ");
        //                 ventaDAO.InsertarDetalle(j);
        //             });
        //    Console.WriteLine("-----------------------------------------------------------------------------------------");
        //}

        //public new void RegistrarPrefacturacionIntelisis(string condiciones, string agente, int? idOrden = null, double? agenteComision = null)
        //{
        //    this.condiciones = condiciones;
        //    this.agente = agente;
        //    this.agenteComision = agenteComision;

        //    if (idOrden.HasValue)
        //    {
        //        this.ProcesarOrden(idOrden.Value);
        //        return;
        //    }
        //    int[] ordenes_id = this.GetListaOrdenes(OrderStatuses.Pending);
        //    if (ordenes_id == null)
        //    {
        //        Console.WriteLine($"No hay ordenes por procesar en la tienda {this._nombreTienda}");
        //        return;
        //    }
        //    Console.WriteLine($"Total de ordenes {ordenes_id.Length} en la tienda {this._nombreTienda}");
        //    foreach (int orden_id in ordenes_id)
        //    {
        //        Console.WriteLine("Procesando la orden " + orden_id);
        //        this.ProcesarOrden(orden_id);
        //    }


        //}
    }
}
