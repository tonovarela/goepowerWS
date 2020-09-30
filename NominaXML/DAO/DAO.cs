using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXML.DAO
{
    public class DAO
    {


        protected XMLEjercicioEntities ctx;
 
        public DAO()
        {

            ctx = new XMLEjercicioEntities();
                        
        }

    }
}
