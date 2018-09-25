using ConsoleApplication2.Class;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.OrdenService;
using System;
using System.Linq;

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


        protected override void ProcesarOrden(int idOrden)
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

            //Solo procesa las ordenes que contengan  el SKU HI16163  
            if (!jobs.ToList().Any(z => z.SKU.Contains("HI16163")))
            {
                Console.WriteLine($"La orden {idOrden} de la tienda {this._nombreTienda} no se proceso, ya que no contiene en su detalle de venta el SKU HI16163 ");
                return;
            }
            
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
                Condicion = this.condiciones,
                Vencimiento = new DateTime(timeVencimiento.Year, timeVencimiento.Month, timeVencimiento.Day),
                Agente = this.agente, 
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
                    RenglonID = x++,

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



    }
}
