using System;
using System.Collections.Generic;
using System.Configuration;
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
            string connection = ConfigurationManager.ConnectionStrings["Lito"].ConnectionString;            
             this.ctx = new  LitoDataContext(connection);            

        }

        
    }
}
