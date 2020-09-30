using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.EstadodeCuentaSAAM
{
    public class DAO
    {
        protected EstadoCuentaSAAMEntities ctx;
        public DAO()
        {

            this.ctx = new EstadoCuentaSAAMEntities();            
        }

        
    }
}
