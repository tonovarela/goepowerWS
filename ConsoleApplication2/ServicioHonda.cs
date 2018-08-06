using ConsoleApplication2.Class;
using ConsoleApplication2.DAO;
using ConsoleApplication2.OrdenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
   public class ServicioHonda:Servicio
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
        

        

        
    }
}
