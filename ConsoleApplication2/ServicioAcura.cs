using ConsoleApplication2.Class;

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
    }
}
