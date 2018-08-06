using ConsoleApplication2.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public class Transaccion
    {


        public String Fecha { get; set; }
        public String Concepto { get; set; }
        public String Cargo { get; set; }
        public String Abono { get; set; }
        public String Saldo { get; set; }

      

        public Transaccion(String line)
        {
            this.Fecha = line.Split('\t')[0];
            this.Concepto = line.Split('\t')[1];
            this.Cargo = line.Split('\t')[2];
            this.Abono = line.Split('\t')[3];
            this.Saldo = line.Split('\t')[4];                       
        }

        
        public bool esTransaccion()
        {
            return this.Concepto.StartsWith("CE") ? true : false;
        }
        public String WorkOrder
        {
            get
            {
                return this.Concepto.Substring(2, 6);
            }
        }

        public void Imprimir()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Concepto :[{this.Concepto}]");
            Console.WriteLine($"WorkOrden :[{this.WorkOrder}]");
            Console.WriteLine($"Abono :[{this.Abono}]");
            Console.WriteLine("------------------------------");
        }
    }
}
