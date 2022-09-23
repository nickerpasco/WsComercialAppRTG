using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_CO_DocumentoDetalle
    {
		public decimal? PorcentajeDescuento01 { get; set; }
		public decimal? MontoDescuento { get; set; }
		public string TransferenciaGratuitaFlag { get; set; }
		public string CompaniaSocio { get; set; }
		public string TipoDocumento { get; set; }
		public string NumeroDocumento { get; set; }
		public string Comentario { get; set; }
		public string Origen { get; set; }
		public int Linea { get; set; }
		public int PrecioNumeroRegistro { get; set; }
		public string TipoDetalle { get; set; }
		public string ItemCodigo { get; set; }
		public string Lote { get; set; }
		public string Descripcion { get; set; }
		public string ImageFile { get; set; }
		public string UnidadCodigo { get; set; }
		public decimal? CantidadPedida { get; set; }
		public decimal? CantidadEntregada { get; set; }
		public decimal? PrecioUnitario { get; set; }
		public decimal? PrecioUnitarioOriginal { get; set; }
		public decimal? PrecioUnitarioFinal { get; set; }
		public decimal? Monto { get; set; }
		public decimal? MontoFinal { get; set; }
		public string AlmacenCodigo { get; set; }
		public string FlujodeCaja { get; set; }
		public string Estado { get; set; }
		public decimal? PrecioUnitarioFlete { get; set; }
		public decimal? PrecioUnitarioFleteOriginal { get; set; }
		public decimal? MontoFlete { get; set; }
		public int? ClienteDireccionDespacho { get; set; }
		public string RutaDespacho { get; set; }
		public string AfectoImpuestoVentasFlag { get; set; }
	}
}