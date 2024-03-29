﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsComercialApp.Utils;

namespace WsComercialApp.Models
{
    public class ModelTransacPersona
    {
        public bool FlagNuevaDireccion { get; set; }
        public int? Persona { get; set; }
        public string Origen { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string TipoDocumentoPersona { get; set; }
        public string Nombres { get; set; }
        public string NombreCompleto { get; set; }
        public string FiltroUbigeo { get; set; }
        public string Busqueda { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string CodigoBarras { get; set; }
        public string EsCliente { get; set; }
        public string EsProveedor { get; set; }
        public string EsEmpleado { get; set; }
        public string EsOtro { get; set; }
        public string TipoPersona { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string CiudadNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidad { get; set; }
        public string EstadoCivil { get; set; }
        public string NivelInstruccion { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string DocumentoFiscal { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string CarnetExtranjeria { get; set; }
        public string DocumentoMilitarFA { get; set; }
        public string TipoBrevete { get; set; }
        public string Brevete { get; set; }
        public string Pasaporte { get; set; }
        public string NombreEmergencia { get; set; }
        public string DireccionEmergencia { get; set; }
        public string TelefonoEmergencia { get; set; }
        public string BancoMonedaLocal { get; set; }
        public string TipoCuentaLocal { get; set; }
        public string CuentaMonedaLocal { get; set; }
        public string BancoMonedaExtranjera { get; set; }
        public string TipoCuentaExtranjera { get; set; }
        public string CuentaMonedaExtranjera { get; set; }
        public string PersonaAnt { get; set; }
        public string CorreoElectronico { get; set; }
        public string ClasePersonaCodigo { get; set; }
        public string EnfermedadGraveFlag { get; set; }
        public string Estado { get; set; }
        public string UltimoUsuario { get; set; }
        public Nullable<System.DateTime> UltimaFechaModif { get; set; }
        public string TipoPersonaUsuario { get; set; }
        public string TarjetadeCredito { get; set; }
        public string CuentaMonedaLocal_tmp { get; set; }
        public string CuentaMonedaExtranjera_tmp { get; set; }
        public string Anexo { get; set; }
        public string SUNATNacionalidad { get; set; }
        public string SUNATVia { get; set; }
        public string SUNATZona { get; set; }
        public string SUNATUbigeo { get; set; }
        public string SUNATDomiciliado { get; set; }
        public string GrupoEmpresarial { get; set; }
        public string CuentaInterBancariaLocal { get; set; }
        public string CuentaInterBancariaDolares { get; set; }
        public string PaisEmisor { get; set; }
        public string CodigoLDN { get; set; }
        public string SUNATConvenio { get; set; }
        public string PersonaClasificacion { get; set; }
        public Nullable<System.DateTime> IngresoFechaRegistro { get; set; }
        public string IngresoAplicacionCodigo { get; set; }
        public string IngresoUsuario { get; set; }
        public string PYMEFlag { get; set; }
        public string SUNATNDConvenio { get; set; }
        public string SUNATNDTipoRenta { get; set; }
        public string SUNATNDExoneracion { get; set; }
        public string SUNATNDServicio { get; set; }
        public Nullable<System.DateTime> Brevete_FecVcto { get; set; }
        public Nullable<System.DateTime> CarnetExtranjeria_FecVcto { get; set; }
        public string Celular { get; set; }
        public string CelularEmergencia { get; set; }
        public string CodigoInterbancario { get; set; }
        public string DireccionReferencia { get; set; }
        public string FlagActualizacion { get; set; }
        public string LugarNacimiento { get; set; }
        public string ParentescoEmergencia { get; set; }
        public string SuspensionFonaviFlag { get; set; }
        public string FlagRepetido { get; set; }
        public string CodDiscamec { get; set; }
        public Nullable<System.DateTime> FecIniDiscamec { get; set; }
        public Nullable<System.DateTime> FecFinDiscamec { get; set; }
        public string CodLicArma { get; set; }
        public string MarcaArma { get; set; }
        public string SerieArma { get; set; }
        public Nullable<System.DateTime> InicioArma { get; set; }
        public Nullable<System.DateTime> VencimientoArma { get; set; }
        public string SeguroDiscamec { get; set; }
        public string CorrelativoSCTR { get; set; }
        public string Pais { get; set; }
        public string EsPaciente { get; set; }
        public string EsEmpresa { get; set; }
        public Nullable<int> Persona_Old { get; set; }
        public Nullable<int> personanew { get; set; }
        public string cmp { get; set; }
        public Nullable<int> IndicadorAutogenerado { get; set; }
        public string RutaFirma { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public Nullable<int> IdPersonaUnificado { get; set; }
        public Nullable<int> TipoMedico { get; set; }
        public Nullable<int> IndicadorLiquidacion { get; set; }
        public Nullable<int> IndicadorRetencion { get; set; }
        public string ApellidoPaternoSiteds { get; set; }
        public string ApellidoMaternoSiteds { get; set; }
        public string NombresSiteds { get; set; }
        public string DocumentoSiteds { get; set; }
        public string SexoSiteds { get; set; }
        public Nullable<System.DateTime> FechaNacimientoSiteds { get; set; }
        public Nullable<int> AplicaSiteds { get; set; }
        public Nullable<System.DateTime> FechaActualizacionSiteds { get; set; }
        public Nullable<int> IndicadorSinCorreo { get; set; }
        public string Validado { get; set; }
        public Nullable<int> IndicadorVinculada { get; set; }
        public Nullable<int> IndicadorRegistroManual { get; set; }
        public Nullable<int> IndicadorFallecido { get; set; }
        public string FlagSolicitaUsuario { get; set; }
        public Nullable<int> Situacion { get; set; }
        public Nullable<int> IndRecienNacido { get; set; }
        public Nullable<int> IndicadorExtranjero { get; set; }
        public string CORREOELECTRONICOFE { get; set; }
        public string UnidadNegocio { get; set; }
        public string PersonaAnt2 { get; set; }
        public string personaant3 { get; set; }



        /// <summary>
        /// DATOS DEL CLIENTE
        /// </summary>
        /// 
        public string TipoCliente { get; set; }
        public string CompaniaUsuario { get; set; }
        public int Vendedor { get; set; }
        public string FormadePago { get; set; }

        public string CentroCosto { get; set; }

        public string TipoFacturacion { get; set; }
        public string ConceptoFacturacion { get; set; }

        public string FormaFacturacion { get; set; }

        public string TipoVenta { get; set; }


        public int pkfalso { get; set; }
        public List<ErrorObj> lstErrores = new List<ErrorObj>();
        public List<ModelTranscDireccion> lstDireccion { get; set; }
        public int CountItems;
        public PaginacionGenerico paginacion = new PaginacionGenerico();
    }
}