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


        //public String Fecha { get; set; }
        public String Concepto { get; set; }
        //public String Cargo { get; set; }
        //public String Abono { get; set; }
        //public String Saldo { get; set; }

      

        public Transaccion(String line)
        {
            this.Concepto = "";
            int valor;
            try
            {
                if (Int32.TryParse(line.Substring(2, 1), out valor))  //Si el tercer Caracter es numerico y no es Cero
                {
                    //if (valor > 0)
                    //{
                        this.Concepto = line.Substring(0, 8);
                    //}
                }
            }
            catch(Exception e)
            {

            }                           
        }

        
        public bool esTransaccion()
        {
            return this.Concepto.StartsWith("C") ? true : false;
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
            Console.WriteLine("------------------------------");
        }
    }
}
