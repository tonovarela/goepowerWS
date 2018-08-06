using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Goepower
{
    public class DAO:IDisposable
    {

        protected GoepowerDataContext ctx;
       
        public DAO()
        {  
            
             this.ctx = new GoepowerDataContext();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}
