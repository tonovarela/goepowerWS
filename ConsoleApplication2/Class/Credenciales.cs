using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public  class Credenciales
    {
        private const int _PRODUCERID=100;
        public static AuthHeaderOrders Michelin()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "21f5e24f-4423-4f9a-864f-55106d2f8494",
                CompanyID = 4727,
                PartnerID = 4727,
                Username = "sistemasmichelin",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-5),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Honda()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "24d2b3e8-83fe-4cf5-9ffd-f711696b54f1",
                CompanyID = 1138,
                PartnerID = 1138,
                Username = "sistemashonda",
                OrderStatus = OrderStatuses.Pending,
                StartDate = DateTime.Today.AddDays(-6),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Kumon()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "448ca106-0fc2-4913-bc11-c1656d3c73f7",
                CompanyID = 5104,
                PartnerID = 5104,
                Username = "sistemaskumon",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-5),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders CirculoK()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "5701b920-087f-4a87-8f22-6194d721ee04",
                CompanyID = 5100,
                PartnerID = 5100,
                Username = "sistemascirclek",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-5),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Starbucks()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "0ee63615-cf72-4645-84d7-f2a21e6ec66a",
                CompanyID = 1582,
                PartnerID = 1582,
                Username = "sistemasstarbucks",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-5),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Volkswagen()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "fc8011de-2f99-442c-ad03-0b248a8b926b",
                CompanyID = 1181,
                PartnerID = 1181,
                Username = "sistemasvolkswagen",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-5),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Acura()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "b3c4adb1-cd74-4530-b427-4b12f99b39c9",
                CompanyID = 4373,
                PartnerID = 4373,
                Username = "sistemasacura",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-5),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Seat()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "876e8da9-b289-4516-b998-9e3801717eaf",
                CompanyID = 1299,
                PartnerID = 1299,
                Username = "sistemasseat",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-5),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Sherwin()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "c3b91791-a48f-44fb-b75d-25b08a090297",
                CompanyID = 4996,
                PartnerID = 4996,
                Username = "sistemassherwin",
                OrderStatus = OrderStatuses.Released,
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }








    }
}
