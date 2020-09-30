using ConsoleApplication2.ViewModel.Goepower;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public class ServiceRest
    {


        private RestClient client;

        public ServiceRest()
        {
            this.client = new RestClient("https://live.goepower.com");
        }

        public ResponseGPViewModel ObtenerDetalleOrden(int numero_orden)
        {
            ResponseGPViewModel responseGPViewModel = null;            
            var request = new RestRequest("cs_handlers/apis/InfoHandler.ashx", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("un", "sistemaskia");
            request.AddParameter("pw", "123456789*/");
            request.AddParameter("action", "getorder");
            request.AddParameter("oid", numero_orden);
            request.AddParameter("apikey", "85524d0d-e70a-4143-bf97-3b6b62e8ce05");
            request.AddParameter("full", "true");
            var res = client.Execute<ResponseGPViewModel>(request);
            if (res.IsSuccessful)
            {
                responseGPViewModel = res.Data;
            }
            return responseGPViewModel;
        }

    }
}
