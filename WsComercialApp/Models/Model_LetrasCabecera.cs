using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class Model_LetrasCabecera
    {
		public string EstadoString { get; set; }
		public string ColorEstado { get; set; }
		public string CompaniaSocio { get; set; }
		public string PedidoEstadoCodigo { get; set; }
		public string LabelMes { get; set; }
		public string LabelHora { get; set; }
		public int IdPersona { get; set; }
		public int ClienteDireccionSecuencia { get; set; }
		public int DiasCanje { get; set; }
		public int LetrasCantidadValorReal { get; set; }
		public DateTime? FechaBase { get; set; }
		public DateTime? FechaMaxima { get; set; }
		public string NombreCompleto { get; set; }
		public int OperacionCanjeNumero { get; set; }
		public int? Vendedor { get; set; }
		public string Comentarios { get; set; }
		public string Direccion { get; set; }
		public string LetrasCantidad { get; set; }
		public string MonedaDocumento { get; set; }
	}
}