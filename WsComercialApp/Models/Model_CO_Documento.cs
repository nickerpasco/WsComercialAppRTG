using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_CO_Documento
    {
		public int? Vendedor { get; set; }
		public string CompaniaSocio { get; set; }
		public string TipoDocumento { get; set; }
		public string NumeroDocumento { get; set; }
		public int? ClienteNumero { get; set; }
		public string ClienteRUC { get; set; }
		public string TipoDocumentoPedidoCLiente { get; set; }
		public string ClienteNombre { get; set; }
		public string EstablecimientoCodigo { get; set; }
		public string TipoVenta { get; set; }
		public string FechaDocumento { get; set; }
		public decimal? MontoTotal { get; set; }
		public decimal? MontoAfecto { get; set; }
		public decimal? MontoNoAfecto { get; set; }
		public decimal? MontoFlete { get; set; }
		public decimal? MontoImpuestos { get; set; }
		public decimal? MontoImpuestoVentas { get; set; }
		public string ClienteDireccion { get; set; } 
		public int? ClienteDireccionDespacho { get; set; }
		public int? TransportistaProvincia { get; set; }
		public string TransportistaProvinciaDescripcion { get; set; }
		public string ClienteReferencia { get; set; }
		public decimal? TipodeCambio { get; set; }
		public string TipoFacturacion { get; set; }
		public string CreditoFlag { get; set; }
		public string FormaPagoDescripcion { get; set; }
		public string TipoDocumentoDescripcion { get; set; }
		public string PedidoEstadoCodigo { get; set; }
		public string ColorEstado { get; set; }
		public string FormadePago { get; set; }
		public string MonedaDocumento { get; set; }
		public string FormaFacturacion { get; set; }
		public string ConceptoFacturacion { get; set; }
		public string TipoCliente { get; set; }
		public string RecojoFlag { get; set; }
		public string RutaDespachoDescripcion { get; set; }
		public string CanalVenta { get; set; }
		public string ComercialPedidoFechaRequeridaString { get; set; }
		public string Estado { get; set; }
		public string FechaVencimiento { get; set; }
		public decimal? MontoDescuentos { get; set; }
		public string RutaDespacho { get; set; } 
		public string TipoDocumentoVentaDescripcion { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string FechaNacimiento { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }
		public string DireccionReferencia { get; set; }
		public string DocumentoFiscal { get; set; }
		public string Documento { get; set; }
		public string Nombres { get; set; }
		public string CorreoElectronico { get; set; }
		public string ApellidoPaternoContacto { get; set; }
		public string ApellidoMaternoContacto { get; set; }
		public string NombreContacto { get; set; }
		public string TelefonoContacto { get; set; }
		public string TipoDocumentoPersona { get; set; }
		public string DireccionContacto { get; set; }
		public string NombresCompletoContacto { get; set; }
		public string FechaNacimientoContacto { get; set; }
		public string DocumentoFiscalContacto { get; set; }
		public string Comentarios { get; set; }
		public decimal? MontoPercepcion { get; set; }
		public decimal? PercepcionPorcentaje { get; set; }
		public string Departamento { get; set; }
		public string Provincia { get; set; }
		public string CodigoPostal { get; set; }
		public string Cadena { get; set; }
		public string UnidadNegocio { get; set; }
		public string DepartamentoCod { get; set; }
		public string CodigoPostalCod { get; set; }
		public string ProvinciaCod { get; set; }
		public string Sucursal { get; set; }
		public string ComercialPedidoNumero { get; set; }
		public string NumeroInterno { get; set; }
		public string FechaDocParaQR { get; set; }
		public string CodigoQR { get; set; }
		public string FEHashCode { get; set; }
		public string AfectoPercepcionIGVFlag { get; set; }
		public string FlagImpresionPercepcionCampo { get; set; }
		public string MonedaString { get; set; }
		public string EstadoString { get; set; }

		public string LabelMes { get; set; }
		public string LabelHora { get; set; }
	}
}