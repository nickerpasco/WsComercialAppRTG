using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class CabeceraLineasCredito
    {
        public string Cliente { get; set; }
        public int Persona { get; set; }
        public decimal? Libre { get; set; }
        public decimal? Usado { get; set; }
        public decimal? Linea { get; set; }
    }
}