using ConsoleApplication2.Class;

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
