using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsComercialApp.Utils;

namespace WsComercialApp.Models
{
    public class ModelUsuario
    {
		public string Busqueda { get; set; }
		public string Telefono { get; set; }
		public string SesionFlag { get; set; }
		public string PrimerNombre { get; set; }
		public string UnidadNegocioAsignada { get; set; }
		public string Documento { get; set; }
		public string Modalidad { get; set; }
		public string PlacaNumero { get; set; }
		public string FlagTipoTransporte { get; set; }
		public string SerieNumero { get; set; }
		public string GuiaNumero { get; set; }
		public string RutaDespacho { get; set; }
		public string RutaDescripcion { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public DateTime? FechaInicioPeriodo2 { get; set; }
		public DateTime? FechaFinPeriodo2 { get; set; }
		public string CompaniaSocio { get; set; }
		public string Sucursal { get; set; }
		public int? Persona { get; set; }
		public int? PuedeHacerDescuento { get; set; }
		public string VentaEquipo { get; set; }
		public int? PuedeFacturar { get; set; }
		public int? PuedeCambiarPrecio { get; set; }
		public int? IdVehiculo { get; set; }
		public string Usuario { get; set; }
		public string Clave { get; set; }
		public string Nombre { get; set; }
		public string UsuarioPerfil { get; set; }
		public string Estado { get; set; }
		public string Token { get; set; }
		public string Año { get; set; }
		public string Mes { get; set; }
		public string Planta { get; set; }


		public string TransportistaDescripcion { get; set; }
		public int? IdTransportista { get; set; }
		public int? LineaVehiculo { get; set; }
		public decimal? TipoCambioValor { get; set; }
		public string VehiculoDescripcion { get; set; }
		public string Brevete { get; set; }
		public string TipoDocumentoConductor { get; set; }
		public string DocumentoCondutor { get; set; }

		public String DocumentoTransportista { get; set; }
		public String NombreTransportista { get; set; }
		public String DireccionTransportista { get; set; }

		public string AñoPeriodo2 { get; set; }
		public string MesPeriodo2 { get; set; }

		public string EstadoEmpleado { get; set; }
		public DateTime? FechaCeseEmpleado { get; set; }



		public DateTime TokenFechaExpiracion { get; set; }
		public String TokenFechaExpiracionString { get; set; }
        public string ConfirmarClave { get; internal set; }





        //public List<ModelSeguridadConcepto> lstSeguridad = new List<ModelSeguridadConcepto>();
        //public List<ModelDireccion> lstdireccionEmpleado = new List<ModelDireccion>();



        public List<ErrorObj> lstErrores = new List<ErrorObj>();
		public List<ModelParametros> lstparametros = new List<ModelParametros>();
		public List<ModelMiscelaneos> lstmiscelaneos = new List<ModelMiscelaneos>();
        //public List<SY_PreferencesModel> lstPreferencias = new List<SY_PreferencesModel>();

        //public PaginacionGenerico paginacion = new PaginacionGenerico();
    }
}