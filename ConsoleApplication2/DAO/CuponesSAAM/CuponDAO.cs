using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.CuponesSAAM
{
    public class CuponDAO:DAO
    {

        public Cupon obtenerSKU(string nombreCupon) {

            return this.ctx.Cupon
                            .AsQueryable()
                            .Where(x => x.nombre == nombreCupon)
                            .FirstOrDefault();
         
        }
    }
}
