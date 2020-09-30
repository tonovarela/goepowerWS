using NominaXML.clases;
using NominaXML.DAO;
using NominaXML.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NominaXML
{
    class Program
    {
        static void Main(string[] args)
        {



            LectorXML lectorXML = new LectorXML();
            List<NominaViewModel> lista = lectorXML.ObtenerInfo();

            //InfoDAO infoDAO = new InfoDAO();


            Console.WriteLine(lista.Count);
            Console.ReadLine();
            lista.ForEach(x =>
            {

                Info info = new Info()
                {
                    rfc = x.RFC,
                    uuid = x.UUID,
                    fechafinalpago = x.FechaFinalPago,
                    fechainicialpago = x.FechaInicialPago,
                    fecha_timbrado = x.FechaTimbrado,
                    fecha_pago = x.FechaPago,
                    isr = Decimal.Parse(x.RetencionISR.ToString()),
                    nombre = x.Nombre,
                    path = x.path,
                    reintegroISR = Decimal.Parse(x.ReintegroISR.ToString())


                };
                //if (infoDAO.existe(x.UUID))
                //{
                //    Console.WriteLine("Ya esta registrado");
                //    return;
                //}
                //infoDAO.Agregar(info);
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Nombre {0}", x.Nombre);
                Console.WriteLine("RFC {0}", x.RFC);
                Console.WriteLine("UIID {0}", x.UUID);
                Console.WriteLine("FechaPago {0}", x.FechaPago);
                Console.WriteLine("FechaInicialPago {0}", x.FechaInicialPago);
                Console.WriteLine("FechaFinaPago {0}", x.FechaFinalPago);
                Console.WriteLine("FechaTimbrado {0}", x.FechaTimbrado);
                Console.WriteLine("Retension ISR {0}", x.RetencionISR);
                Console.WriteLine("Reintegro ISR {0}", x.ReintegroISR);
                Console.WriteLine("-----------------------------");
            });


            //string[] cancelados = lectorXML.obtenerCDFICancelados();
            //Console.WriteLine(cancelados.Length);
            //cancelados.ToList().ForEach((c) =>
            //{
            //    infoDAO.marcarComoCancelada(c);

            //});

            Console.ReadLine();

        }
    }
}
