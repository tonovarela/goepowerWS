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
            ServicioKumon s_kummon = new ServicioKumon();
            ServicioHonda s_honda = new ServicioHonda();
            ServicioMichelin s_michelin = new ServicioMichelin();
            ServicioSeat s_seat = new ServicioSeat();
            ServicioStarBucks s_starbuck = new ServicioStarBucks();
            ServicioAcura s_acura = new ServicioAcura();
            ServicioVolkswagen s_volkswagen = new ServicioVolkswagen();
            ServicioCirculoK s_circulok = new ServicioCirculoK();
            ServicioSherwin s_sherwin = new ServicioSherwin();
            if (args.Length > 0)  // Parametro desde la tarea programada  
            {
                switch (args[0])
                {
                    case "DescargaPrintTemplate":
                        s_seat.DescargarArchivos();
                        s_kummon.DescargarArchivos();
                        s_circulok.DescargarArchivos();
                        s_michelin.DescargarArchivos();
                        s_sherwin.DescargarArchivos();
                        break;
                    case "PrefacturacionIntelisis":
                        // Solo las ordenes de Kumon y Sherwin                       
                        s_kummon.RegistrarPrefacturacionIntelisis("15 DIAS", "U.D.P.");
                        s_sherwin.RegistrarPrefacturacionIntelisis("15 DIAS", "L.R.P.");
                        break;
                    case "leerDropbox":
                        //Monedero Electronico y Facturacion
                        Console.WriteLine("Leyendo la carpeta de Dropbox");
                        // Lectura de la Carpeta en Dropbox
                        Lector lector = new Lector();
                        List<TransaccionDTO> ordenes = lector.getOrdenes();
                        var ordenes_kumon = ordenes.Where(x => x.Tienda.Contains("SAAM KUMON"))
                                                   .Select(x => x.WorkOrden);
                        if (ordenes_kumon.ToList().Count > 0)
                        {
                            Console.WriteLine(string.Join(",", ordenes_kumon));
                            s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenes_kumon));
                        }
                        break;
                    //  //Actualiza el estado de las ordenes  en la base de datos Goepower 192.168.2.209
                    case "Sincronizar":
                        Console.WriteLine("Sincronizando WorkOrders de GoPower");
                        s_honda.LlenarInfoGoePower();
                        s_circulok.LlenarInfoGoePower();
                        s_sherwin.LlenarInfoGoePower();
                        s_kummon.LlenarInfoGoePower();
                        s_michelin.LlenarInfoGoePower();
                        s_seat.LlenarInfoGoePower();
                        s_starbuck.LlenarInfoGoePower();
                        s_volkswagen.LlenarInfoGoePower();
                        s_acura.LlenarInfoGoePower();
                        break;

                    case "HondaPrefacturacion": // se corre Manual  -- 
                       s_honda.RegistrarPrefacturacionIntelisis("7 DIAS", "A.L.P.");
                        break;
                }
                return;
            }


            //FileExcel ex =new FileExcel();
            //ex.CrearExcelPrimerColumna(@"C:\Desarrollo\test.xlsx");

            s_seat.DescargarArchivos();
            //s_kummon.TraerInfo(731819);
            //728224




            Console.ReadLine();
            
        }






       













}
}
