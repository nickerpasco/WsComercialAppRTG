﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BdEntityGenerico : DbContext
    {
        public BdEntityGenerico()
            : base("name=BdEntityGenerico")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClienteMast> ClienteMast { get; set; }
        public virtual DbSet<CO_Documento> CO_Documento { get; set; }
        public virtual DbSet<CO_DocumentoDespacho> CO_DocumentoDespacho { get; set; }
        public virtual DbSet<CO_DocumentoDetalle> CO_DocumentoDetalle { get; set; }
        public virtual DbSet<CO_DocumentoDetalleDespacho> CO_DocumentoDetalleDespacho { get; set; }
        public virtual DbSet<CO_DocumentoImpuesto> CO_DocumentoImpuesto { get; set; }
        public virtual DbSet<CorrelativosMast> CorrelativosMast { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<MA_FormadePago> MA_FormadePago { get; set; }
        public virtual DbSet<MA_FormadePagoDetalle> MA_FormadePagoDetalle { get; set; }
        public virtual DbSet<PersonaMast> PersonaMast { get; set; }
        public virtual DbSet<SY_Preferences> SY_Preferences { get; set; }
        public virtual DbSet<MA_ClienteVendedor> MA_ClienteVendedor { get; set; }
        public virtual DbSet<CO_LetraCompromiso> CO_LetraCompromiso { get; set; }
        public virtual DbSet<CO_LetraCompromisoBlog> CO_LetraCompromisoBlog { get; set; }
        public virtual DbSet<CO_LetraCompromisoDocumento> CO_LetraCompromisoDocumento { get; set; }
        public virtual DbSet<CO_LetraCompromisoLetra> CO_LetraCompromisoLetra { get; set; }
    }
}
