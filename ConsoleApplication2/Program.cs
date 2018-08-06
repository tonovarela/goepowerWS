using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication2

{
    class Program
    {

        static void Main(string[] args)
        {

            ServicioKumon sk = new ServicioKumon();
            ServicioHonda ch = new ServicioHonda();
            ServicioMichelin mc = new ServicioMichelin();
            ServicioSeat sseat = new ServicioSeat();
            ServicioStarBucks sstar = new ServicioStarBucks();
            ServicioAcura sacura = new ServicioAcura();
            ServicioVolkswagen sv = new ServicioVolkswagen();
            ServicioCirculoK sck = new ServicioCirculoK();
            ServicioSherwin ssw = new ServicioSherwin();



            if (args.Length > 0)  // Parametro desde la tarea programada  
            {

                switch (args[0])
                {
                    case "leerDropbox":
                        // 9 p.m de la noche Solo las ordenes de Kumon
                        sk.LlenarInfoDB();

                        
                        Console.WriteLine("Leyendo la carpeta de Dropbox");
                        // Lectura de la Carpeta en Dropbox
                        Lector lector = new Lector();
                        List<TransaccionDTO> ordenes = lector.getOrdenes();
                        var ordenes_kumon = ordenes.Where(x => x.Tienda.Contains("SAAM KUMON"))
                                                   .Select(x => x.WorkOrden);
                        if (ordenes_kumon.ToList().Count > 0)
                        {
                            Console.WriteLine(string.Join(",", ordenes_kumon));
                            sk.CambiarEstatusOrdenes(string.Join(",", ordenes_kumon));
                        }
                        break;
                    // 12:00 de la noche //Actualiza el estado de las ordenes  en la base de datos Goepower 192.168.2.209
                    case "Sincronizar":
                        Console.WriteLine("Sincronizando WorkOrders de GoPower");
                        ssw.LlenarInfoDB();
                        sk.LlenarInfoGoePower();
                        mc.LlenarInfoGoePower();
                        sseat.LlenarInfoGoePower();
                        sstar.LlenarInfoGoePower();
                        sv.LlenarInfoGoePower();
                        sacura.LlenarInfoGoePower();
                        ch.LlenarInfoGoePower();
                        sck.LlenarInfoGoePower();

                        break;

                }


                return;
            }
            //sk.CambiarEstatusOrdenes("694929");


            // Este se corre manual
            //ServicioHonda h = new ServicioHonda();
            //h.LLenarInfoDB();

            
            #region Descarga de Archivos
            sk.DescargarArchivos();
            sck.DescargarArchivos();
            mc.DescargarArchivos();
            ssw.DescargarArchivos();
            #endregion


            Console.ReadLine();

            //Console.Read();























            //Console.Read();

        }












    }
}
