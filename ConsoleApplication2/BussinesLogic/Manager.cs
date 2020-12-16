using ConsoleApplication2.Class;
using ConsoleApplication2.DAO.EstadodeCuentaSAAM;
using ConsoleApplication2.Model;
using ConsoleApplication2.OrdenChangeStatus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication2.BussinesLogic
{
    public  class Manager
    {
        private ServicioKumon _s_kummon = new ServicioKumon();
        private ServicioHonda _s_honda = new ServicioHonda();                     
        private ServicioAcura _s_acura = new ServicioAcura();        
        private ServicioKFC _s_kfc = new ServicioKFC();
        private ServicioSherwin _s_sherwin = new ServicioSherwin();        
        private ServicioToyota _s_toyota = new ServicioToyota();
        private ServicioKIA _s_kia = new ServicioKIA();
        private ServicioLSM _s_lsm = new ServicioLSM();

        public void RegistroPreFacturacion()
        {
            _s_lsm.RegistrarPrefacturacionIntelisis(false,null, null);                                   //Transferencia Bancaria/Pago Ventanilla 
            _s_kfc.RegistrarPrefacturacionIntelisis(true, null, 9.0, OrdenService.OrderStatuses.Released); //Facturación por pedido
            _s_kummon.RegistrarPrefacturacionIntelisis(true, null, 3);                                     //Facturación por pedido
            _s_sherwin.RegistrarPrefacturacionIntelisis(true, null, 2.5);                                  //Facturación por pedido
            _s_kia.RegistrarPrefacturacionIntelisis(false, null, null);                                    //Transferencia Bancaria/Pago Ventanilla 
            BitacoraDAO bitacoraDAO = new BitacoraDAO();
            bitacoraDAO.Insertar(new Ejecucion()
            {
                fecha_registro = DateTime.Now
            });
        }
        public void RegistraOrdenesEstadoCuenta()
        {
            try
            {
                _s_honda.RegistrarOrdenesEnEstadoCuentaSAAM(1);
                _s_acura.RegistrarOrdenesEnEstadoCuentaSAAM(2);
                _s_toyota.RegistrarOrdenesEnEstadoCuentaSAAM(3);
                _s_honda.EnviarCorreosEstadoCuenta();
            }
            catch (Exception ex)
            {

            }
        }
        public void LeerCarpetaMovimientos()
        {
            ServicioKumon servicioKumon = new ServicioKumon();
            Console.WriteLine("Leyendo la carpeta de Movimientos");
            Lector _lector = new Lector();
            try
            {
                List<TransaccionDTO> ordenes = _lector.LeerOrdenesExcel();
                ordenes.ForEach(x =>
                {
                    //Se aprovecha el bug del servicio Soap para saber si la workorden es de SAAM sin importar la tienda
                    var resultadoWS = servicioKumon.TraerInfo(Int32.Parse($"1{x.WorkOrden}"));
                    if (resultadoWS.Order.OrderID > 0)
                    {
                        Console.WriteLine($"WorkOrden Identificada {resultadoWS.Order.OrderID.ToString()} en la tienda {resultadoWS.Order.CompanyID}");
                        x.EsTransferencia = resultadoWS.Order.BillingMethodName.Contains("Transferencia Bancaria");
                        x.Tienda = "SAAM";
                        x.WorkOrden = resultadoWS.Order.OrderID.ToString();
                        x.IdTienda = resultadoWS.Order.CompanyID;
                    }
                });
                
                var ordenesSAAM = ordenes.Where(x => x.Tienda == "SAAM").ToList();
                this.PagoFacturacionPedido(ordenes, 5104, "KUMON");
                this.PagoFacturacionPedido(ordenes, 4996, "SHERWIN");  //NUEVO                            
                this.PagoFacturacionPedido(ordenes, 6785, "KIA");      //NUEVO
                this.PagoTransferenciaKIA(ordenes);
                this.PagoTransferenciaLSM(ordenes);                    //NUEVO --solo libera a release

                    _lector.borrarArchivo = true;
            }
            catch (Exception ex)
            {
                _lector.borrarArchivo = false;
                Console.WriteLine("Excepcion en la lectura del Reporte de Movimientos");
            }
            _lector.moverArchivos();
        }
        private  void PagoFacturacionPedido(List<TransaccionDTO> ordenes, int idTienda, string nombre)
        {
            var ordenesLiberar = ordenes.Where(x => x.IdTienda == idTienda && !x.EsTransferencia)
                                     .Select(x => x.WorkOrden);
            ServicioKumon s_kummon = new ServicioKumon();
            if (ordenesLiberar.ToList().Count > 0)
            {
                Console.WriteLine($"Liberando a produccion ordenes de {nombre}");
                Console.WriteLine(string.Join(",", ordenesLiberar));
                s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesLiberar), ePowerOrderStatus.Release);
                s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesLiberar), ePowerOrderStatus.ToProduction);
            }
        }
        private  void PagoTransferenciaKIA(List<TransaccionDTO> ordenes)
        {
            var ordenesLiberar = ordenes.Where(x => x.EsTransferencia && x.IdTienda == 6785)
                                      .Select(x => new { x.WorkOrden, x.EsTransferencia });
            //Se registran todos cuyo método de pago sea Transferencia bancaria  de la Tienda de KIA            
            ServicioKumon s_kummon = new ServicioKumon();
            if (ordenesLiberar.ToList().Count > 0)
            {
                Console.WriteLine($"Liberando a produccion ordenes de  KIA");
                Console.WriteLine(string.Join(",", ordenesLiberar));
                s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesLiberar), ePowerOrderStatus.Release);
                s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesLiberar), ePowerOrderStatus.ToProduction);
            }            
        }
        private  void PagoTransferenciaLSM(List<TransaccionDTO> ordenes)
        {
            var ordenesLiberar = ordenes.Where(x => x.EsTransferencia && x.IdTienda == 4909)
                                      .Select(x => x.WorkOrden);
            //Se liberan a produccion todos cuyo método de pago sea Transferencia bancaria 
            ServicioKumon s_kummon = new ServicioKumon();
            if (ordenesLiberar.ToList().Count > 0)
            {
                Console.WriteLine($"Liberando a release ordenes de  LSM");
                Console.WriteLine(string.Join(",", ordenesLiberar));
                s_kummon.CambiarEstatusOrdenes(string.Join(",", ordenesLiberar), ePowerOrderStatus.Release);
            }


        }        
        public void DescargarArchivos(params IProduccion[] servicios)
        {
            foreach (IProduccion servicio in servicios)            
                    servicio.DescargarArchivos();            
        }
    
    }



}
