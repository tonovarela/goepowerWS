using ConsoleApplication2.Class;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Linq;

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


        //Lista de Webuser que paso Mario Escobedo
        private bool esWebUserExcluyente(int WebUserID)
        {

            List<int> _webUsersID = new List<int> { 249056,249171,249042,249086,249172,249220,249069,249073,249034,249096,249147,249275,249063,249193,249212,249148,249271,249221,249276,249149,255533,249277,249104,249150,249090,249194,249173,249222,249278,249245,249129,249028,249037,249110,249066,249084,249151,249130,249195,249223,249057,249058,249040,249197,249199,249048,249021,249044,249097,249152,249131,249247,249224,249132,249279,249198,249153,249174,249280,249248,249200,249081,249107,249125,249065,249175,249176,249146,249133,249201,249154,249049,249095,249091,249249,249126,249121,249202,249155,249192,255765,255762,249043,249105,249072,249177,249055,249074,249092,249203,249226,249225,249227,249228,249108,249204,249102,249098,249250,249229,249178,249205,249179,249230,249029,249111,249078,249101,249134,249244,249120,249103,249106,249075,249219,255526,249061,249062,249115,249051,249118,249070,249019,249079,249124,249127,249094,249087,249085,249022,249100,249116,249053,249033,255766,249123,249032,249030,249060,249089,249047,249013,249088,249093,249014,249206,249251,249214,249180,249156,249269,249181,249281,249232,249015,249031,249252,249253,249046,249012,249114,249076,249045,249161,249036,249255,249041,249282,249119,256163,249207,249256,249283,249135,249257,249167,249039,249268,249054,249170,215600,249254,249208,256036,249068,249196,249210,249209,249211,249233,249080,249182,249157,249112,249024,249183,249284,249136,249246,249184,249035,249137,249067,249266,249038,249158,249113,249109,249259,249138,249267,249234,249185,249186,249139,249270,249187,249159,249235,249231,249117,249027,249059,249285,249023,249026,249082,249071,249286,249128,249020,249215,249016,249213,249264,249236,249140,249064,249261,249237,249238,249239,249188,249216,249160,249272,249262,249052,249099,249240,249217,214862,249141,249083,249017,249189,249162,249163,249263,249265,249164,249241,249165,249166,249273,249218,249142,249143,249025,249242,249018,249260,249258,249274,249144,249168,249050,249145,249169,249122,249077,249190,249243,249191};
            return _webUsersID.Any(x => x == WebUserID);            
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

            //this._parametroOrden.OrderID = idOrden;          
            AuthReturnOrder result = this.client.GetOrder(this._parametroOrden);

            Order orden = result.Order;

            Job[] jobs = result.Jobs;
            VentaDAO venta_dao = new VentaDAO();

            bool existeVenta = venta_dao.Buscar(orden.OrderID.ToString());


            if (this.esWebUserExcluyente(orden.WebUserID))
            {
                Console.WriteLine($"El webUserID { orden.WebUserID} se excluye por la lista que paso Mario ");
                return;
            }

            if (existeVenta)
            {
                Console.WriteLine("La orden {0} ya existe", orden.OrderID.ToString());
                return;
            }



            ClienteDAO cliente_dao = new ClienteDAO();            
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
                    ABC = "2",
                    Cantidad = _cantidad,
                    Almacen = "AL PT",
                    Articulo = job.SKU,
                    Unidad = "pza",
                    Precio = (job.TotalPrice/_cantidad),                                       
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
                    ABC = "5",
                    RenglonID = x,
                    RenglonTipo = 'N',
                    Renglon = x * 2048,
                    Articulo = "EN",
                    Precio = Math.Round(orden.ShippingPrice,2),
                    Cantidad = 1,
                    Impuesto1 = 16,
                    Unidad = "Servicio",
                    Almacen = "AL PT"
                };
                venta_dao.InsertarDetalle(detalleEnvio);
            }
            #endregion

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
