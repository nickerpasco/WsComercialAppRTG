using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsComercialApp.Utils;

namespace WsComercialApp.Models
{
    public class ModelTransac_CO_Pedido
    {
        public ErrorObj objerror = new ErrorObj();

        public string BASE64Certificado { get; set; }
        public string NumeroDocumento { get; set; }
    }
}