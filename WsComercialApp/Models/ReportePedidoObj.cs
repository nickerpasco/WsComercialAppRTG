using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{ 
    public class ReportePedidoObj
    {
		public string LogoCompania { get; set; }
		public string Nombre { get; set; }
		public string RUC { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Celular { get; set; }
		public string Email { get; set; }
		public int Linea { get; set; }
		public string NumeroCotizacion { get; set; }
		public string Cliente { get; set; }
		public string Fecha { get; set; }
		public string TelefonoCliente { get; set; }
		public string Moneda { get; set; }
		public string DireccionCliente { get; set; }
		public string Entrega { get; set; }
		//public string Telefono { get; set; }
		public string Validez { get; set; }
		public string Referencia { get; set; }
		public string Item { get; set; }
		public string Descripcion { get; set; }
		public string Cantidad { get; set; }
		public string PrecioUnitario { get; set; }
		public string Total { get; set; }
		public string TotalPagar { get; set; }
		public string ValorUnitario { get; set; }
		public string Descuento { get; set; }
		public string SubTotal { get; set; }
		public string IGV { get; set; }
		//public string Total { get; set; }
		public string CondicionEntrega { get; set; }
		public string CondicionGarantia { get; set; }
		public string Condicion { get; set; }
		public string Observacion { get; set; }
		public string EquipoVenta { get; set; }
		public string SitioWeb { get; set; }
		public string Usuario { get; set; }
		public string UsuarioCorreo { get; set; }
	}
}