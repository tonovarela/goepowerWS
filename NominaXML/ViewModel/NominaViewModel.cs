using System;

namespace NominaXML.ViewModel
{
    public  class NominaViewModel
    {


        public string UUID { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaInicialPago { get; set; }
        public DateTime FechaFinalPago { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public string RFC { get; set; }

        public string Nombre { get; set; }
        public double RetencionISR { get; set; }

        public double ReintegroISR { get; set; }

        public string path { get; set; }



    }
}
