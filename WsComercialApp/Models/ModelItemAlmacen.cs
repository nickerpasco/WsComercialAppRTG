using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class ModelItemAlmacen
    {
         
        public string ItemDescripcion { get; set; } 
        public string Item { get; set; } 
        public string AlmacenDescripcion { get; set; } 
        public string AlmacenCodigo { get; set; } 
        public Nullable<decimal> Stock { get; set; }
    }
}