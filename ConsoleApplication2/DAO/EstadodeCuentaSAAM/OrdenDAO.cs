using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.EstadodeCuentaSAAM
{
    public class OrdenDAO:DAO
    {
        public void Agregar(OrdenEstadoCuenta orden)
        {
            this.ctx.Orden.Add(orden);
            this.ctx.SaveChanges();
        }

        public bool Existe(int numero_orden)
        {
            return this.ctx.Orden.Any(x=>x.numero_orden==numero_orden);
        }

        public OrdenEstadoCuenta obtenerOrden(int numero_orden)
        {
            return this.ctx.Orden
                                 .Where(x => x.numero_orden == numero_orden)
                                 .FirstOrDefault() ;
        }

        public void ActualizarEstatus(int numero_orden,string status)
        {
            var _orden = this.obtenerOrden(numero_orden);
            _orden.estatus = status;
            this.ctx.SaveChanges();

        }

    }
}
