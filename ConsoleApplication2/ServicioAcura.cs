using ConsoleApplication2.Class;
using ConsoleApplication2.OrdenService;
using System;
using System.Linq;

namespace ConsoleApplication2
{
    public class ServicioAcura:Servicio
    {


        public ServicioAcura()
        {
            this._nombreTienda = "acura";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Acura();

        }

        public void RegistrarOrdenesEnEstadoCuentaSAAM(int id_tienda)
        {
            this._conexion.StartDate = new DateTime(2019, 03, 14, 15, 0, 0);
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
