using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class InfoCliente_LCredito
    {
		public int Persona { get; set; }
		public string Busqueda { get; set; }
		public string DocumentoFiscal { get; set; }
		public string Clasificacion { get; set; }
		public string FormaPago { get; set; }
		public string Moneda { get; set; }
		public decimal? Monto { get; set; }
		public DateTime? FechaVigenciaDesde { get; set; }
		public DateTime? FechaVigenciaHasta { get; set; }
		public List<Model_MontosLineaCredito> lst = new List<Model_MontosLineaCredito>();
	}
}