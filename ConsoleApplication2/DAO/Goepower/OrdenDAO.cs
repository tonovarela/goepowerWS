using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Goepower
{
    public class OrdenDAO : DAO 
    {

        public Orden existe(string ordenID)
        {
            return this.ctx.Orden.AsQueryable().Where(x => x.ordenID.Contains(ordenID)).FirstOrDefault() ;
            
        }
        
        public void Agregar(Orden orden)
        {
            ctx.Orden.InsertOnSubmit(orden);
            
            this.ctx.SubmitChanges();
        }
        public void Actualiza(Orden orden)
        {
            Orden _orden= this.existe(orden.ordenID);
            _orden.status = orden.status;
            _orden.orderDate = orden.orderDate;
            _orden.releaseDate = orden.releaseDate;
            _orden.shippingDate = orden.shippingDate;
            _orden.completeDate = orden.completeDate;

            ctx.SubmitChanges();

            Console.WriteLine("Se actualiza");
        }
        public void ActualizaPago (string idOrden)
        {
            Orden _orden = this.existe(idOrden);
            if (_orden != null)
            {
                _orden.pagado = true;
                ctx.SubmitChanges();
                Console.WriteLine("Se actualiza el pago");
            }
        }




    }
}
