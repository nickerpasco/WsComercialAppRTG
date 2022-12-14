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
    
    public partial class CO_DocumentoDetalleDespacho
    {
        public string CompaniaSocio { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int Linea { get; set; }
        public int Secuencia { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public string AlmacenCodigo { get; set; }
        public Nullable<System.DateTime> FechaEntrega { get; set; }
        public Nullable<int> Turno { get; set; }
        public string Estado { get; set; }
        public string UltimoUsuario { get; set; }
        public Nullable<System.DateTime> UltimaFechaModif { get; set; }
        public Nullable<decimal> CantidadSOD { get; set; }
        public string ComprometeFlag { get; set; }
        public Nullable<decimal> CantidadMerma { get; set; }
        public Nullable<decimal> CantidadTotal { get; set; }
        public string Item { get; set; }
        public string TipoHoraEntrega { get; set; }
        public string AlmacenPrincipal { get; set; }
        public string Ordendistribucion { get; set; }
        public Nullable<decimal> CantidadRecibida { get; set; }
        public Nullable<int> ClienteDireccionDespacho { get; set; }
        public string DOT { get; set; }
        public string TipoRegistro { get; set; }
    
        public virtual CO_Documento CO_Documento { get; set; }
    }
}
