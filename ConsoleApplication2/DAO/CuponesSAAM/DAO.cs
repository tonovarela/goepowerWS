using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.CuponesSAAM
{
    public class DAO
    {
        protected CuponesSAAMEntities ctx;
        public DAO()
        {

            this.ctx = new CuponesSAAMEntities();
        }

    }
}
