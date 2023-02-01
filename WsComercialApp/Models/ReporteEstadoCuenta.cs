using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class ReporteEstadoCuenta
    {
		public string CompaniaSocio { get; set; }
		public string MonedaDocumentoSimbolo { get; set; }
		public string TipoDocumento { get; set; }
		public string TipoDocumentoDescripcion { get; set; }
		public string NumeroDocumento { get; set; }
		public string FechaDocumento { get; set; }
		public string FechaVencimiento { get; set; }
		public string ClienteNombre { get; set; }
		public string ClienteRUC { get; set; }
		public string MonedaDocumento { get; set; }
		public string ConceptoFacturacion { get; set; }
		public string TipoVenta { get; set; }
		public string TipoFacturacion { get; set; }
		public int ClienteNumero { get; set; }
		public string Busqueda { get; set; }
		public string Codigo01 { get; set; }
		public string Codigo02 { get; set; }
		public string Clasificacion { get; set; }
		public string Documento { get; set; }
		public int Vendedor { get; set; }
		public string FormadePago { get; set; }
		public string FormadePagoDesc { get; set; }
		public string NumeroInterno { get; set; }
		public string MontoAfecto { get; set; }
		public string MontoNoAfecto { get; set; }
		public string MontoImpuestoVentas { get; set; }
		public string MontoTotal { get; set; }
		public decimal MontoTotalDecimal { get; set; }
		public string MontoPagado { get; set; }
		public string MontoPendiente { get; set; }
		public decimal MontoPendienteDecimal { get; set; }
		public int Cobrador { get; set; }
		public string siaf_fechapago { get; set; }
		public string EstadoLetra { get; set; }
		public string LetraEstado { get; set; }
		public string LineaCreditoMoneda { get; set; }
		public string MontoPendienteGrupo { get; set; }
		public string MontoTotalByVendedorGrupo { get; set; }
		public decimal LineaCredito { get; set; }
		public int Dias { get; set; }
		public string Comentario { get; set; }
		public string ComentarioAdicional { get; set; }
		public string FormadePagoCliente { get; set; }
		public string PersonaContacto { get; set; }
		public string LineaCreditoFechaVencimiento { get; set; }
		public string TipoCliente { get; set; }
		public decimal MontoPercepcion { get; set; }
		public string PercepcionDocumento { get; set; }
		public string ProcesoImportacionFecha { get; set; }
		public string Sucursal { get; set; }
		public string DocumentoIdentidad { get; set; }
		public string Telefono { get; set; }
		public string Fax { get; set; }
		public string Direccion { get; set; }
		public string DescripcionCorta { get; set; }
		public string LetraNumeroUnico { get; set; }
		public string notacreditodocumento { get; set; }
		public string Departamento { get; set; }
		public string Provincia { get; set; }
		public string Distrito { get; set; }
		public string TelefonoContactoCob { get; set; }
		public string ContactoVenTelefono { get; set; }
		public string Contacto { get; set; }
		public string CorreoElectronico { get; set; }


		public string ValorComodin1 { get; set; }
		public string ValorComodin2 { get; set; }
		public string ValorComodin3 { get; set; }
		public string ValorComodin4 { get; set; }
		public string ValorComodin5 { get; set; }
		public string ValorComodin6 { get; set; }
		public string ValorComodin7 { get; set; }
		public string ValorComodin8 { get; set; }
		public string ValorComodin9 { get; set; }
		public string ValorComodin10 { get; set; }
	}
}