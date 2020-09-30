using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class.Excepciones
{
    public class JobException:Exception
    {
        public JobException(string mensaje)
            : base(String.Format("Excepcion de Crece {0}", mensaje))
        {

        }
    }
}
