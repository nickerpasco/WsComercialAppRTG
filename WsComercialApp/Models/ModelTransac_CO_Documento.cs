﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsComercialApp.Utils;

namespace WsComercialApp.Models
{
    public class ModelTransac_CO_Documento
    {

        public string CompaniaSocio { get; set; }
        public string NotaCredito { get; set; }
        public string ComentarioCambio { get; set; } 
        public string LetraNumeroUnicoString { get; set; }
        public string MostrarDatosRuta { get; set; }

        //public DateTime? FechaVencimientoDate { get; set; }
        public Nullable<int> DiasAdicionales { get; set; }

        public string MostrarComentarios { get; set; }
        public string AccionLetras { get; set; }
        public string TipoDocumento { get; set; }
        public string FlagEnEspera { get; set; }
        public string ItemDescuento { get; set; }
        public string AlmacenItemDescuento { get; set; }
        public string TipoDocumentoPedidoCLiente { get; set; }
        public string NumeroDocumento { get; set; }
        public string Comentario { get; set; }
        public string Accion { get; set; }
        public Nullable<int> TransportistaProvincia { get; set; }
        public int?  OperacionCanjeNumero { get; set; }
      
        public string EstablecimientoCodigo { get; set; }
        public string FiltroUbigeo { get; set; }
        public string FormaFacturacion { get; set; }
        public string CodigoQR { get; set; }
        public Nullable<int> ClienteNumero { get; set; }
        public Nullable<int> IdPersonaUsuario { get; set; }
        public Nullable<decimal> CantidadItemDescuento { get; set; }
        public string ClienteRUC { get; set; }
        public string Plataforma { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteDireccion { get; set; }
        public string ClienteReferencia { get; set; }
        public Nullable<int> ClienteCobrarA { get; set; }
        public Nullable<int> Dias { get; set; }
        public Nullable<int> Index { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<System.DateTime> FechaDocumento { get; set; }
        public Nullable<System.DateTime> FechaDespacho { get; set; }
        public String Telefono { get; set; }
        public String Conductor { get; set; }
        public String EstadoDespacho { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public String FechaVencimientoString { get; set; }
        public Nullable<System.DateTime> FechaVencimientoDate { get; set; }
        public string TipoFacturacion { get; set; }
        public string TipoVenta { get; set; }
        public string ConceptoFacturacion { get; set; }
        public string FormadePago { get; set; }
        public string Criteria { get; set; }
        public Nullable<int> Vendedor { get; set; }
        public Nullable<decimal> TipodeCambio { get; set; }
        public Nullable<decimal> Pendiente { get; set; }
        public Nullable<decimal> MontoTotalLetras { get; set; }
        public string MonedaDocumento { get; set; }
        public bool ResponseBoolean { get; set; }
        public Nullable<decimal> MontoAfecto { get; set; }
        public Nullable<decimal> MontoNoAfecto { get; set; }
        public Nullable<decimal> MontoImpuestoVentas { get; set; }
        public Nullable<decimal> MontoImpuestos { get; set; }
        public Nullable<decimal> MontoDescuentos { get; set; }
        public Nullable<decimal> MontoTotal { get; set; }
        public Nullable<decimal> MontoPagado { get; set; }
        public Nullable<decimal> MontoAdelantoSaldo { get; set; }
        public Nullable<int> PreparadoPor { get; set; }
        public Nullable<int> AprobadoPor { get; set; }
        public Nullable<System.DateTime> FechaPreparacion { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
        public Nullable<System.DateTime> FechaCobranza { get; set; }
        public string NotaCreditoDocumento { get; set; }
        public string TipoMotivo { get; set; }
        public string ComercialPedidoNumero { get; set; }
        public Nullable<System.DateTime> ComercialPedidoFechaRequerida { get; set; }
        public string AlmacenCodigo { get; set; }
        public string ImpresionPendienteFlag { get; set; }
        public string DocumentoMoraFlag { get; set; }
        public string ContabilizacionPendienteFlag { get; set; }
        public string VoucherPeriodo { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherAnulacion { get; set; }
        public string CentroCosto { get; set; }
        public bool ValidacionLineaCredito { get; set; }
        public bool ValidacionFacturasVencidas { get; set; }
        public string Proyecto { get; set; }
        public string CampoReferencia { get; set; }
        public string Sucursal { get; set; }
        public string TipoCanjeFactura { get; set; }
        public string LetraCarteraFlag { get; set; }
        public string LetraBanco { get; set; }
        public string LetraCobranzaNumero { get; set; }
        public string LetraAvalNombre { get; set; }
        public string LetraAvalRUC { get; set; }
        public string LetraAvalDireccion { get; set; }
        public string LetraAvalTelefono { get; set; }
        public string LetraAceptadoPor { get; set; }
        public string LetraUbicacion { get; set; }
        public string EquipoVenta { get; set; }
        public Nullable<System.DateTime> LetraFechaRecepcion { get; set; }
        public string LetraDescuentoCuentaBancaria { get; set; }
        public Nullable<decimal> LetraDescuentoIntereses { get; set; }
        public string LetraDescuentoVoucherFlag { get; set; }
        public string LetraDescuentoVoucher { get; set; }
        public string LetraDescuentoCanjeFlag { get; set; }
        public string LetraDescuentoCanjeVoucher { get; set; }
        public string DescuentoFlag { get; set; }
        public string APTransferidoFlag { get; set; }
        public Nullable<int> APProcesoNumero { get; set; }
        public Nullable<int> APProcesoSecuencia { get; set; }
        public string CobranzaDudosaEstado { get; set; }
        public string CobranzaDudosaVoucher { get; set; }
        public string CobranzaDudosaVoucherClearing { get; set; }
        public Nullable<System.DateTime> CobranzaDudosaFecha { get; set; }
        public Nullable<System.DateTime> CobranzaDudosaFechaClearing { get; set; }
        public string UnidadNegocio { get; set; }
        public string UnidadReplicacion { get; set; }
        public string ProcesoImportacion { get; set; }
        public string ProcesoImportacionNumero { get; set; }
        public Nullable<System.DateTime> ProcesoImportacionFecha { get; set; }
        public string Comentarios { get; set; }
        public string ComentariosImprimirFlag { get; set; }
        public Nullable<decimal> ComentariosMonto { get; set; }
        public string NumeroInterno { get; set; }
        public string RutaDespacho { get; set; }
        public string Estado { get; set; }
        public string UltimoUsuario { get; set; }
      
        public Nullable<System.DateTime> UltimaFechaModif { get; set; }
        public Nullable<decimal> MontoRedondeo { get; set; }
        public string LetraAceptadoFlag { get; set; }
        public string PrecioModificadoFlag { get; set; }
        public string clientecontacto { get; set; }
        public Nullable<int> clientedireccionsecuencia { get; set; }
        public string transportistavehiculo { get; set; }
        public string transportistachofer { get; set; }
        public string exportacionsituacion { get; set; }
        public string reprogramacionpuntollegada { get; set; }
        public string ReprogramacionPuntoPartida { get; set; }
        public string LicitacionNumeroProceso { get; set; }
        public string MEF_Estado { get; set; }
        public Nullable<System.DateTime> SIAF_FechaPago { get; set; }
        public string SIAF_Correlativo { get; set; }
        public string SIAF_Secuencia { get; set; }
        public string SIAF_Expediente { get; set; }
        public string INT_EstadoEnvio { get; set; }
        public string INT_EstadoProceso { get; set; }
        public string INT_Secuencial { get; set; }
        public string LetraProtestoFlag { get; set; }
        public string RutaDespachoPlacaVehiculo { get; set; }
        public Nullable<System.DateTime> RutaDespachoFechaAsignacion { get; set; }
        public string DocumentosinCantidadFlag { get; set; }
        public string DocumentosinDespachoFlag { get; set; }
        public Nullable<System.DateTime> APFechaTransferencia { get; set; }
        public string ReprogramacionMotivo { get; set; }
        public string DiferidoFlag { get; set; }
        public Nullable<System.DateTime> FechaVencimientoOriginal { get; set; }
        public string NumeroContrato { get; set; }
        public string CentroCostoDestino { get; set; }
        public string CobranzaDudosaMotivo { get; set; }
        public Nullable<decimal> MontoImpuestoRetenido { get; set; }
        public Nullable<decimal> TransferenciaGratuitaIGVFactor { get; set; }
        public string DiferidoDocumento { get; set; }
        public Nullable<System.DateTime> FechaImpresion { get; set; }
        public Nullable<decimal> LetraDescuentoPortes { get; set; }
        public string LetraEstado { get; set; }
        public Nullable<System.DateTime> LetraProtestoFecha { get; set; }
        public string LetraProtestoNDFlag { get; set; }
        public string AlmacenConsignacion { get; set; }
        public Nullable<int> AprobadoPorDescuento { get; set; }
        public string DespachoFlag { get; set; }
        public string TransferProduccionFlag { get; set; }
        public Nullable<int> ImpresionNumero { get; set; }
        public string Cadena { get; set; }
        public string ComentarioAprobacion { get; set; }
        public Nullable<decimal> LetraIntereses { get; set; }
        public Nullable<decimal> LetraPortes { get; set; }
        public Nullable<decimal> LetraProtesto { get; set; }
        public Nullable<int> LetraPersona { get; set; }
        public string LetraTipoNegociacion { get; set; }
        public string LetraNumeroUnico { get; set; }
        public string LetraPlanillaBanco { get; set; }
        public string LetraPresentadaVoucher { get; set; }
        public Nullable<decimal> LetraComisiones { get; set; }
        public Nullable<int> Cobrador { get; set; }
        public Nullable<decimal> MontoRetenido { get; set; }
        public string MontoRetenidoFlag { get; set; }
        public Nullable<decimal> MontoPercepcion { get; set; }
        public string PercepcionDocumento { get; set; }
        public Nullable<decimal> PercepcionPorcentaje { get; set; }
        public Nullable<System.DateTime> FechaOrdenCompra { get; set; }
        public Nullable<System.DateTime> FechaRecepcion { get; set; }
        public Nullable<System.DateTime> FechaRecepcionADV { get; set; }
        public string CotizacionNumero { get; set; }
        public string RequisicionNumero { get; set; }
        public string ComentarioOrdenCompra { get; set; }
        public Nullable<int> ClienteDireccionDespacho { get; set; }
        public string MotivoAnulacion { get; set; }
        public string MotivoNotaCredito { get; set; }
        public string Vendedor_all { get; set; }
        public string FENotaCreditoMotivo { get; set; }
        public string FENotaCreditoSustento { get; set; }
        public string ObservacionFE { get; set; }
        public string FEEstado { get; set; }
        public string FEHashCode { get; set; }
        public Nullable<int> FEInternalNumber { get; set; }
        public Nullable<int> FEResumenNumero { get; set; }
        public string EstadoSunat { get; set; }
        public Nullable<decimal> MontoBruto { get; set; }
        public Nullable<decimal> MontoFlete { get; set; }
        public string RecojoFlag { get; set; }
        public Nullable<int> PedidoNumerado { get; set; }
        public Nullable<System.DateTime> FechaInicioTraslado { get; set; }
        public Nullable<System.DateTime> FechaIngreso { get; set; }
        public Nullable<System.DateTime> FechaVencimientoDOT { get; set; }
        public string CanalVenta { get; set; }
        public string LetraVoucherDiferido { get; set; }
        public Nullable<int> ContratoSecuencia { get; set; }
        public Nullable<int> FECOMUNICADONUMERO { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string CobranzaLegalVoucher { get; set; }
        public Nullable<System.DateTime> CobranzaLegalFecha { get; set; }
        public Nullable<System.DateTime> LetrasFechaBase { get; set; }

        public Nullable<int> FEENVIONUMERO { get; set; }
        public Nullable<int> flag_resumendiario { get; set; }
        public Nullable<System.DateTime> FechaBaseLetras { get; set; }
        public Nullable<int> flg_resumendiario { get; set; }
        public string flag_EstadoTransmision { get; set; }
        public Nullable<int> PromotorVenta { get; set; }
        public Nullable<decimal> MontoConvenio { get; set; }
        public string Convenio { get; set; }
        public Nullable<int> FEEnvioBaja { get; set; }
        public Nullable<int> CantidadLetras { get; set; }
        public Nullable<int> DiasCredito { get; set; }
        public string TipoCliente { get; set; }
        public string CreditoFlag { get; set; }
        public string MonedaDocumentoLetra { get; set; }


        public string ComprobanteBase64 { get; set; }

        public List<ModelTransac_CO_Documento> LstLetras { get; set; }
        public List<ModelTransac_CO_DocumentoDetalle> Detalle { get; set; }
        public List<ModelTransac_CO_DocumentoDetalle> LstBLogs { get; set; }

        public List<ErrorObj> lstErrores = new List<ErrorObj>();
        public List<Model_Motivos> lstMotivos = new List<Model_Motivos>();
      
        public List<Model_CoDocumentoImpuesto> DetalleImpuestos { get; set; }



        public string FormadePagoAntigua { get; set; }
        public string FormadePagoAntiguaFlagCedito { get; set; }
        public string FormadePagoNuevaFlagCedito { get; set; }
        public string FormadePagoNueva { get; set; }
        public string Procedencia { get; set; }
        public DateTime? FechaMaxima { get; set; }
        public DateTime? FechaBloj { get; set; }
        public int? DiasDiferencias { get;   set; }
        public bool ValidacionDiasVencidoCanjeLetras { get;   set; }


        public string Agencia { get; set; } 
        public DateTime? FechaSalida { get; set; }


        //public int Pendiente { get; set; }
        //public string MostrarComentarios { get; set; }
        //public string NotaCredito { get; set; }
        //public string ComentarioCambio { get; set; }
        //public string NumeroDocumento { get; set; }
        //public DateTime? FechaDocumento { get; set; }
        //public decimal? MontoTotal { get; set; }
        //public string ComercialPedidoNumero { get; set; }
        //public DateTime? FechaCobranza { get; set; }
        //public decimal? MontoPagado { get; set; }



    }
}