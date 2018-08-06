using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Lito
{
    public class ClienteDAO:DAO
    {

        public CteHonda BuscarCliente(string id)
        {
            CteHonda cliente = null;                        
                cliente=this.ctx.CteHonda.AsQueryable().Where(x=>x.CteHonda1==id).FirstOrDefault();
            
            return cliente;
                
        }
        public CtoCampoExtra BuscarClienteCampoExtra (string id)
        {

            CtoCampoExtra client = null;
            client=this.ctx.CtoCampoExtra.AsQueryable()
                .Where(x => x.Tipo == "Cliente" && 
                            x.SubTipo == "Cliente" && 
                            x.CampoExtra == "WebUserID" &&
                            x.Valor==id
                            )
                 .FirstOrDefault();
            return client;
        }
    }
}
