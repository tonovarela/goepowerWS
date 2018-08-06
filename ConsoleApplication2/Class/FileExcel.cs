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


        public void AgregarRow(string nameField, String value)
        {
            Row row = new Row();
            row.Append(
                    ConstructCell(nameField, CellValues.String),
                    ConstructCell(value, CellValues.String));
            this._valores.Add(row);
        }


        public void SalvarExcel(string fileName,string nombreHoja)

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

                Row row = new Row();
                row.Append(
                    ConstructCell("WorkOrder", CellValues.String),
                    ConstructCell("Tienda", CellValues.String));

                sheetData.AppendChild(row);

                //Se agregan los valores
                this._valores.ForEach(x => sheetData.Append(x));

                worksheetPart.Worksheet.Save();
            }

        }


        public void SalvarExcel(string fileName)

        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Michelin" };
                sheets.Append(sheet);
                workbookPart.Workbook.Save();                
                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                
                Row row = new Row();
                row.Append(
                    ConstructCell("Valor", CellValues.String),
                    ConstructCell("Campo", CellValues.String));
                
                sheetData.AppendChild(row);

                //Se agregan los valores
                this._valores.ForEach(x=>sheetData.Append(x));

                worksheetPart.Worksheet.Save();
            }
            
        }

                

        private Cell ConstructCell(string value, CellValues dataType)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }

    }
}
