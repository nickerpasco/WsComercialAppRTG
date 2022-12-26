using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class CabeceraLineasCreditoDetalle
    {
        public string NumeroDocumento { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int? Dias { get; set; }
        public string FormaPago { get; set; }
        public decimal? Pendiente { get; set; }
        public string LetraNumeroUnico { get; set; }
    }
}