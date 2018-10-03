using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public class FileExcel
    {
            
        private List<Row> _valores;


        public FileExcel()
        {
            _valores = new List<Row>();
        }


        public void AgregarRow(string nameField, string value)
        {
            Row row = new Row();
            row.Append(
                    ConstructCell(nameField, CellValues.String),
                    ConstructCell(value, CellValues.String));
            this._valores.Add(row);
        }

       

        public void SalvarExcel (string fileName,string nombreHoja,Row cabecera)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = nombreHoja };
                sheets.Append(sheet);
                workbookPart.Workbook.Save();
                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                //Row row = new Row();
                //row.Append(
                //    ConstructCell("WorkOrder", CellValues.String),
                //    ConstructCell("Tienda", CellValues.String));
                
                sheetData.AppendChild(cabecera);

                //Se agregan los valores
                this._valores.ForEach(x => sheetData.Append(x));

                worksheetPart.Worksheet.Save();
            }
        }

        
  

        public void SalvarExcel(string fileName,string sheetName)

        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = sheetName };
                sheets.Append(sheet);
                workbookPart.Workbook.Save();
                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());              
                //Se agregan los valores
                this._valores.ForEach(x => sheetData.Append(x));
                worksheetPart.Worksheet.Save();
            }

        }


        public void CrearArchivoPrimerColumna(String File)
        {
            string fileFullPath = File;           
            // Specify your column index and then convert to letter format
            int columnIndex = 1;
            string columnName = GetExcelColumnName(columnIndex);
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileFullPath, true))
           {
                Sheet sheet = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().FirstOrDefault();
                if (sheet != null)
                {
                    WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id.Value);
                    // Get all the rows in the workbook
                    IEnumerable<Row> rows = worksheetPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>();
                    // Ensure that there are actually rows in the workbook
                    if (rows.Count() == 0)
                    {
                        return;
                    }

                    rows.ToList().ForEach(row =>
                    {
                        var cells = row.Elements<Cell>().Where(x => new String(x.CellReference.Value.Where(Char.IsLetter).ToArray()) == columnName).ToList();
                        cells.ForEach(cell =>
                        {
                            try
                            {
                                string valor = this.GetCellValue(document, cell);
                                Row r = new Row();
                                r.Append(new Cell()
                                {
                                    CellValue = new CellValue(valor),
                                    DataType = new EnumValue<CellValues>(CellValues.String)
                                });
                                this._valores.Add(r);

                            }
                            catch (NullReferenceException)
                            {

                            }

                        });

                    });                    
                 
                }

                
            }
            if (System.IO.File.Exists(File))
            {
                System.IO.File.Delete(File);
                this.SalvarExcel(File, "Kumon");
                Console.WriteLine("Archivo con una sola columna creado " + File);                
            }
            
        }

        private  string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }



        public Cell ConstructCell(string value, CellValues dataType)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }

    }
}
