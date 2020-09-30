using ConsoleApplication2.Class;
using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class ServicioToyota :  Servicio
    {
        public ServicioToyota()
        {
            this._nombreTienda = "honda";
            this._conexion = Credenciales.Toyota();
            this._parametroOrden = new AuthHeaderOrder()
            {
                MasterKey = _conexion.MasterKey,
                CompanyID = _conexion.CompanyID,
                Username = _conexion.Username,
                ProducerID = _conexion.ProducerID
            };


        }


        public void RegistrarOrdenesEnEstadoCuentaSAAM(int id_tienda)
        {
            this._conexion.StartDate = new DateTime(2019, 05, 14, 15, 0, 0);
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

                if (_orden == null)
                {
                    Console.WriteLine($"Registrando la orden {ordenSAAM.numero_orden}");
                    ordenDAO.Agregar(ordenSAAM);
                }



            });




        }
    }
}
