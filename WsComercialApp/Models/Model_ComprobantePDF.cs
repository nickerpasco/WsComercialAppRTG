using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_ComprobantePDF
    {
		public string MonedaDescripcion { get; set; }
		public string DocumentoDescripcion { get; set; }
		public string DocumentoFiscal { get; set; }
		public string DireccionComun { get; set; }
		public string LogoFile { get; set; }
		public string Telefono1 { get; set; }
		public string CompaniaSocio { get; set; }
		public string TipoDocumento { get; set; }
		public string EmpresaNombre { get; set; }
		public string NumeroDocumento { get; set; }
		public DateTime FechaDocumento { get; set; }
		public DateTime FechaVencimiento { get; set; }
		public int ClienteNumero { get; set; }
		public string ClienteRUC { get; set; }
		public string ClienteNombre { get; set; }
		public string ClienteDireccion { get; set; }
		public string MonedaDocumento { get; set; }
		public string FormaPagoDescripcion { get; set; }
		public string FechaEmision { get; set; }
		public string OPgravada { get; set; }
		public string OPInafecta { get; set; }
		public string MontoNoAfecto { get; set; }
		public string MontoImpuestoVentas { get; set; }
		public string MontoImpuestos { get; set; }
		public string OPDescuentoGlobal { get; set; }
		public string ImporteTotal { get; set; }
		public string OPExonerada { get; set; }
		public string OPExportación { get; set; }
		public string Telefono2 { get; set; }
		public string Telefono3 { get; set; }
		public string Fax1 { get; set; }
		public string Fax2 { get; set; }
		public string ClienteReferencia { get; set; }
		public string Comentarios { get; set; }
		public string paginaweb { get; set; }
		public string Direccion { get; set; }
		public string DireccionCLiente { get; set; }
		public string TipoVenta { get; set; }
		public string FormadePago { get; set; }
		public int Vendedor { get; set; }
		public string Telefono { get; set; }
		public string CorreoElectronico { get; set; }
		public string TipoDocumentoPersona { get; set; }
		public string CorreoEmisor { get; set; }
		public string Distrito { get; set; }
		public string Provincia { get; set; }
		public string Departamento { get; set; }
		public string FEHAshCode { get; set; }
		public string CodEstableSunat { get; set; }
		public string DescripcionCorta { get; set; }
		public string DescripcionLarga { get; set; }
		public string Direccionadicional { get; set; }
		public string NotaCreditoDocumento { get; set; }
		public string tipofacturacion { get; set; }
		public int Linea { get; set; }
		public string ItemCodigo { get; set; }
		public string Descripcion { get; set; }
		public string UnidadCodigo { get; set; }
		public string CantidadPedida { get; set; }
		public string porcentajedescuento01 { get; set; }
		public string PrecioUnitario { get; set; }
		public string preciounitariooriginal { get; set; }
		public string preciounitariogratuito { get; set; }
		public string igvexoneradoflag { get; set; }
		public string transferenciagratuitaflag { get; set; }
		public string CodigoQR { get; set; }
		public string MontoGratuito { get; set; }
		public string IGV { get; set; }
		public string MontoLetra { get; set; }
	}
}