using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class CO_OperacionCanjeDetalle_Model
    {
        public string CompaniaSocio { get; set; }
        public string Moneda { get; set; }
        public int OperacionCanjeNumero { get; set; }
        public int Linea { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public string InputOutputFlag { get; set; }
        public Nullable<decimal> MontoComision { get; set; }
        public Nullable<decimal> Dias { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public Nullable<System.DateTime> FechaEmisionDocumento { get; set; }
    }
}