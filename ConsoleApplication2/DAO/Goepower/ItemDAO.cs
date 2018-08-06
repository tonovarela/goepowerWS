using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Goepower
{
    public class ItemDAO:DAO
    {

        

        public void Agregar(Item item)
        {
            this.ctx.Item.InsertOnSubmit(item);
            this.ctx.SubmitChanges();
        }


    }
}
