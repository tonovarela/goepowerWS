using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public class Excel
    {
        private Application xlApp;
        private Workbook xlWorkBook;
        private Worksheet xlWorkSheet;
        private object misValue;

        public Excel()
        {
            this.xlApp = new Application();
            this.misValue = System.Reflection.Missing.Value;
            this.xlApp.DisplayAlerts = false;
            this.xlWorkBook = this.xlApp.Workbooks.Add(misValue);
            this.xlWorkSheet = (Worksheet)this.xlWorkBook.Worksheets.get_Item(1);

            this.xlWorkSheet.Cells[1, 1] = "Campo  ";
            this.xlWorkSheet.Cells[1, 2] = "Valor";

        }
        private int getIndexLastRow()
        {
            return xlWorkSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
        }
        public void AgregarRow(string nameField, String value)
        {
            int _rowIndex = this.getIndexLastRow();
            this.xlWorkSheet.Cells[_rowIndex + 1, 1] = "'" + nameField;
            this.xlWorkSheet.Cells[_rowIndex + 1, 2] = value;
        }
        public void SalvarExcel(String destino)
        {

            
            this.xlWorkBook.SaveAs(destino, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,Type.Missing, Type.Missing);
            this.xlWorkBook.Close();
            this.xlApp.Quit();
            Marshal.ReleaseComObject(this.xlWorkSheet);
            Marshal.ReleaseComObject(this.xlWorkBook);
            Marshal.ReleaseComObject(this.xlApp);
        }



    }
}
