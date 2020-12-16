using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.EstadodeCuentaSAAM
{
    class BitacoraDAO:DAO
    {

        public void Insertar(BitacoraIntelisis bitacoraIntelisis)
        {
            this.ctx.BitacoraIntelisis.Add(bitacoraIntelisis);
            this.ctx.SaveChanges();
        }

        public void Insertar(Ejecucion ejecucion)
        {
            this.ctx.Ejecucion.Add(ejecucion);
            this.ctx.SaveChanges();
        }
    }
}
