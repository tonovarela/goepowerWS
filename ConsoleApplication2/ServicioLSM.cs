using ConsoleApplication2.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public  class ServicioLSM:Servicio
    {

        public ServicioLSM()
        {
            
                this._nombreTienda = "lsm";
                this._workspace += _nombreTienda;
                this._conexion = Credenciales.LSM();            
        }

       
    }
}
