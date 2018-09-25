using ConsoleApplication2.Class;

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


      
    }
}
