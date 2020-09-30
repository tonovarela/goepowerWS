using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.ReporteSAAM
{
    public class OrdenDAO:DAO
    {
        public void Agregar(OrdenSAAM orden)
        {
            this.ctx.Orden.Add(orden);
            this.ctx.SaveChanges();
        }
        public void BorrarTodo()
        {
            this.ctx.Database.ExecuteSqlCommand("delete from Orden");
            this.ctx.SaveChanges();
        }
    }
}
