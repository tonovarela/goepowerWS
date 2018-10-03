using ConsoleApplication2.DAO;
using ConsoleApplication2.DAO.Goepower;
using ConsoleApplication2.DAO.Lito;
using ConsoleApplication2.Model;
using DocumentFormat.OpenXml.Spreadsheet;
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

        private string _directorio = @"\\192.168.5.28\EstadoDeCuenta";


         private string _directorio_bitacora;

        private FileInfo[] _archivos;
        private FileExcel _bitacoraExcel;   
        public Lector()
        {            
            this._bitacoraExcel = new FileExcel();
            this._archivos = new DirectoryInfo(_directorio).GetFiles("*.exp");
            this._directorio_bitacora = "Z:\\kumon\\reportes";
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
                File.Delete(archivo.FullName);
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

    }
}
