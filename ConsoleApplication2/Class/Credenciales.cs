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
        private const int _DIAS = -20;
        public static AuthHeaderOrders Michelin()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "21f5e24f-4423-4f9a-864f-55106d2f8494",
                CompanyID = 4727,
                PartnerID = 4727,
                Username = "sistemasmichelin",
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }
        public static AuthHeaderOrders Axalta()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "c16d8c60-753e-481c-9335-ee88d5abeb7a",
                CompanyID = 4681,
                PartnerID = 4681,
                Username = "sistemasaxalta",
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
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
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
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
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }
        public static AuthHeaderOrders KFC()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "5701b920-087f-4a87-8f22-6194d721ee04",
                CompanyID = 5100,
                PartnerID = 5100,
                Username = "sistemascirclek",
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
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
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
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
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
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
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
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
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
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
                OrderStatus = OrderStatuses.InProduction,
                //StartDate = new DateTime(2019, 10,10),
                //EndDate = new DateTime(2019, 10, 24),
                StartDate = DateTime.Today.AddDays(_DIAS),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }
        public static AuthHeaderOrders Exitus()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "0d69d096-d446-49f5-8f12-c5d1b0dfeaa7",
                CompanyID = 6363,
                PartnerID = 6363,
                Username = "sistemasexitus",
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }
        public static AuthHeaderOrders KIA()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "85524d0d-e70a-4143-bf97-3b6b62e8ce05",
                CompanyID = 6785,
                PartnerID = 6785,
                Username = "sistemaskia",
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders LSM()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "3d2e434c-e73b-4ec1-8f6d-ad89e933b079",
                CompanyID = 4909,
                PartnerID = 4909,
                Username = "sistemaslsmws",
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }

        public static AuthHeaderOrders Toyota()
        {
            return new AuthHeaderOrders()
            {
                MasterKey = "4c679eaf-2b92-4c66-b35a-5dac20ca5989",
                CompanyID = 6377,
                PartnerID = 6377,
                Username = "sistemastoyota",
                OrderStatus = OrderStatuses.InProduction,
                StartDate = DateTime.Today.AddDays(_DIAS),
                EndDate = DateTime.Now,
                ProducerID = _PRODUCERID
            };
        }
    }
}
