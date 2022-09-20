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
    
    public partial class CO_OperacionCanje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CO_OperacionCanje()
        {
            this.CO_OperacionCanjeDetalle = new HashSet<CO_OperacionCanjeDetalle>();
            this.CO_OperacionCanjeDetalle1 = new HashSet<CO_OperacionCanjeDetalle>();
            this.CO_OperacionCanjeDetalle2 = new HashSet<CO_OperacionCanjeDetalle>();
            this.CO_OperacionCanjeDetalle3 = new HashSet<CO_OperacionCanjeDetalle>();
            this.CO_OperacionCanjeDetalle4 = new HashSet<CO_OperacionCanjeDetalle>();
            this.CO_OperacionCanjeDetalle5 = new HashSet<CO_OperacionCanjeDetalle>();
            this.CO_OperacionCanjeDetalle6 = new HashSet<CO_OperacionCanjeDetalle>();
            this.CO_OperacionCanjeDetalle7 = new HashSet<CO_OperacionCanjeDetalle>();
        }
    
        public string CompaniaSocio { get; set; }
        public int OperacionCanjeNumero { get; set; }
        public Nullable<int> Cliente { get; set; }
        public Nullable<int> PreparadoPor { get; set; }
        public Nullable<System.DateTime> FechaPreparacion { get; set; }
        public string VoucherPeriodo { get; set; }
        public string VoucherNo { get; set; }
        public string Estado { get; set; }
        public string UltimoUsuario { get; set; }
        public Nullable<System.DateTime> UltimaFechaModif { get; set; }
        public string Procedencia { get; set; }
        public Nullable<int> DiasAdicionales { get; set; }
        public Nullable<System.DateTime> FechaBase { get; set; }
        public Nullable<int> DiasCanje { get; set; }
        public Nullable<System.DateTime> FechaMaxima { get; set; }
        public string NumeroSolicitud { get; set; }
        public string TipoOperacion { get; set; }
        public Nullable<int> ClienteCobrarA { get; set; }
        public string FinanciamientoNumeroDocumento { get; set; }
        public string FinanciamientoTipoDocumento { get; set; }
        public Nullable<int> ClienteDireccionSecuencia { get; set; }
        public Nullable<int> LetrasCantidad { get; set; }
        public string Comentarios { get; set; }
        public Nullable<int> Vendedor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CO_OperacionCanjeDetalle> CO_OperacionCanjeDetalle7 { get; set; }
    }
}
