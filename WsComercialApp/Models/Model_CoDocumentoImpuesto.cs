using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_CoDocumentoImpuesto
    {
        public string CompaniaSocio { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoRegistro { get; set; }
        public string Impuesto { get; set; }
        public Nullable<decimal> Porcentaje { get; set; }
        public int? PkFalso { get; set; }
        public Nullable<decimal> Monto { get; set; }
    }
}