using ConsoleApplication2.Class.Excepciones;
using ConsoleApplication2.Model;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public class LectorOrdenesCreateOrden
    {
        protected FileInfo[] _archivos;
        private List<OrdenCargaDTO> _ordenes;
        private List<JobCargarDTO> _jobs;
        public LectorOrdenesCreateOrden()
        {
             _ordenes = new List<OrdenCargaDTO>();
        }




       private void LeerJob(ExcelWorksheet worksheet,int row,int indice)
        {
            if (worksheet.Cells[row, indice].Value != null )
            {
                JobCargarDTO job = new JobCargarDTO()
                {
                    ProductID = Int32.Parse(worksheet.Cells[row, indice].Value.ToString().Trim()),
                    Name = worksheet.Cells[row, indice + 1].Value.ToString().Trim(),
                    Quantity = Int32.Parse(worksheet.Cells[row, indice + 2].Value.ToString().Trim())
                };
                if (job.Quantity > 0)
                {
                    this._jobs.Add(job);
                }
                
            }
        }

        public List<OrdenCargaDTO> obtenerOrdenes()
        {
            return this._ordenes;
        }
        public void LeerOrdenes(string directorio)
        {
            this._archivos = new DirectoryInfo(directorio).GetFiles("*.xlsx");
            foreach (FileInfo archivo in this._archivos)
            {             
                using (ExcelPackage package = new ExcelPackage(archivo))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    var start = worksheet.Dimension.Start;
                    var end = worksheet.Dimension.End;
                    int columnaInicialProd = 11;
                    int factor = (end.Column - columnaInicialProd) / 3;                    
                    for (int row = start.Row + 1 ; row <= end.Row; row++)
                    {

                        OrdenCargaDTO orden;
                        try
                        {
                            if (worksheet.Cells[row, 2].Value ==null)
                            {
                                continue;                                
                            }
                            Console.WriteLine($"Row :{row} {worksheet.Cells[row, 2].Value.ToString().Trim()}");
                             orden = new OrdenCargaDTO()
                            {
                                Username = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                 Firstname = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                 Lastname = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                 Address1 = worksheet.Cells[row, 5].Value.ToString().Trim(),                                 
                                 Address2 = worksheet.Cells[row, 6].Value == null ? " " : worksheet.Cells[row, 6].Value.ToString().Trim(),
                                 Address3 = worksheet.Cells[row, 7].Value == null ? " " : worksheet.Cells[row, 7].Value.ToString().Trim(),
                                 PostalCode = worksheet.Cells[row, 8].Value.ToString().Trim(),
                                 Email = worksheet.Cells[row, 9].Value.ToString().Trim(),
                                 Phone = worksheet.Cells[row, 10].Value == null ? " " : worksheet.Cells[row, 10].Value.ToString().Trim(),                                 
                                 City = worksheet.Cells[row, 11].Value == null ? "." : worksheet.Cells[row, 11].Value.ToString().Trim(),

                             };
                            this._jobs = new List<JobCargarDTO>();
                            //Leer los jobs por bloques de 3
                            for (int i = 0; i < factor; i++)
                                this.LeerJob(worksheet, row, 12 + (i * 3));

                            orden.jobs =this._jobs;
                            this._ordenes.Add(orden);
                        }                        
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception en la aplicacion");
                        }                        
                    }                                        
                }
            }



            
        }




                

    }
}
