using ConsoleApplication2.Class;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication2
{
    public class ServicioSherwin : Servicio
    {

        public ServicioSherwin()
        {
            this._nombreTienda = "sherwin";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Sherwin();
        }

        
        public override void ProcesarOrden(int idOrden)
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
                Agente = "U.D.P.",
                Sucursal = 0,
                SucursalOrigen = 0,
                Atencion = "",
                AtencionTelefono = "",
                Clase = "",
                Directo = true,
                OrdenID = orden.OrderID.ToString()

            };
            #endregion

            //int id_venta = venta_dao.Insertar(venta);
            //int x = 1;

            //#region  Llenado de VentaD
            //foreach (Job job in jobs)
            //{
            //    int cantidad = job.Quantity == job.Records ? job.Quantity : job.Records;
            //    VentaD ventad = new VentaD()
            //    {
            //        ID = id_venta,
            //        Renglon = x * 2048,
            //        RenglonID = x++,
            //        ABC = "2",
            //        Cantidad = cantidad,
            //        Almacen = "AL PT",
            //        Articulo = job.SKU,
            //        Unidad = "pza",
            //        Precio = (job.TotalPrice / cantidad),
            //        Impuesto1 = 16,
            //        DescripcionExtra = job.JobName,
            //        RenglonTipo = 'N'
            //    };
            //    venta_dao.InsertarDetalle(ventad);
            //}
            //#endregion

            //#region Envio
            //if (orden.ShippingPrice > 0)
            //{
            //    VentaD detalleEnvio = new VentaD()
            //    {
            //        ID = id_venta,
            //        DescripcionExtra = "Gastos de Envio",
            //        ABC = "5",
            //        RenglonID = x,
            //        RenglonTipo = 'N',
            //        Renglon = x * 2048,
            //        Articulo = "EN",
            //        Precio = orden.ShippingPrice,
            //        Cantidad = 1,
            //        Impuesto1 = 16,
            //        Unidad = "Servicio",
            //        Almacen = "AL PT"
            //    };
            //    venta_dao.InsertarDetalle(detalleEnvio);
            //}
            //#endregion

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
                int x = Int32.Parse(orden.SKU.Substring(orden.SKU.Length - 2, 2));                             
                string _ordenFolder = $"{this._workspace}\\{orden.OrdenId}";
                orden.Imprimir();

                FileExcel cfile = new FileExcel();
                orden.CustomData.ForEach( custom=>cfile.AgregarRow(custom.Name,custom.Value.Replace("<nextline>"," ")) );                               
                this.DownloadFile(orden.FilePdfURL, _ordenFolder, $"{orden.NombreArchivo}.pdf");                
                cfile.SalvarExcel($"{_ordenFolder}\\{orden.NombreArchivo}.xlsx");
              
            }
            );



            
    


        }

    }
}
