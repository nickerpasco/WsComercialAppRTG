using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_Stock_Query
    {
        public string Lote { get; set; }
        public decimal? StockActual { get; set; }
    }
}