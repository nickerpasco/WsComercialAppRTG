using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class ObjToken
    {
        public string Token { get; set; }

        public DateTime TokenFechaExpiracion { get; set; }
        public String TokenFechaExpiracionString { get; set; }
    }
}