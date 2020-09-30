using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.ReporteSAAM
{
    public class DAO : IDisposable
    {
        protected ReporteSAAMEntities ctx;
        public DAO()
        {
            this.ctx = new ReporteSAAMEntities();            
        }
        public void Dispose()
        {
            this.ctx.Dispose();
        }
    }
}
