using ConsoleApplication2.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DAO.Lito
{
    public class ClienteDAO:DAO
    {

        public CteCto BuscarContacto(int id)
        {
            return this.ctx.CteCto.AsQueryable().FirstOrDefault(x => x.ID == id);
        }



        public CteHonda BuscarCliente(string id)
        {
            CteHonda cliente = null;                        
                cliente=this.ctx.CteHonda.AsQueryable().Where(x=>x.CteHonda1==id).FirstOrDefault();
            
            return cliente;
                
        }

        public Cte ObtenerDetalle(string cte)
        {
            string sql = $@"Select 
                                Agente, 
                                Condicion, 
                                Concepto= 'SAAM ' +(select Valor From ctocampoextra Where campoextra='EmpresaWeb' and clave=cte.cliente),
                                Vencimiento =(Select Diasvencimiento from condicion where condicion=cte.condicion)
                                From Cte 
                                Where estatus='Alta' 
                                and cliente={cte}";
            return this.ctx
                        .ExecuteQuery<Cte>(sql)
                        .FirstOrDefault() ;            
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
