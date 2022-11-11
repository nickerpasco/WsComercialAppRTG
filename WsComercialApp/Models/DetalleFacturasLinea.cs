using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class DetalleFacturasLinea
    {
        public string NumeroDocumento { get; set; } 
        public string MontoTotal { get; set; } 
        public string MontoPendiente { get; set; } 
        public string FechaDocumento { get; set; } 
        public string FechaVencimiento { get; set; }  
        public Nullable<int> DiasVencido { get; set; }
        public Nullable<int> LetraNumeroUnico { get; set; }
    }
}