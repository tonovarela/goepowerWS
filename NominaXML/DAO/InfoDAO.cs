using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXML.DAO
{
    public class InfoDAO:DAO
    {

        public void Agregar(Info info)
        {
            this.ctx.Info.Add(info);
            this.ctx.SaveChanges();
        }


        public bool existe (string uiid)
        {
            return this.ctx.Info.Any(x=>x.uuid==uiid);
        }


        public void marcarComoCancelada(string uiid)
        {
            Info info=this.ctx.Info.SingleOrDefault(x => x.uuid == uiid);
            info.esCancelado = true;
            this.ctx.Info.Add(info);
            this.ctx.Entry(info).State = EntityState.Modified;
            this.ctx.SaveChanges();         
        }



    }
}
