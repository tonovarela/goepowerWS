using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApplication2
{
    public  class ServicioStarBucks:Servicio
    {


        public ServicioStarBucks()
        {
            this._nombreTienda = "starbucks";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Starbucks();
            
        }

        public string CrearOrden(OrdenCargaDTO orden)
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
                Phone = orden.Phone,
                Email = orden.Email,
                FirstName = orden.Firstname,
                LastName = orden.Lastname,
                Items = items.ToArray(),
                PostalCode = orden.PostalCode,
                Country = "MX",
                City = orden.City,
                BillingMethodID = orden.Id_BillingMethod,
                ShippingPrice=Decimal.ToDouble(orden.ShipingPrice),
                SendNotifications = false
            });

        
            return response1.OrderID.ToString() ;
            

        }

        
    }
}
