using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Lito
{
    public class DAO
    {

        protected LitoDataContext ctx;
       
        public DAO()
        {
             this.ctx = new  LitoDataContext();
             
        }

        
    }
}
