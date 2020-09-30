using NominaXML.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NominaXML.clases
{
    public class LectorXML
    {

        private string directorioArchivos = @"C:\Desarrollo\Nomina\RespCFD_Nomina\2019";

        private string[] obtenerListaArchivos()
        {
            //string[] filePaths = Directory.GetFiles(directorioArchivos, "*.xml");
            //            return filePaths;
            Console.WriteLine(Directory.EnumerateFiles(directorioArchivos, "*.xml", SearchOption.AllDirectories).ToList().Count);
            return Directory.EnumerateFiles(directorioArchivos, "*.xml", SearchOption.AllDirectories).ToArray();
        }


        public string[] obtenerListaArchivosCancelados()
        {
            
            return Directory.EnumerateFiles(directorioArchivos, "*CANCELACION.xml", SearchOption.AllDirectories).ToArray();
         

        }


        public NominaCanceladaViewModel CFDICancelado(XDocument doc)
        {
            NominaCanceladaViewModel nominaViewModel = null;
            List<XElement> xElements = doc.Root.Descendants().ToList();
            if (xElements.Any(r => r.Name.LocalName.ToUpper() == "UUID"))
            {
                //string fecha= this.obtenerInfo(doc, "ACUSE", "FECHA");
                nominaViewModel = new NominaCanceladaViewModel()
                {
                    UUID = xElements.Where(r => r.Name.LocalName.ToUpper() == "UUID").FirstOrDefault().Value,                    
                };                
            }               
            return nominaViewModel;
        }


        public string[] obtenerCDFICancelados()
        {
            XmlDocument xml = new XmlDocument();
            string[] paths;
            List<string> result = new List<string>();
            paths = this.obtenerListaArchivosCancelados();
            //int i = 0;
            paths.ToList().ForEach(path =>
            {
                try
                {
                    xml.Load(path);
                }
                catch (Exception e)
                {
                    return;
                }
                XDocument doc = XDocument.Load(path);
                //this.CFDICancelado(doc);
                XElement xelement = doc.Root.Descendants().ToList().Where(r => r.Name.LocalName.ToUpper() == "UUID").FirstOrDefault();
                result.Add(xelement.Value);
                
            });
            return result.ToArray() ;
        }
     

        private string obtenerInfo(XDocument d, string elemento, string atributoValor)
        {
            XElement xelement = d.Root.Descendants().ToList().Where(r => r.Name.LocalName.ToUpper() == elemento).FirstOrDefault();
            XAttribute xatributte = null;
            if (xelement != null && xelement.HasAttributes)
            {
                xatributte = xelement.Attributes().Where(x => x.Name.ToString().ToUpper() == atributoValor).FirstOrDefault();

            }
            return xatributte == null ? "" : xatributte.Value;
        }


        public void leerChildren(XDocument d,string padre)
        {
            var todos = d.Root.Descendants()
                                  .Where(r => r.Name.LocalName.ToUpper() == padre.ToUpper())
                                  .Descendants()                                  
                                  .ToList();
            todos.ForEach(x =>
            {
                Console.WriteLine($"-----------------------------------");
                x.Attributes().ToList().ForEach(a =>
                {
                    Console.WriteLine($" Nombre: {a.Name}  valor: {a.Value}");
                });                
                Console.WriteLine($"-----------------------------------");
            });
            
        }

        private double obtenerReintegro(XDocument d) 
        {
            double isr = 0;
            var percepcionesElement = d.Root.Descendants()
                                   .Where(r => r.Name.LocalName.ToUpper() == "PERCEPCIONES")
                                   .Descendants()
                                   .Attributes()
                                   .Where(x => x.Value.ToUpper().Contains("ISR"))
                                   .ToList();

           
            percepcionesElement.ForEach(pe =>
            {
                XElement xElementISR = pe.Parent;
                XAttribute xAttributeISR = xElementISR.Attributes().Where(x => x.Name.ToString().ToUpper() == "IMPORTEEXENTO").FirstOrDefault();
                if (xAttributeISR != null)
                {
                    isr = Double.Parse(xAttributeISR.Value);
                }
            });
            //if (percepcionesElement != null)
            //{
            //    XElement xElementISR = percepcionesElement.Parent;
            //    XAttribute xAttributeISR = xElementISR.Attributes().Where(x =>  x.Name.ToString().ToUpper() == "IMPORTEEXENTO").FirstOrDefault();
            //    if (xAttributeISR != null)
            //    {
            //        isr = Double.Parse(xAttributeISR.Value);
            //    }
            //}

            return isr;
        }



        private double obtenerReintegroOtrosPagos(XDocument d)
        {

            double isr = 0;
            var deduccionesElement = d.Root.Descendants()
                                   .Where(r => r.Name.LocalName.ToUpper() == "OTROSPAGOS")
                                   .Descendants()
                                   .Attributes()
                                   .Where(x => x.Value.ToUpper().Contains("ISR"))
                                   .ToList();

            
                deduccionesElement.ForEach(de =>
                {
                    XElement xElementISR = de.Parent;
                    var xAttributeISR = xElementISR.Attributes().Where(x => x.Name.ToString().ToUpper() == "IMPORTE" || x.Name.ToString().ToUpper() == "IMPORTEGRAVADO").FirstOrDefault();
                    if (xAttributeISR != null)
                    {
                        isr += Double.Parse(xAttributeISR.Value);
                    }
                });
            
                return isr;
        }

        private double obtenerISR(XDocument d)
        {
            double isr = 0;
            var deduccionesElement = d.Root.Descendants()
                                   .Where(r => r.Name.LocalName.ToUpper() == "DEDUCCIONES")
                                   .Descendants()
                                   .Attributes()
                                   .Where(x => x.Value.ToUpper().Contains("ISR"))
                                   .ToList();
            if (deduccionesElement != null)
            {
                deduccionesElement.ForEach(de =>
                {                    
                    XElement xElementISR = de.Parent;                   
                    var xAttributeISR = xElementISR.Attributes().Where(x => x.Name.ToString().ToUpper() == "IMPORTE" || x.Name.ToString().ToUpper() == "IMPORTEGRAVADO").FirstOrDefault();
                    if (xAttributeISR != null)
                    {
                            isr += Double.Parse(xAttributeISR.Value);
                    }
                });
                                           
            }
            return isr;
        }

        private bool tieneTimbrado(XDocument d)
        {
            XElement xelement = d.Root.Descendants().ToList().Where(r => r.Name.LocalName.ToUpper() == "TIMBREFISCALDIGITAL").FirstOrDefault();
            return xelement !=null;
        }

        public List<NominaViewModel> ObtenerInfo()
        {
            string[] paths;

            List<NominaViewModel> nominaViewModels = new List<NominaViewModel>();
            paths = this.obtenerListaArchivos();
            XmlDocument xml = new XmlDocument();
            int i = 0;
            paths.ToList().ForEach(path =>
            {
                NominaViewModel nominaViewModel = new NominaViewModel();
                DateTime fecha = DateTime.Now;
                string fechaString = String.Empty;
                if (path.Contains("CANCELACION"))
                {
                    return;
                }             
                try
                {
                    xml.Load(path);
                }
                catch (Exception e)
                {
                    return;
                }                               
                XDocument doc = XDocument.Load(path);            
                if (!tieneTimbrado(doc))
                {
                    Console.WriteLine("No tiene timbrado");
                    return;
                }
                else
                {
                    //Console.WriteLine("Tiene timbrado");
                    Console.WriteLine("Leyendo la iteración {0} del archivo {1}", i++, path);
                }
                nominaViewModel.path = path;
                nominaViewModel.Nombre = this.obtenerInfo(doc, "RECEPTOR", "NOMBRE");
                nominaViewModel.RFC = this.obtenerInfo(doc, "RECEPTOR", "RFC");
                nominaViewModel.UUID = this.obtenerInfo(doc, "TIMBREFISCALDIGITAL", "UUID");
                fechaString = this.obtenerInfo(doc, "NOMINA", "FECHAFINALPAGO");
                nominaViewModel.FechaFinalPago = DateTimeOffset.Parse(fechaString).DateTime;
                fechaString = this.obtenerInfo(doc, "NOMINA", "FECHAINICIALPAGO");
                nominaViewModel.FechaInicialPago = DateTimeOffset.Parse(fechaString).DateTime;
                fechaString = this.obtenerInfo(doc, "NOMINA", "FECHAPAGO");
                nominaViewModel.FechaPago = DateTimeOffset.Parse(fechaString).DateTime;
                fechaString = this.obtenerInfo(doc, "TIMBREFISCALDIGITAL", "FECHATIMBRADO");
                nominaViewModel.FechaTimbrado = DateTimeOffset.Parse(fechaString).DateTime;

                //Console.WriteLine("PERCEPCIONES");
                //this.leerChildren(doc, "PERCEPCIONES");
                //Console.WriteLine("DEDUCCIONES");
                this.leerChildren(doc, "DEDUCCIONES");
                //Console.WriteLine("OTROS PAGOS");
                //this.leerChildren(doc, "OTROSPAGOS");
                //Console.WriteLine("CONCEPTOS");
                //this.leerChildren(doc, "CONCEPTOS");               
                    double r = this.obtenerReintegroOtrosPagos(doc);
                    if (r == 0)
                    {
                        nominaViewModel.ReintegroISR = this.obtenerReintegro(doc);
                    }
                    else
                    {
                        nominaViewModel.ReintegroISR = r;
                    }                
                nominaViewModel.RetencionISR = this.obtenerISR(doc);
                nominaViewModels.Add(nominaViewModel);
            });
            return nominaViewModels;
        }

    }
}
