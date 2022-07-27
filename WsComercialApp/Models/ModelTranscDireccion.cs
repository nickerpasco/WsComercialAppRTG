using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class ModelTranscDireccion
    {
        public int Persona { get; set; }
        public int Secuencia { get; set; }
        public string Direccion { get; set; }
        public string ReferenciasDireccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string AREA { get; set; }
        public string ZONA { get; set; }
        public string LOCALIDAD { get; set; }
        public string EsRemision { get; set; }
        public string EsCobranza { get; set; }
        public string EsDomicilio { get; set; }
        public string EsTrabajo { get; set; }
        public string EsOtros { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Zonadespacho { get; set; }
        public Nullable<int> Ordendespacho { get; set; }
        public string AlmacenCodigo { get; set; }
        public string Empresa { get; set; }
        public string CondicionDireccion { get; set; }
        public string UnidadTiempo { get; set; }
        public Nullable<float> TiempoResidencia { get; set; }
        public Nullable<decimal> MontoAvaluo { get; set; }
        public string TipoCalle { get; set; }
        public string NombreCalle { get; set; }
        public string Numero { get; set; }
        public string NumDep { get; set; }
        public string Manzana { get; set; }
        public string Lote { get; set; }
        public string Interior { get; set; }
        public string Sector { get; set; }
        public string Urbanizacion { get; set; }
        public string ProvinciaDomicilio { get; set; }
        public string Pais { get; set; }
        public string Distrito { get; set; }
        public string UBIGEO { get; set; }
        public string PROVINCIAFLAG { get; set; }
        public Nullable<int> PRIORIDAD { get; set; }
        public string PDTTipoZona { get; set; }
        public string PDTZona { get; set; }
        public string PDTTipoCalle { get; set; }
        public string Estado { get; set; }
        public string UltimoUsuario { get; set; }
        public Nullable<System.DateTime> UltimaFechaModif { get; set; }
        public byte[] Timestamp { get; set; }
        public string TipoDireccion { get; set; }
        public string Kilometro { get; set; }
        public string Etapa { get; set; }
        public string FlagCentroEssalud { get; set; }
        public Nullable<int> Vendedor { get; set; }
        public string RutaDespacho { get; set; }
    }
}