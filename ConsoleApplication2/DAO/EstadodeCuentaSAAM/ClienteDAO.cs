using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.EstadodeCuentaSAAM
{
    public class ClienteDAO:DAO
    {
        public List<int> getPendientesMandarCorreo()
        {
            return this.ctx.Database.SqlQuery<int>(@"select 
                                                a.webUserID
                                                from Cliente a
                                                join Orden b  on (a.webUserID = b.webUserID)
                                                where 1 = 1
                                                 and b.BillingMethod = 'Convenio CIE Bancomer / Monedero Electrónico 2019'
                                                 and envioCorreo = 0
                                                 and b.estatus not in ('Cancelled', 'Pending')
                                                 and a.envioCorreo = 0").ToList();            
        }
        public void marcarCorreoEnviado(int webUserID)
        {
            this.ctx.Database.ExecuteSqlCommand("update Cliente set envioCorreo=1 where webUserID=@webUserID", new SqlParameter("webUserID",webUserID));
        }
        
    }

}
