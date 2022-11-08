using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_PersonaMast
    {
        public int Persona { get; set; }
        public string Busqueda { get; set; }
        public string ComentarioPersona { get; set; }
        public string TipoFacturacionNoAfectoFlag { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string TipoPersona { get; set; }
        public string EsCliente { get; set; }
        public string EsProveedor { get; set; }
        public string EsEmpleado { get; set; }
        public string TipoDocumentoPersona { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoCliente { get; set; }
        public string CentroCosto { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string TipoFacturacion { get; set; }
        public string FormadePago { get; set; }
        public string FormaFacturacion { get; set; }
        public int? Vendedor { get; set; }
        public string ConceptoFacturacion { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string PersonaAnt { get; set; }
        public string DocumentoFiscal { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Estado { get; set; }
        public string BloquearVendedor { get; set; }



        public string VentaEquipo { get; set; }

        /// <summary>
        /// DATOS DEL CLIENTE
        /// </summary>
        ///  


        public string TipoVenta { get; set; }

        public string TransportistaDescripcion { get; set; }
        public int? IdTransportista { get; set; }
        public int DiasCredito { get; set; }
        public int DireccionSecuencia { get; set; }
        public string TipoCredito { get; set; }
    }
}