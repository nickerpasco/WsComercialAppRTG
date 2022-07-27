using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class ModelParametros
    {
        public string CompaniaCodigo { get; set; }
        public string AplicacionCodigo { get; set; }
        public string ParametroClave { get; set; }
        public string TipodeDatoFlag { get; set; }
        public decimal? Numero { get; set; }
        public string Texto { get; set; }
        public DateTime? Fecha { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public string Estado { get; set; }
        public string Explicacion { get; set; }
        public string DescripcionParametro { get; set; }
    }
}