using ConsoleApplication2.ViewModel.Crece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Model.Crece
{
    public class OrdenViewModel
    {

        public string Tienda { get; set; }

        public string  NumeroOrden { get; set; }

        public string  Status { get; set; }

        public String  TipoEnvio { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaEntrega { get; set; }

        public  ClienteViewModel Cliente { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string DireccionEnvio { get; set; }

        public string Calle { get; set; }

        public string Ciudad { get; set; }

        public string Estado { get; set; }

        public string CodigoPostal { get; set; }


      public  IEnumerable<ItemViewModel> Items { get; set; }



        public void Imprimir()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Numero Tienda {this.Tienda}");
            Console.WriteLine($"Numero Orden {this.NumeroOrden}");
            Console.WriteLine($"Estatus {this.Status}");
            Console.WriteLine($"Tipo Envio {this.TipoEnvio}");
            Console.WriteLine($"Fecha entrega {this.FechaEntrega}");
            Console.WriteLine($"Fecha Registro {this.FechaRegistro}");
            Console.WriteLine($"Email {this.Email}");
            Console.WriteLine($"Direccion de Envio {this.DireccionEnvio}");
            Console.WriteLine($"Calle {this.Calle}");
            Console.WriteLine($"Codigo Postal {this.CodigoPostal}");
            Console.WriteLine($"Ciudad {this.Ciudad}");
            Console.WriteLine($"Estado {this.Estado}");
            Console.WriteLine($"Telefono {this.Telefono}");
            Console.WriteLine($"Items {this.Items.Count()}");

            Console.WriteLine("-------------------------------------");
        }


    }
}
