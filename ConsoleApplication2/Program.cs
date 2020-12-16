using ConsoleApplication2.BussinesLogic;
using System;

namespace ConsoleApplication2

{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            ServicioKumon servicioKummon = new ServicioKumon();
            ServicioHonda serviciohonda = new ServicioHonda();
            ServicioMichelin servicioMichelin = new ServicioMichelin();
            ServicioSeat servicioSeat = new ServicioSeat();            
            ServicioAcura servicioAcura = new ServicioAcura();                       
            ServicioSherwin servicioSherwin = new ServicioSherwin();
            ServicioAxalta servicioAxalta = new ServicioAxalta();    
                      
            if (args.Length > 0)  // Parametro desde la tarea programada  
            {
                switch (args[0])
                {
                    case "DescargaPrintTemplate":                        
                        manager.DescargarArchivos(servicioAxalta,
                                                  servicioKummon,                                                  
                                                  servicioSherwin,
                                                  servicioSeat,
                                                  servicioMichelin);                        
                        break;
                    case "DescargaPrintTemplateMichelin":
                        Console.WriteLine("Descarga de Archivos para la tienda de Michelin");
                        manager.DescargarArchivos(servicioMichelin);                        
                        break;
                    case "DescargaPrintTemplateSherwin":
                        Console.WriteLine("Descarga de Archivos para la tienda de Sherwin");
                        manager.DescargarArchivos(servicioSherwin);
                        
                        break;
                    case "DescargaPrintTemplateSeat":
                        Console.WriteLine("Descarga de Archivos para la tienda de Seath");
                        manager.DescargarArchivos(servicioSeat);
                        
                        break;                    
                    case "DescargaPrintTemplateKumon":
                        Console.WriteLine("Descarga de Archivos para la tienda de Kumon");
                        manager.DescargarArchivos(servicioKummon);                        
                        break;
                    case "DescargaPrintTemplateAxalta":
                        Console.WriteLine("Descarga de Archivos para la tienda de Axalta");
                        manager.DescargarArchivos(servicioAxalta);                        
                        break;
                    case "Intelisis":
                        manager.RegistroPreFacturacion();                        
                        break;
                    case "leerDropbox":  //Leer carpeta de Movimientos                         
                        manager.LeerCarpetaMovimientos();
                        break;                    
                    case "HondaPrefacturacion": // se corre Manual  -- 
                        serviciohonda.RegistrarPrefacturacionIntelisis();
                        servicioAcura.RegistrarPrefacturacionIntelisis();
                        break;

                    //case "KIAPrefacturacion": // se corre Manual  -- 
                    //    //ServicioKIA serviciokia = new ServicioKIA();
                    //    //serviciokia.RegistrarPrefacturacionIntelisis("7 DIAS", "C.R.C.");
                    //    break;
                    case "ReporteSAAM": // Cada hora
                        manager.RegistraOrdenesEstadoCuenta();
                        break;
                    case "ReporteNotificacion": //Diario en la 9:00 de la mañana
                        //Solo aplica para Honda y Acura
                        //Envia reporte de los dealer que este a 20% o menos de su saldo de monedero (Corporativo y jmartinez)
                        //Los dealers dejaran de aparecer en este reporte cuando exista un orden de Convenio CIE en estado pending asociada a su cuenta
                        serviciohonda.enviarReporteCorporativo();
                        //Envia notificacion por correo al dealer de que tiene una orden de Convenio CIE en estado pending por pagar
                        //Este correo solo se enviara una sola vez --(Se toma como referencia el campo envioCorreoSaldoporVencer de la base de datos)                        
                        serviciohonda.enviarNotificacionSaldoporVencer();
                        break;
                }
                return;
            }

            
            
            
        }    
    }
}
