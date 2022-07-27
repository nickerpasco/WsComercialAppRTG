using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_SpringUsuario
    {
        public string transaccionEstado { get; set; }
        public List<ObjErrorSpring> transaccionListaMensajes = new List<ObjErrorSpring>();
        public string auxFlgPreparado { get; set; }
        public string auxFlgValidado { get; set; }
        public string auxFlgNuevo { get; set; }
        public string auxFlgEditar { get; set; }
        public string auxFlgEliminar { get; set; }
        public string tipoUsuarioId { get; set; }
        public string documentoFiscal { get; set; }

        public string companiaCodigo { get; set; }

        public string usuario { get; set; }
        public string clave { get; set; }
        public string token { get; set; }
        public string transaccionMensajesCadena { get; set; }
        public bool auxFlgPreparadoBoolean { get; set; }
        public bool auxFlgValidadoBoolean { get; set; }


        public string URL_LOGIN { get; set; }
        public string URL_VALIDAR_RUC { get; set; }
        public string URL_GET_RUC { get; set; }
    }
}