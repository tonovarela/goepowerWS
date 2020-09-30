using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Goepower
{
    public class OrdenFacturacionDAO: DAO
    {


        public bool Existe(int ordenID)
        {
            return this.ctx.OrdenFacturacion.AsQueryable().Any(x => x.numero_orden == ordenID);

        }
        public void Registrar(OrdenFacturacion orden)
        {
            this.ctx.OrdenFacturacion.Attach(orden);
            this.ctx.SubmitChanges();
        }
    }
}
