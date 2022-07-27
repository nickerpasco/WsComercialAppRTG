using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_Sy_Preferences
    {
        public string Usuario { get; set; }
        public string Preference { get; set; }
        public string AplicacionCodigo { get; set; }
        public string ValorUnido { get; set; }
        public string TipoValor { get; set; }
        public string ValorString { get; set; }
        public Nullable<int> ValorNumero { get; set; }
        public Nullable<System.DateTime> ValorFecha { get; set; }
        public Nullable<System.DateTime> UltimaFechaModif { get; set; }
        public string UltimoUsuario { get; set; }
    }
}