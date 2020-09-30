using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.ReporteSAAM
{
    public class ProductoDAO:DAO
    {
        public void Agregar(ProductoSAAM producto)
        {
            this.ctx.Producto.Add(producto);
            this.ctx.SaveChanges();
        }

        public void EliminarTodos()
        {
            this.ctx.Database.ExecuteSqlCommand("delete from Producto");
            this.ctx.SaveChanges();
        }
    }
}
