using ConsoleApplication2.Class;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenChangeStatus;
using ConsoleApplication2.ViewModel.Goepower;
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
            ServicioKFC s_kfc = new ServicioKFC();
            ServicioSherwin s_sherwin = new ServicioSherwin();
            ServicioAxalta s_axalta = new ServicioAxalta();
            ServicioToyota s_toyota = new ServicioToyota();
            if (args.Length > 0)  // Parametro desde la tarea programada  
            {
                switch (args[0])
                {
                    case "DescargaPrintTemplate":
                        s_axalta.DescargarArchivos();
                        s_kummon.DescargarArchivos();
                        s_michelin.DescargarArchivos();
                        s_sherwin.DescargarArchivos();
                        s_seat.DescargarArchivos();                        
                       // s_kfc.DescargarArchivos();                        
                        break;
                    case "DescargaPrintTemplateMichelin":
                        Console.WriteLine("Descarga de Archivos para la tienda de Michelin");
                        s_michelin.DescargarArchivos();
                        break;
                    case "DescargaPrintTemplateSherwin":
                        Console.WriteLine("Descarga de Archivos para la tienda de Sherwin");
                        s_sherwin.DescargarArchivos();
                        break;
                    case "DescargaPrintTemplateSeat":
                        Console.WriteLine("Descarga de Archivos para la tienda de Seath");
                        //s_seat.LiberarOrdenesReleaseToProduccion();
                        s_seat.DescargarArchivos();
                        break;
                    //case "DescargaPrintTemplateCirculoK":
                    //    //Console.WriteLine("Descarga de Archivos para la tienda de Circulo K");
                    //    //s_kfc.DescargarArchivos();
                    //    break;
                    case "DescargaPrintTemplateKumon":
                        Console.WriteLine("Descarga de Archivos para la tienda de Kumon");
                        s_kummon.DescargarArchivos();
                        break;
                    case "DescargaPrintTemplateAxalta":
                        Console.WriteLine("Descarga de Archivos para la tienda de Axalta");
                        s_axalta.DescargarArchivos();
                        break;
                    case "PrefacturacionIntelisis":
                        // Solo las ordenes de Kumon y Sherwin                       
                        //s_kummon.RegistrarPrefacturacionIntelisis("15 DIAS", "U.D.P.",null,3);
                        //s_sherwin.RegistrarPrefacturacionIntelisis("15 DIAS", "L.R.P.",null,2.5);
                        s_kfc.RegistrarPrefacturacionIntelisis("", "", null, 9.0, OrdenService.OrderStatuses.Released);
                        s_kummon.RegistrarPrefacturacionIntelisis("", "", null, 3);
                        s_sherwin.RegistrarPrefacturacionIntelisis("", "", null, 2.5);
                        break;
                    case "leerDropbox":  //Leer carpeta de Movimientos

                        Console.WriteLine("Leyendo la carpeta de Movimientos");
                        Lector lector = new Lector();
                        try
                        {
                            List<TransaccionDTO> ordenes = lector.LeerOrdenesExcel();
                            ordenes.ForEach(x =>
                            {
                                //Se aprovecha el bug del servicio Soap para saber si la workorden es de SAAM sin importar la tienda
                                var resultadoWS = s_kummon.TraerInfo(Int32.Parse(x.WorkOrden));
                                if (resultadoWS != null)
                                {
                                    x.EsTransferencia = resultadoWS.Order.BillingMethodName.Contains("Transferencia Bancaria");
                                    x.Tienda = "SAAM";
                                    x.IdTienda = resultadoWS.Order.CompanyID;
                                }
                            });
                            var ordenesRegistroIntelisis = ordenes.Where(x => x.EsTransferencia || x.IdTienda == 6785)
                                                  .Select(x => new { x.WorkOrden, x.EsTransferencia });
                            //Se registran todos cuyo metodo de pago sea Transferencia bancaria o de la Tienda de KIA
                            ServicioKIA servicioKIA = new ServicioKIA();
                            ordenesRegistroIntelisis.ToList().ForEach(x =>
                            {
                                Console.WriteLine(x.WorkOrden);
                                servicioKIA.esTransferencia = true;
                                servicioKIA.RegistrarPrefacturacionIntelisis("","", Int32.Parse(x.WorkOrden));                                
                            });
                            var ordenesLiberar = ordenesRegistroIntelisis
                                                  .Select(x => x.WorkOrden);
                            if (ordenesLiberar.ToList().Count > 0)
                            {
                                Console.WriteLine(string.Join(",", ordenesLiberar));
                                s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesLiberar), ePowerOrderStatus.Release);
                                s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesLiberar), ePowerOrderStatus.ToProduction);
                            }
                            lector.borrarArchivo = true;
                        }
                        catch (Exception ex)
                        {
                            lector.borrarArchivo = false;
                            Console.WriteLine("Excepcion en la lectura del Reporte de Movimientos");
                        }
                        //lector.moverArchivos();
                        //Console.WriteLine("Leyendo la carpeta de Dropbox");                        
                        //try
                        //{
                        //Lector lector = new Lector();
                        //List<TransaccionDTO> ordenes = lector.LeerOrdenesExcel();
                        //ordenes.ForEach(x =>
                        //{
                        //    //Se aprovecha el bug del servicio Soap para saber si la workorden es de SAAM sin importar la tienda
                        //    var orden = s_kummon.TraerInfo(Int32.Parse(x.WorkOrden));
                        //    if (orden != null)
                        //    {
                        //        x.Tienda = "SAAM";
                        //    }
                        //});
                        //var ordenesK = ordenes.Where(x => x.Tienda.Contains("SAAM"))
                        //                      .Select(x => x.WorkOrden);
                        //ordenesK.ToList().ForEach(x =>
                        //{
                        //    //Preregistro en Intelisis
                        //    s_kummon.condiciones = "Contado Transferencia";
                        //    //s_kummon.RegistrarPrefacturacionIntelisis("","",Int32.Parse(x));
                        //});


                        //ordenesK.ToList().ForEach(x =>
                        //{
                        //    ServiceRest serviceRest = new ServiceRest();
                        //    int _orden = Int32.Parse(x);
                        //    //Si la orden que esta en este listado es de KIA registrarla en Intelisis
                        //    var o = serviceRest.ObtenerDetalleOrden(_orden);
                        //    if (o.Success)
                        //    {
                        //        ServicioKIA servicioKIA = new ServicioKIA();
                        //        servicioKIA.RegistrarPrefacturacionIntelisis("7 DIAS", "C.R.C.", _orden);
                        //    }
                        //});
                        //if (ordenesK.ToList().Count > 0)
                        //{
                        //    Console.WriteLine(string.Join(",", ordenesK));
                        //    s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesK), ePowerOrderStatus.Release);
                        //    s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesK), ePowerOrderStatus.ToProduction);
                        //}
                        //}
                        //catch(Exception ex)
                        //{

                        //}
                        break;
                    //  //Actualiza el estado de las ordenes  en la base de datos Goepower 192.168.2.209
                    case "Sincronizar":
                        Console.WriteLine("Sincronizando WorkOrders de GoePower");
                        try
                        {
                            s_honda.LlenarInfoGoePower();
                            s_kfc.LlenarInfoGoePower();
                            s_sherwin.LlenarInfoGoePower();
                            s_kummon.LlenarInfoGoePower();
                            s_michelin.LlenarInfoGoePower();
                            s_seat.LlenarInfoGoePower();
                            s_starbuck.LlenarInfoGoePower();
                            //s_volkswagen.LlenarInfoGoePower();
                            s_acura.LlenarInfoGoePower();
                        }catch(Exception ex)
                        {

                        }
                        break;
                    case "HondaPrefacturacion": // se corre Manual  -- 
                       s_honda.RegistrarPrefacturacionIntelisis("7 DIAS", "A.L.P.");
                       s_acura.RegistrarPrefacturacionIntelisis("7 DIAS", "A.L.P.");
                        break;

                    case "KIAPrefacturacion": // se corre Manual  -- 
                        ServicioKIA serviciokia = new ServicioKIA();                        
                        serviciokia.RegistrarPrefacturacionIntelisis("7 DIAS", "C.R.C.");
                        break;
                    case "ReporteSAAM": // Cada hora
                        try
                        {
                            s_honda.RegistrarOrdenesEnEstadoCuentaSAAM(1);
                            s_acura.RegistrarOrdenesEnEstadoCuentaSAAM(2);                            
                            s_toyota.RegistrarOrdenesEnEstadoCuentaSAAM(3);
                            s_honda.EnviarCorreosEstadoCuenta();
                        }catch(Exception ex)
                        {

                        }                                                
                        break;
                    case "ReporteNotificacion": //Diario en la 9:00 de la mañana
                        //Solo aplica para Honda y Acura
                        //Envia reporte de los dealer que este a 20% o menos de su saldo de monedero (Corporativo y jmartinez)
                        //Los dealers dejaran de aparecer en este reporte cuando exista un orden de Convenio CIE en estado pending asociada a su cuenta
                        s_honda.enviarReporteCorporativo();
                        //Envia notificacion por correo al dealer de que tiene una orden de Convenio CIE en estado pending por pagar
                        //Este correo solo se enviara una sola vez --(Se toma como referencia el campo envioCorreoSaldoporVencer de la base de datos)                        
                        s_honda.enviarNotificacionSaldoporVencer();
                        break;
                }
                return;
            }




            













        }




















    }
}
