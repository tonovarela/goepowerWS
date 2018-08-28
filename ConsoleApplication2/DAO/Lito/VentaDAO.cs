using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Lito
{
    public class VentaDAO:DAO
    {
        
        public bool Buscar(string idOrden)
        {
            //this.ctx.Log = Console.Out;            
            var result = ctx.Venta.AsQueryable().Any(venta => venta.OrdenID == idOrden && venta.Estatus != "Cancelado");
            return result;
            
        }

        public int Insertar(Venta venta)
        {
            
                this.ctx.Venta.InsertOnSubmit(venta);
                this.ctx.SubmitChanges();
                                        
                return venta.ID;
        }

        public void InsertarDetalle(VentaD venta)
        {            
            this.ctx.VentaD.InsertOnSubmit(venta);
            this.ctx.SubmitChanges();            

        }

        public string obtenerTienda(string ordenid)
        {
            var venta= this.ctx.Venta.AsQueryable().Where(x=>x.OrdenID==ordenid).FirstOrDefault();
            return venta == null ? "sin Tienda" : venta.Concepto;
        }




    }
}
