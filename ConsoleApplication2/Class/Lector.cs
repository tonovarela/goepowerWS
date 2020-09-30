using ConsoleApplication2.Class.sbk;
using ConsoleApplication2.DAO;
using ConsoleApplication2.DAO.Goepower;
using ConsoleApplication2.DAO.Lito;
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
    public class Lector
    {

      public  bool borrarArchivo = false;
       private string _directorio = @"\\192.168.2.218\movimientos";
       // string _directorio= @"C:\Desarrollo\Movimientos";

        private string _directorio_bitacora;
        protected FileInfo[] _archivos;
        private FileExcel _bitacoraExcel;   
        public Lector()
        {            
            this._bitacoraExcel = new FileExcel();
            Console.WriteLine($"Leyendo el directorio  {this._directorio}");
            //this._archivos = new DirectoryInfo(_directorio).GetFiles("*.exp");

            this._directorio_bitacora = "Z:\\kumon\\reportes";
            //this._directorio_bitacora = @"C:\Desarrollo\Bitacora";
        }


        public void moverArchivos()
        {
            if (this.borrarArchivo)
            {
                this._archivos.ToList().ForEach(x =>File.Delete(x.FullName));
            }
            else
            {
                string directorioErrores = "Z:\\kumon\\reportesError";
                this._archivos.ToList().ForEach(x =>File.Move(x.FullName, $"{directorioErrores}\\{x.Name}"));
            }
            
        }
        protected string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = string.Empty;
            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
            }
            return value;
        }
        public List<TransaccionDTO> LeerOrdenesExcel()
        {
            this._archivos = new DirectoryInfo(_directorio).GetFiles("*.xlsx");
            List<TransaccionDTO> transacciones = new List<TransaccionDTO>();
            foreach (FileInfo archivo in this._archivos)
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(archivo.FullName, false))
                {
                    Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                    foreach (Row row in rows)
                    {
                        if (row.RowIndex.Value >= 3)
                        {
                            Cell cells = row.Descendants<Cell>().Skip(1).First();
                            string concepto=GetValue(doc, cells);
                            Transaccion tr = new Transaccion(concepto);                            
                            if (!tr.esTransaccion())
                                continue;

                            VentaDAO dao = new VentaDAO();
                            OrdenDAO _ordenDao = new OrdenDAO();
                            string tienda = "Sin tienda";
                               //dao.obtenerTienda(tr.WorkOrder);
                            _ordenDao.ActualizaPago(tr.WorkOrder);
                            transacciones.Add(new TransaccionDTO()
                            {
                                WorkOrden = tr.WorkOrder,
                                Tienda = tienda
                            }
                                               );
                            this._bitacoraExcel.AgregarRow(tr.WorkOrder, tienda);
                            tr.Imprimir();
                        }
                    }
                }
                String fecha = String.Format("{0:dd-MM-yyyy_HH_mm_ss}", DateTime.Now);
                //File.Move(archivo.FullName, $"{_directorio_bitacora}\\{fecha}-{archivo.Name}");
            }

            //if (transacciones.Count > 0)
            //{
            //    String fecha = String.Format("{0:dd-MM-yyyy_HH_mm_ss}", DateTime.Now);
            //    String mes = String.Format("{0:MMMM}", DateTime.Now);
            //    String directory = $@"{this._directorio_bitacora}\{mes}";
            //    if (!Directory.Exists(directory))
            //    {
            //        Directory.CreateDirectory(directory);
            //    }
            //    Row cabecera = new Row();
            //    cabecera.Append(
            //        _bitacoraExcel.ConstructCell("WorkOrder", CellValues.String),
            //        _bitacoraExcel.ConstructCell("Tienda", CellValues.String));

            //    _bitacoraExcel.SalvarExcel($@"{directory}\{fecha}-{DateTime.Now.Millisecond}.xlsx", "informacion", cabecera);
            //}
            return transacciones;
                             
            }
        public List<TransaccionDTO> getOrdenes()
        {
            List<TransaccionDTO> transacciones = new List<TransaccionDTO>();
            foreach (FileInfo archivo in this._archivos)
            {
                string[] lines = File.ReadAllLines(archivo.FullName);
                foreach (string line in lines)
                {                                       
                    Transaccion tr = new Transaccion(line);
                    if (!tr.esTransaccion())
                        continue;
                    VentaDAO dao = new VentaDAO();
                    OrdenDAO _ordenDao = new OrdenDAO();                    
                    string tienda = dao.obtenerTienda(tr.WorkOrder);
                    _ordenDao.ActualizaPago(tr.WorkOrder);
                    transacciones.Add(new TransaccionDTO()
                    {
                        WorkOrden = tr.WorkOrder,
                        Tienda = tienda
                    }
                                       );
                    this._bitacoraExcel.AgregarRow(tr.WorkOrder, tienda);
                    tr.Imprimir();
                }
                //File.Delete(archivo.FullName);
                File.Move(archivo.FullName, $"{_directorio_bitacora}\\{archivo.Name}.exp");
            }
            if (transacciones.Count > 0)
            {
                String fecha = String.Format("{0:dd-MM-yyyy_HH_mm_ss}", DateTime.Now);
                String mes = String.Format("{0:MMMM}", DateTime.Now);
                String directory = $@"{this._directorio_bitacora}\{mes}";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);                
                }
                Row cabecera = new Row();
                cabecera.Append(
                    _bitacoraExcel.ConstructCell("WorkOrder", CellValues.String),
                    _bitacoraExcel.ConstructCell("Tienda", CellValues.String));

                _bitacoraExcel.SalvarExcel($@"{directory}\{fecha}.xlsx", "informacion",cabecera);
            }

            return transacciones;
        }




        public List<OrdenCargaDTO> LeerOrdenesSBK()
        {
        
            List<OrdenCargaDTO> ordenesCarga = new List<OrdenCargaDTO>();
            var s = new FileStream("C:\\Desarrollo\\sbk.xlsx", FileMode.Open, FileAccess.Read);

            #region Lectura del Excel
            List<LineExcel> lineas = new List<LineExcel>();
            using (ExcelPackage package = new ExcelPackage(s))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                for (int i = 3; i <= sheet.Dimension.End.Row; i++)
                {
                    lineas.Add(new LineExcel()
                    {
                        Username = sheet.Cells[i, 2].Value.ToString(),
                        Firstname = sheet.Cells[i, 3].Value.ToString(),
                        Lastname = sheet.Cells[i, 4].Value.ToString(),
                        Address1 = sheet.Cells[i, 5].Value == null ? "" : sheet.Cells[i, 5].Value.ToString(),
                        Address2 = sheet.Cells[i, 6].Value == null ? "" : sheet.Cells[i, 6].Value.ToString(),
                        Address3 = sheet.Cells[i, 7].Value == null ? "" : sheet.Cells[i, 7].Value.ToString(),
                        PostalCode = sheet.Cells[i, 8].Value.ToString(),
                        Email = sheet.Cells[i, 9].Value.ToString(),
                        Phone = sheet.Cells[i, 10].Value.ToString(),
                        City = sheet.Cells[i, 11].Value.ToString(),
                        ProductoID = sheet.Cells[i, 12].Value.ToString(),
                        JobName = sheet.Cells[i, 13].Value.ToString(),
                        Quantity = Int32.Parse(sheet.Cells[i, 14].Value.ToString()),
                        ID_BillingMethod = Int32.Parse(sheet.Cells[i, 15].Value.ToString()),
                        ShippingPrice = Decimal.Parse(sheet.Cells[i, 16].Value.ToString()),
                        UserNote = sheet.Cells[i, 17].Value.ToString()
                    });

                }
            }
            var result = lineas.GroupBy(x => x.Username, (key, data) => new { key, data = data.ToList() }).ToList();
            #endregion

            #region Llenado de DTO
            result.ForEach(z =>
            {
                var o = z.data.FirstOrDefault();
                OrdenCargaDTO or = new OrdenCargaDTO()
                {
                    Username=o.Username,
                    Lastname=o.Lastname,
                    Address1=o.Address1,
                    Address2=o.Address2,
                    Address3=o.Address3,
                    City=o.City,
                    Email=o.Email,
                    Firstname=o.Firstname,
                    Phone=o.Phone,
                    PostalCode=o.PostalCode,
                    Id_BillingMethod=o.ID_BillingMethod,
                     ShipingPrice=o.ShippingPrice
                     
                };
                or.jobs = new List<JobCargarDTO>();                
                //Console.WriteLine(z.key);
                z.data.ForEach(e =>
                {                    
                    or.jobs.Add(new JobCargarDTO()
                    {
                         Name=e.JobName,
                         ProductID=Int32.Parse(e.ProductoID),
                         Quantity=e.Quantity   ,
                          UserNote=e.UserNote
                    });
                 //   Console.WriteLine(e.ProductoID);
                });

                ordenesCarga.Add(or);
            });
            #endregion

            return ordenesCarga;

            
        }

    }
}
