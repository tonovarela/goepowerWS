using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.ReporteSAAM
{
    public class ItemDAO:DAO
    {


        public void Registrar(ItemSAAM item)
        {
            this.ctx.Item.Add(item);
            this.ctx.SaveChanges();
        }
        public void BorrarTodo()
        {
            this.ctx.Database.ExecuteSqlCommand("delete from Item");
            this.ctx.SaveChanges();
        }
    }

}
