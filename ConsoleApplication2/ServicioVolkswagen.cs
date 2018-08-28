using ConsoleApplication2.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class ServicioVolkswagen:Servicio
    {

        public ServicioVolkswagen()
        {
            this._nombreTienda = "volkswagen";
            this._workspace += _nombreTienda;
            this._conexion = Credenciales.Volkswagen();
        }
    }
}
