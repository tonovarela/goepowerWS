using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.ReporteSAAM
{
    public class ClienteDAO:DAO
    {

        public bool Existe(int webuserID)
        {
            return this.ctx.Cliente.AsQueryable().Any(x => x.id_cliente == webuserID);
        }
        public void Registrar(ClienteSAAM cliente)
        {
            this.ctx.Cliente.Add(cliente);
            this.ctx.SaveChanges();

        }
    }
}
