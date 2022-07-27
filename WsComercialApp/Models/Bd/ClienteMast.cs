//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WsComercialApp.Models.Bd
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClienteMast
    {
        public int Cliente { get; set; }
        public string Clasificacion { get; set; }
        public Nullable<decimal> IngresoMensual { get; set; }
        public string EsGarante { get; set; }
        public string LineaCreditoMoneda { get; set; }
        public Nullable<decimal> LineaCredito { get; set; }
        public string TipoActividad { get; set; }
        public Nullable<int> CantidadDependientes { get; set; }
        public Nullable<decimal> TotalAcumulado { get; set; }
        public string FormadePago { get; set; }
        public string TipoServicio { get; set; }
        public Nullable<int> NumeroDiasCobranza { get; set; }
        public string ResolucionDirectoral { get; set; }
        public Nullable<System.DateTime> LicenciaFechaDesde { get; set; }
        public Nullable<System.DateTime> LicenciaFechaHasta { get; set; }
        public string PracticoTMCFlag { get; set; }
        public string LicenciaNumero { get; set; }
        public string PagoEfectivoFlag { get; set; }
        public string SuspensionFlag { get; set; }
        public string TipoFacturacion { get; set; }
        public string TipoVenta { get; set; }
        public string ConceptoFacturacion { get; set; }
        public Nullable<int> Vendedor { get; set; }
        public string CentroCosto { get; set; }
        public string FormaFacturacion { get; set; }
        public string TipoCliente { get; set; }
        public string RutaDespacho { get; set; }
        public string TipoDocumento { get; set; }
        public string PersonaContacto { get; set; }
        public Nullable<System.DateTime> LineaCreditoFechaVencimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string GaranteNombre { get; set; }
        public string GaranteDocumento { get; set; }
        public string GaranteClasificacion { get; set; }
        public string GaranteDireccion { get; set; }
        public Nullable<decimal> FinanciamientoTasa { get; set; }
        public string PlacaVehiculo { get; set; }
        public Nullable<decimal> LineaCreditoUsado { get; set; }
        public string CreditosComentario { get; set; }
        public Nullable<decimal> UltimaLineaCredito { get; set; }
        public string CorreoFacturacionElectronica { get; set; }
        public string ClaveUsuario { get; set; }
        public string SeguroExcluirFlag { get; set; }
        public string BloquearPedidoFlag { get; set; }
        public string ClienteMarca { get; set; }
        public string Comentario { get; set; }
        public string ComentarioAdicional { get; set; }
        public Nullable<decimal> DeudaTotal { get; set; }
        public Nullable<int> DiaCierre { get; set; }
        public string EstadoCliente { get; set; }
        public Nullable<System.DateTime> FechaModificacionInfocorp { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string FechaVencimientoLogica { get; set; }
        public string FormadePagoLC { get; set; }
        public string InfoCorpFile { get; set; }
        public Nullable<decimal> InfoCorpPorcentaje01 { get; set; }
        public Nullable<decimal> InfoCorpPorcentaje02 { get; set; }
        public Nullable<decimal> InfoCorpPorcentaje03 { get; set; }
        public Nullable<decimal> InfoCorpPorcentaje04 { get; set; }
        public Nullable<decimal> InfoCorpPorcentaje05 { get; set; }
        public Nullable<System.DateTime> LineaCreditoFechaAprobacion { get; set; }
        public Nullable<System.DateTime> LineaCreditoFechaVencHasta { get; set; }
        public Nullable<decimal> LineaCreditoVF { get; set; }
        public string LineaCreditoVFMoneda { get; set; }
        public string Mercado { get; set; }
        public string PersonaContactoAdicional { get; set; }
        public Nullable<int> PersonaGarante1 { get; set; }
        public Nullable<int> PersonaGarante2 { get; set; }
        public string Recepcion02Dia01Flag { get; set; }
        public string Recepcion02Dia02Flag { get; set; }
        public string Recepcion02Dia03Flag { get; set; }
        public string Recepcion02Dia04Flag { get; set; }
        public string Recepcion02Dia05Flag { get; set; }
        public string Recepcion02Dia06Flag { get; set; }
        public string RegistrosPublicosComentario { get; set; }
        public string RegistrosPublicosFile { get; set; }
        public string TipoEmision { get; set; }
        public string TipoNegocio { get; set; }
        public string UnidadNegocio { get; set; }
        public string Accionista1 { get; set; }
        public string Accionista2 { get; set; }
        public string Accionista3 { get; set; }
        public string AccionistaDoc1 { get; set; }
        public string AccionistaDoc2 { get; set; }
        public string AccionistaDoc3 { get; set; }
        public string AutoDetraccionFlag { get; set; }
        public string BloquearVendedor { get; set; }
        public string categoriatempo { get; set; }
        public string CO_TipoCliente { get; set; }
        public string CodigoPostal { get; set; }
        public string CompaniaSocio { get; set; }
        public string conconvenio { get; set; }
        public Nullable<int> Convenio { get; set; }
        public string creditocarga { get; set; }
        public string Departamento { get; set; }
        public string Email { get; set; }
        public string Encargado1 { get; set; }
        public string Encargado2 { get; set; }
        public string Encargado3 { get; set; }
        public string EncargadoCargo1 { get; set; }
        public string EncargadoCargo2 { get; set; }
        public string EncargadoCargo3 { get; set; }
        public Nullable<double> LineaCreditoAcumulado { get; set; }
        public Nullable<double> LineaCreditoPedido { get; set; }
        public Nullable<decimal> LineaCreditoSaldo { get; set; }
        public string Localidad { get; set; }
        public string Ocupacion { get; set; }
        public string PoderFirma1 { get; set; }
        public string PoderFirma2 { get; set; }
        public string PoderFirma3 { get; set; }
        public string PoderFirmaDoc1 { get; set; }
        public string PoderFirmaDoc2 { get; set; }
        public string PoderFirmaDoc3 { get; set; }
        public string PrecioMinimoValidacionFlag { get; set; }
        public string provincia { get; set; }
        public string PwdWeb { get; set; }
        public Nullable<decimal> Sobregiro { get; set; }
        public string TipoDescuentoDefault { get; set; }
        public string TipoLineaCreditoFlag { get; set; }
        public string UsuarioWeb { get; set; }
        public string Zona { get; set; }
        public string BloquearVendedorPedidoFlag { get; set; }
        public string EsAgenteRetenedor { get; set; }
        public string EspecialidadMaestro { get; set; }
        public string CalificacionMaestro { get; set; }
        public string FlagEsMaestro { get; set; }
        public Nullable<System.DateTime> Recepcion02HorarioFin02 { get; set; }
        public Nullable<System.DateTime> Recepcion02HorarioInicio02 { get; set; }
        public Nullable<System.DateTime> Recepcion01HorarioFin02 { get; set; }
        public Nullable<System.DateTime> Recepcion01HorarioInicio02 { get; set; }
        public string IndVisualizarNeto { get; set; }
        public Nullable<int> IdConductor { get; set; }
        public Nullable<int> IdTransportista { get; set; }
        public string AcumularPuntosFlag { get; set; }
        public string InformeDocumentario { get; set; }
        public string Recepcion01Dia06Flag { get; set; }
        public string RequiereGuia { get; set; }
        public Nullable<System.DateTime> Recepcion02HorarioFin { get; set; }
        public Nullable<System.DateTime> Recepcion02HorarioInicio { get; set; }
        public Nullable<System.DateTime> Recepcion01HorarioFin { get; set; }
        public Nullable<System.DateTime> Recepcion01HorarioInicio { get; set; }
        public string Recepcion01Dia05Flag { get; set; }
        public string Recepcion01Dia04Flag { get; set; }
        public string Recepcion01Dia03Flag { get; set; }
        public string Recepcion01Dia02Flag { get; set; }
        public string Recepcion01Dia01Flag { get; set; }
        public byte[] Timestamp { get; set; }
    
        public virtual PersonaMast PersonaMast { get; set; }
    }
}
