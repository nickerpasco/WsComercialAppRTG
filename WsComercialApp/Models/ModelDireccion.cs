using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class ModelDireccion
    {
        public int Persona { get; set; }
        public int Secuencia { get; set; }
        public string Direccion { get; set; }
        public string ReferenciasDireccion { get; set; }
        public string RutaDespacho { get; set; }
    }
}