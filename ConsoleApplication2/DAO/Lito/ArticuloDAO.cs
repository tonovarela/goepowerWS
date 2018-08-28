using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Lito
{
   public class ArticuloDAO:DAO
    {
        
        public bool EstaDadodeAltaHonda(string SKU)
        {

            return true;
           // return this.ctx.Art.AsQueryable().Any(articulo => articulo.Articulo == SKU); 
        }


    }
}
