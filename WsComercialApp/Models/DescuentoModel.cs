using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class DescuentoModel
    {
        public string TipoDescuento { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
        public decimal? Desde { get; set; }
        public decimal? Hasta { get; set; }
    }
}