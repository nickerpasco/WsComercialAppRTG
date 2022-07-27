using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsComercialApp.Utils;

namespace WsComercialApp.Models
{
    public class SunatDataObject
    {
        public float TipoRespuesta{ get; set; }
        public String MensajeRespuesta{ get; set; }
        public String RUC{ get; set; }
        public String RazonSocial{ get; set; }
        public String TipoContribuyente{ get; set; }
        public String TipoDocumento { get; set; }
        public String NombreComercial{ get; set; }
        public String FechaInscripcion{ get; set; }
        public String FechaInicioActividades{ get; set; }
        public String EstadoContribuyente{ get; set; }
        public String CondicionContribuyente{ get; set; }
        public String DomicilioFiscal{ get; set; }
        public String SistemaEmisionComprobante{ get; set; }
        public String ActividadComercioExterior{ get; set; }
        public String SistemaContabilidiad{ get; set; }
        public String ActividadesEconomicas{ get; set; }
        public String ComprobantesPago{ get; set; }
        public String SistemaEmisionElectronica{ get; set; }
        public String EmisorElectronicoDesde{ get; set; }
        public String ComprobantesElectronicos{ get; set; }
        public String AfiliadoPLEDesde{ get; set; }
        public String Padrones{ get; set; }
        public List<ErrorObj> lstErrores = new List<ErrorObj>();
    }
}