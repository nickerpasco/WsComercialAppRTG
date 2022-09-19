using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Models
{
    public class ModelTransac_CO_DocumentoDetalle
    {
        public string CompaniaSocio { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int Linea { get; set; }
        public string TipoDetalle { get; set; }
        public string ItemCodigo { get; set; }
        public string Lote { get; set; }
        public string Descripcion { get; set; }
        public string UnidadCodigo { get; set; }
        public string Comentario { get; set; }
        public Nullable<decimal> CantidadPedida { get; set; }
        public Nullable<decimal> CantidadEntregada { get; set; }
        public string UnidadCodigoDoble { get; set; }
        public Nullable<decimal> CantidadPedidaDoble { get; set; }
        public Nullable<decimal> CantidadEntregadaDoble { get; set; }
        public Nullable<decimal> PrecioUnitario { get; set; }
        public Nullable<decimal> PrecioUnitarioFinal { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public Nullable<decimal> MontoFinal { get; set; }
        public string IGVExoneradoFlag { get; set; }
        public string DespachoUnidadEquivalenteFlag { get; set; }
        public string ImprimirPUFlag { get; set; }
        public string AlmacenCodigo { get; set; }
        public string FlujodeCaja { get; set; }
        public string Estado { get; set; }
        public string UltimoUsuario { get; set; }
        public Nullable<System.DateTime> UltimaFechaModif { get; set; }
        public string PrecioModificadoFlag { get; set; }
        public Nullable<int> autorizacionnumero { get; set; }
        public string exportacionsituacion { get; set; }
        public string exportacioncomentarios { get; set; }
        public string reprogramacionpuntollegada { get; set; }
        public Nullable<System.DateTime> ExportacionFechaProgramacion { get; set; }
        public string Condicion { get; set; }
        public Nullable<decimal> PrecioUnitarioDoble { get; set; }
        public Nullable<decimal> ExportacionCantidadEmbarque { get; set; }
        public string ReferenciaFiscal02 { get; set; }
        public string ExportacionMarcaPaquete { get; set; }
        public Nullable<int> Agro_Tienda { get; set; }
        public string RutaDespachoPlacaVehiculo { get; set; }
        public Nullable<System.DateTime> RutaDespachoFechaAsignacion { get; set; }
        public Nullable<decimal> PrecioUnitarioOriginal { get; set; }
        public string NumeroSerie { get; set; }
        public string TransferenciaGratuitaFlag { get; set; }
        public Nullable<decimal> PrecioUnitarioGratuito { get; set; }
        public Nullable<decimal> PorcentajeDescuento01 { get; set; }
        public Nullable<decimal> PorcentajeDescuento02 { get; set; }
        public Nullable<decimal> PorcentajeDescuento03 { get; set; }
        public string DocumentoRelacTipoDocumento { get; set; }
        public string TipoPromocion { get; set; }
        public string DocumentoRelacNumeroDocumento { get; set; }
        public Nullable<int> DocumentoRelacLinea { get; set; }
        public string CentroCosto { get; set; }
        public string Proyecto { get; set; }
        public Nullable<decimal> AnchoNumero { get; set; }
        public Nullable<decimal> LargoNumero { get; set; }
        public Nullable<decimal> AltoNumero { get; set; }
        public Nullable<decimal> Auxiliar01Numero { get; set; }
        public Nullable<decimal> Auxiliar02Numero { get; set; }
        public Nullable<decimal> Auxiliar03Numero { get; set; }
        public string Formula { get; set; }
        public string CadenaMedidas { get; set; }
        public string EquipoVenta { get; set; }
        public string Modelo { get; set; }
        public Nullable<decimal> PesoUnitario { get; set; }
        public string ItemSerie { get; set; }
        public Nullable<decimal> MargenPorcentaje { get; set; }
        public string SpaQuote { get; set; }
        public Nullable<decimal> CantidadAutorizada { get; set; }
        public string ClasificacionRotacion { get; set; }
        public Nullable<decimal> CantidadSOD { get; set; }
        public Nullable<decimal> PrecioUnitarioFlete { get; set; }
        public Nullable<decimal> PrecioUnitarioFleteOriginal { get; set; }
        public Nullable<decimal> MontoFlete { get; set; }
        public string Ordendistribucion { get; set; }
        public string TransferenciaBonificacionFlag { get; set; }
        public string TransferenciaRepresentacionFla { get; set; }
        public string DOT { get; set; }
        public Nullable<decimal> MontoDescuento { get; set; }
        public Nullable<int> ClienteDireccionDespacho { get; set; }
        public string RutaDespacho { get; set; }
        public string AplicacionTipoDocumento { get; set; }
        public string AplicacionNumeroDocumento { get; set; }
        public string IndBonificacion { get; set; }
        public Nullable<int> Promocion { get; set; }
        public Nullable<int> PrecioNumeroRegistro { get; set; }
        public Nullable<int> PromocionNumero { get; set; }
        public Nullable<int> CodigoFlete { get; set; }
        public Nullable<decimal> PrecioUnitarioFlete02 { get; set; }
        public Nullable<decimal> MontoConvenio { get; set; }
        public Nullable<decimal> PrecioUnitarioConvenio { get; set; }
        public Nullable<decimal> cantidadDevolucion { get; set; }
        public Nullable<decimal> PorcentajeDescuento01Original { get; set; }

        public string AlmacenCodigoDetalle { get; set; }
    }
}