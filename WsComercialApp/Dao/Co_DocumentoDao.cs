using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web; 
using WsComercialApp.Models;
using WsComercialApp.Models.Bd;
using WsComercialApp.Repositorio;
using WsComercialApp.Utils;

namespace WsComercialApp.Dao
{
    public class Co_DocumentoDao
    {

        public ModelTransac_CO_Documento InsertCo_Documento(ModelTransac_CO_Documento c)
        {
            ErrorObj error = new ErrorObj();
            String TipoMotivo = "";

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            using (var context = new BdEntityGenerico())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        if (c.Accion == "Editar")
                        {
                            response = ActualizarPedido(c, context);
                        }
                        else
                        {
                            response = InsertarPedido(c, context);
                        }


                        dbContextTransaction.Commit();


                        ///INICIAR INVOCACION STORE PROCEDURE
                        ///

                        var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.StoreActualiza_Costos_Margenes");
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@CompaniaSocio", response.CompaniaSocio));
                        parametros.Add(new SqlParameter("@TipoDocumento", response.TipoDocumento));
                        parametros.Add(new SqlParameter("@NumeroDocumento", response.NumeroDocumento));


                        var retorno = UtilsDAO.ExecuteQueryResponse(sqlString, parametros);

                        StokCompromentidoStart(c.Detalle, c.AlmacenCodigo);


                        //Co_Documento.StoreActualiza_Costos_Margenes
                        //SP_CO_Actualiza_Costos_Margenes @CompaniaSocio, @TipoDocumento, @NumeroDocumento



                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.InnerException.InnerException.Message;

                        }
                        else
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.Message;
                        }


                        //var validaciones = ex.EntityV

                        dbContextTransaction.Rollback();

                        response.IdPersonaUsuario = 0;
                        response.CantidadLetras = 0;
                        response.DiasCredito = 0;
                        response.FechaBaseLetras = DateTime.Now;
                        response.FechaMaxima = DateTime.Now;
                        response.FechaBloj = DateTime.Now;

                        response.lstErrores.Add(error);


                        //throw ex;
                    }
                }
            }
            return response;
        }


        public ModelTransac_CO_Documento SaveLetras(ModelTransac_CO_Documento c)
        {
            ErrorObj error = new ErrorObj();
            String TipoMotivo = "";

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            using (var context = new BdEntityGenerico())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (c.AccionLetras == "Nuevo")
                        {
                            response = InsertarLetras(c, context);
                        }
                        else if (c.AccionLetras == "Editar")
                        {
                            response = ActualizarLetras(c, context);
                        }

                           
                        if (response.lstErrores.Count > 0)
                        {
                            dbContextTransaction.Rollback();
                            return response;
                        }

                        dbContextTransaction.Commit();



                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.InnerException.InnerException.Message;

                        }
                        else
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.Message;
                        }
                        dbContextTransaction.Rollback();

                        response.lstErrores.Add(error);
                    }
                }
            }
            return response;
        }

        private void StokCompromentidoStart(List<ModelTransac_CO_DocumentoDetalle> detalle, String Almacen)
        {

            String ParametroValida = UtilsDAO.getParametroString("999999", "ITEMCOMMIT");

            if (FuncPrinc.trimValor(ParametroValida) == "S")
            {

                foreach (var item in detalle)
                {
                    var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.MotoActualizaComprometido");
                    List<SqlParameter> parametros = new List<SqlParameter>();
                    parametros.Add(new SqlParameter("@Item", item.ItemCodigo.Trim()));
                    parametros.Add(new SqlParameter("@Cantidad", item.CantidadPedida));
                    parametros.Add(new SqlParameter("@Almacen", Almacen));
                    parametros.Add(new SqlParameter("@Lote", getLote(item.ItemCodigo, Almacen, item.CantidadPedida)));

                    var retorno = UtilsDAO.ExecuteQueryResponse(sqlString, parametros);
                }


            }


        }

        private String getLote(string itemCodigo, string almacen, decimal? cantidad)
        {
            String validatem = UtilsDAO.getValuString("select isnull(ManejoxLoteFlag,'N') as ManejoxLoteFlag from WH_ItemMast where Item = '" + itemCodigo + "'", null);

            if (validatem == "N")
            {
                return "00";
            }

            String realQuery = "select Lote,StockActual from WH_ItemAlmacenLote  where  AlmacenCodigo = '" + almacen + "' AND Item = '" + itemCodigo + "' order by FechaIngreso asc";
            var resultado = UtilsDAO.getDataByQuery<Model_Stock_Query>(realQuery);

            foreach (var item in resultado)
            {
                if (item.StockActual > cantidad)
                {
                    return item.Lote;
                }
            }

            return "00";

        }

        public ModelTransac_CO_Documento SaveFacturacion(ModelTransac_CO_Documento c)
        {
            ErrorObj error = new ErrorObj();
            String TipoMotivo = "";

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            using (var context = new BdEntityGenerico())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        response = InsertarFacturacion(c, context);

                        if (response.lstErrores.Count == 0)
                        {
                            dbContextTransaction.Commit();
                        }
                        else
                        {
                            dbContextTransaction.Rollback();
                            return response;
                        }


                        RepositorioCO_Pedido obj = new RepositorioCO_Pedido();
                        //response.CodigoQR = c.CodigoQR;
                        var responseRerporte = obj.ExportarComprobantePdf(response);

                        response.ComprobanteBase64 = responseRerporte.BASE64Certificado;


                        /////INICIAR INVOCACION STORE PROCEDURE
                        /////

                        //var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.StoreActualiza_Costos_Margenes");
                        //List<SqlParameter> parametros = new List<SqlParameter>();
                        //parametros.Add(new SqlParameter("@CompaniaSocio", response.CompaniaSocio));
                        //parametros.Add(new SqlParameter("@TipoDocumento", response.TipoDocumento));
                        //parametros.Add(new SqlParameter("@NumeroDocumento", response.NumeroDocumento));


                        //var retorno = UtilsDAO.ExecuteQueryResponse(sqlString, parametros);

                        //Co_Documento.StoreActualiza_Costos_Margenes
                        //SP_CO_Actualiza_Costos_Margenes @CompaniaSocio, @TipoDocumento, @NumeroDocumento



                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.InnerException.InnerException.Message;

                        }
                        else
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.Message;
                        }


                        //var validaciones = ex.EntityV

                        dbContextTransaction.Rollback();

                        response.lstErrores.Add(error);


                        //throw ex;
                    }
                }
            }
            return response;
        }

        public ModelTransac_CO_Documento AnularPedido(ModelTransac_CO_Documento c)
        {
            ErrorObj error = new ErrorObj();
            String TipoMotivo = "";

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            using (var context = new BdEntityGenerico())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        response = AnularPedido(c, context);

                        dbContextTransaction.Commit();



                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.InnerException.InnerException.Message;

                        }
                        else
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.Message;
                        }


                        //var validaciones = ex.EntityV

                        dbContextTransaction.Rollback();

                        response.lstErrores.Add(error);


                        //throw ex;
                    }
                }
            }
            return response;
        }

        private ModelTransac_CO_Documento InsertarPedido(ModelTransac_CO_Documento c, BdEntityGenerico context)
        {

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            ErrorObj error = new ErrorObj();

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", " PersonaMast.GetIdByUser");
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Usuario", c.UltimoUsuario.Trim()));
            var IdUsuarioPersona = UtilsDAO.getValueInt(sqlString, parametros);


            response.IdPersonaUsuario = IdUsuarioPersona;
            c.IdPersonaUsuario = IdUsuarioPersona;
            response.FechaBaseLetras = DateTime.Now;
            c.FechaBaseLetras = DateTime.Now;

            var sqlString2 = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", " PersonaMast.getCentroCostosUser");
            List<SqlParameter> parametros2 = new List<SqlParameter>();
            parametros2.Add(new SqlParameter("@Persona", c.ClienteNumero));
            var CentroCostosUsuario = UtilsDAO.getValuString(sqlString2, parametros2);



            var sqlString3 = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", " PersonaMast.getDataEmpleado");
            List<SqlParameter> parametros3 = new List<SqlParameter>();
            parametros3.Add(new SqlParameter("@Empleado", IdUsuarioPersona));
            ModelTransac_CO_Documento obj = (ModelTransac_CO_Documento)UtilsDAO.getDataObjectByQueryWithParameters<ModelTransac_CO_Documento>(sqlString3, parametros3);





            c.Sucursal = FuncPrinc.trimValor(obj.Sucursal);
            c.UnidadNegocio = FuncPrinc.trimValor(obj.UnidadNegocio);


            var companyAll = c.CompaniaSocio;
            var company = c.CompaniaSocio.Substring(0, 6);



            var CorrelativosMast = context.CorrelativosMast.DefaultIfEmpty().Where(t => t.CompaniaCodigo == company
            && t.TipoComprobante == c.TipoDocumento && t.Serie == c.Sucursal).ToList();
            int correlativo = 0;
            int? NumeroInterno = 0;
            if (CorrelativosMast.Count > 0)
            {
                correlativo = (int)((CorrelativosMast[0].CorrelativoNumero) + 1);
                //var serie = (CorrelativosMast[0].Serie); 
                //serie = serie.Substring(2, 2);
                //c.NumeroDocumento = serie + correlativo.ToString("D8");
                c.NumeroDocumento = correlativo.ToString("D10");
            }
            else
            {
                error.CodigoError = 500;
                error.MensajeError = "Error al crear el correlativo ";
                response.lstErrores.Add(error);
                return response;
            }

            try
            {

                var OtablaCorreclativo = context.CorrelativosMast.SingleOrDefault(t => t.CompaniaCodigo == company && t.TipoComprobante == c.TipoDocumento && t.Serie == c.Sucursal);
                if (OtablaCorreclativo != null)
                {

                    OtablaCorreclativo.CorrelativoNumero = correlativo;

                    context.Entry(OtablaCorreclativo).State = System.Data.Entity.EntityState.Modified;
                }


                var OtablaNumeroInterno = context.CorrelativosMast.SingleOrDefault(t => t.TipoComprobante == "TK");
                if (OtablaNumeroInterno != null)
                {
                    NumeroInterno = (OtablaNumeroInterno.CorrelativoNumero) + 1;
                    OtablaNumeroInterno.CorrelativoNumero = NumeroInterno;

                    context.Entry(OtablaNumeroInterno).State = System.Data.Entity.EntityState.Modified;
                }


                var getEstablecimiento = context.SY_Preferences.SingleOrDefault(t => t.Preference == "UNIDADREP" && t.AplicacionCodigo == "CO" && t.Usuario == c.UltimoUsuario);

                if (getEstablecimiento != null)
                {
                    c.EstablecimientoCodigo = FuncPrinc.trimValor(getEstablecimiento.ValorString);
                    c.UnidadReplicacion = FuncPrinc.trimValor(getEstablecimiento.ValorString);
                }
                else
                {
                    c.EstablecimientoCodigo = null;
                    c.UnidadReplicacion = null;

                    error.CodigoError = 500;
                    error.MensajeError = "El Usuario " + c.UltimoUsuario + " no cuenta con la preferencia UNIDADREP";
                    response.lstErrores.Add(error);
                    return response;
                }

                if (c.CreditoFlag == "S")
                {
                    var getNumerosFechaVencimiento = context.MA_FormadePagoDetalle.SingleOrDefault(t => t.FormadePago == c.FormadePago);

                    if (getNumerosFechaVencimiento != null)
                    {
                        int value = 0;
                        int? numeroDias = getNumerosFechaVencimiento.NumeroDias;
                        if (numeroDias != 0)
                        {
                            value = Convert.ToInt32(numeroDias);
                        }

                        c.FechaVencimiento = DateTime.Now.AddDays(value);
                    }
                    else
                    {
                        c.FechaVencimiento = null;
                    }
                }
                else
                {
                    c.FechaVencimiento = null;
                }


                var Otabla = new CO_Documento();


                c.CentroCosto = FuncPrinc.trimValor(CentroCostosUsuario);
                Otabla.CompaniaSocio = c.CompaniaSocio;
                Otabla.TipoDocumento = c.TipoDocumento;
                Otabla.NumeroDocumento = c.NumeroDocumento;
                Otabla.EstablecimientoCodigo = c.EstablecimientoCodigo;
                Otabla.FechaDocumento = DateTime.Now;//c.FechaDocumento;
                Otabla.FechaVencimiento = c.FechaVencimiento;
                Otabla.ClienteReferencia = c.ClienteReferencia;
                Otabla.NumeroInterno = NumeroInterno.ToString();

                Otabla.TipoFacturacion = c.TipoFacturacion; // UtilsDAO.getParametroString("999999", "DOCTIPOFAC"); 
                Otabla.NumeroDocumento = c.NumeroDocumento;
                Otabla.TipoVenta = UtilsDAO.getValuString("select tipoventa from CO_Vendedor where Vendedor=" + IdUsuarioPersona + "", null);
                //Otabla.ConceptoFacturacion = UtilsDAO.getParametroString("999999", "DOCCONCEPT");
                Otabla.ConceptoFacturacion = c.ConceptoFacturacion;
                Otabla.REALIZADOPORWEB = "S";
                Otabla.ClienteNumero = c.ClienteNumero;
                Otabla.ClienteRUC = c.ClienteRUC;
                Otabla.ClienteNombre = c.ClienteNombre;
                Otabla.ClienteDireccion = c.ClienteDireccion;
                Otabla.ClienteCobrarA = c.ClienteNumero;

                Otabla.FormadePago = c.FormadePago;
                Otabla.Criteria = "999999";
                Otabla.Vendedor = IdUsuarioPersona;
                Otabla.TipodeCambio = c.TipodeCambio;
                Otabla.MonedaDocumento = c.MonedaDocumento;
                Otabla.EquipoVenta = UtilsDAO.getValuString("select VentaEquipo from CO_Vendedor where Vendedor=" + IdUsuarioPersona + "", null); ;
                Otabla.VentaEquipo = Otabla.EquipoVenta;
                Otabla.ValidezOferta = UtilsDAO.getValueDecimal("select numero from ParametrosMast where ParametroClave='DIASVALPED' and CompaniaCodigo='999999' and AplicacionCodigo='CO'", null); ;
                Otabla.MontoAfecto = c.MontoAfecto;
                Otabla.MontoNoAfecto = c.MontoNoAfecto;
                Otabla.MontoImpuestoVentas = c.MontoImpuestoVentas;
                Otabla.MontoImpuestos = c.MontoImpuestos;
                Otabla.MontoDescuentos = c.MontoDescuentos;

                Otabla.MontoTotal = c.MontoTotal;
                Otabla.MontoPagado = 0;
                Otabla.PreparadoPor = IdUsuarioPersona;
                Otabla.AprobadoPor = IdUsuarioPersona;
                Otabla.FechaAprobacion = Otabla.FechaDocumento;

                Otabla.ClienteDireccionDespacho = c.ClienteDireccionDespacho;
                Otabla.FechaPreparacion = Otabla.FechaDocumento;
                Otabla.ComercialPedidoFechaRequerida = Otabla.FechaPreparacion;

                //Otabla.ComercialPedidoFechaRequerida = c.FechaDocumento;
                Otabla.TipoMotivo = c.TipoMotivo;
                Otabla.AlmacenCodigo = UtilsDAO.getValuString("select ValorString from SY_Preferences where usuario='" + c.UltimoUsuario + "' and Preference='ALMACEN'", null); ;
                c.AlmacenCodigo = Otabla.AlmacenCodigo;
                Otabla.ReferenciaTipoPago = UtilsDAO.getValuString("select texto from ParametrosMast where CompaniaCodigo='999999' and ParametroClave='TIPAGAPP' and AplicacionCodigo='CO'", null); ;
                Otabla.ImpresionPendienteFlag = "S";
                Otabla.DocumentoMoraFlag = "N";
                Otabla.ContabilizacionPendienteFlag = "S";




                string dateString = DateTime.Now.ToString("yyyyMM", CultureInfo.InvariantCulture);

                Otabla.VoucherPeriodo = dateString;

                Otabla.Sucursal = c.Sucursal;
                Otabla.LetraCarteraFlag = "S";
                Otabla.UnidadReplicacion = c.UnidadReplicacion;
                Otabla.UnidadNegocio = c.UnidadNegocio;

                Otabla.Comentarios = c.Comentarios;
                Otabla.ComentariosImprimirFlag = "N";
                Otabla.DocumentosinCantidadFlag = "N";
                //Otabla.TipoFormaPagoSunat = "0";
                Otabla.MontoPendientePago = 0;
                Otabla.ComentariosMonto = 0;
                //Otabla.Estado = c.Estado;
                Otabla.Estado = "AP";

                Otabla.UltimoUsuario = c.UltimoUsuario;

                Otabla.UltimaFechaModif = DateTime.Now;
                Otabla.TipoCanjeFactura = "NO";
                Otabla.CobranzaDudosaEstado = "NO";
                Otabla.LetraDescuentoVoucherFlag = "N";
                Otabla.LetraDescuentoIntereses = 0;

                Otabla.APTransferidoFlag = "N";
                Otabla.MontoAdelantoSaldo = 0;
                Otabla.FormaFacturacion = UtilsDAO.getParametroString(company, "DOCFORFACT");
                Otabla.RutaDespacho = UtilsDAO.getValuString("select RutaDespacho from Direccion  with(nolock) where persona=" + Otabla.ClienteNumero + " and secuencia =" + Otabla.ClienteDireccionDespacho + "", null); ;
                Otabla.MontoRedondeo = 0;
                //Otabla.clientedireccionsecuencia = c.clientedireccionsecuencia;
                Otabla.SIAF_Correlativo = "01";

                Otabla.TipoCliente = c.TipoCliente;
                Otabla.DocumentosinDespachoFlag = "N";

                if (c.CreditoFlag == "S")
                {
                    Otabla.FechaVencimientoOriginal = UtilsDAO.getValueDatetime("select GETDATE()+MA_FormadePagoDetalle.NumeroDias from MA_FormadePago inner join MA_FormadePagoDetalle on MA_FormadePago.FormadePago=MA_FormadePagoDetalle.FormadePago where MA_FormadePago.FormadePago='" + c.FormadePago + "' and MA_FormadePAgo.CreditoFlag='S'", null);


                    //if (!c.ValidacionLineaCredito)
                    //{
                    //    Otabla.Estado = "PR";
                    //    Otabla.TipoMotivo = "97";
                    //}

                    //if (!c.ValidacionFacturasVencidas)
                    //{
                    //    Otabla.Estado = "PR";
                    //    Otabla.TipoMotivo = "93";
                    //}

                    //if (c.ValidacionDiasVencidoCanjeLetras)
                    //{
                    //    Otabla.Estado = "PR";
                    //    Otabla.TipoMotivo = "92";
                    //}



                    if (c.FormadePagoNuevaFlagCedito == "S" && c.FormadePagoAntiguaFlagCedito == "S")
                    {
                        var DiasCreditoAntigua = UtilsDAO.getValueIntOnly("select MAX(MA_FormadePagoDetalle.NumeroDias) from MA_FormadePago,MA_FormadePagoDetalle where MA_FormadePago.FormadePago=MA_FormadePagoDetalle.FormadePago and MA_FormadePago.FormadePago='" + c.FormadePagoAntigua + "' "); ;
                        var DiasCreditoNueva = UtilsDAO.getValueIntOnly("select MAX(MA_FormadePagoDetalle.NumeroDias) from MA_FormadePago,MA_FormadePagoDetalle where MA_FormadePago.FormadePago=MA_FormadePagoDetalle.FormadePago and MA_FormadePago.FormadePago='" + c.FormadePagoNueva + "' "); ;

                        if (DiasCreditoNueva > DiasCreditoAntigua)
                        {
                            Otabla.Estado = "PR";
                            Otabla.TipoMotivo = "99";
                        }
                    }
                }
                else
                {
                    Otabla.FechaVencimientoOriginal = DateTime.Now;
                    Otabla.Estado = "AP";
                }


                ///VALIDAR STATUS 
                ///

                List<Model_Motivos> lstMotivos = c.lstMotivos;

                if (lstMotivos.Count > 0)
                {
                    StringBuilder cuerpo = new StringBuilder();
                    foreach (var item in lstMotivos)
                    {


                        cuerpo.Append("'" + item.CodigoMotivo + "',");


                    }
                    String Ver = cuerpo.ToString();
                    int charcan = (Ver.Length - 1);
                    String queryRealIn = Ver.Substring(0, charcan);

                    var realQuery = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getReglasPrioridadesMotivos");

                    String queryArmado = realQuery + " and valorcodigo in(" + queryRealIn + ")" + " order by Numero01";

                    var LstReglas = UtilsDAO.getDataByQuery<Model_Motivos>(queryArmado);

                    var ReglaPrioridad = LstReglas.FirstOrDefault();

                    Otabla.TipoMotivo = ReglaPrioridad.CodigoMotivo.Trim();
                    Otabla.Estado = "PR";




                }


                Otabla.MontoPercepcion = c.MontoPercepcion;
                Otabla.flagnuevaversion = "S";
                Otabla.DetraccionCodigo = null;
                Otabla.DetraccionMontoLocal = null;
                //Otabla.TipoMotivo = null;
                Otabla.FechaOrdenCompra = c.FechaOrdenCompra;
                Otabla.FechaRecepcion = c.FechaRecepcion;
                Otabla.FechaRecepcionADV = c.FechaRecepcionADV;
                Otabla.CotizacionNumero = c.CotizacionNumero;
                Otabla.MontoBruto = c.MontoBruto;
                Otabla.MontoFlete = c.MontoFlete;
                Otabla.RecojoFlag = c.RecojoFlag;
                Otabla.CanalVenta = "01";

                Otabla.MontoConvenio = c.MontoConvenio;
                Otabla.Convenio = c.Convenio;

                Otabla.CentroCosto = c.CentroCosto;
                Otabla.TransportistaProvincia = c.TransportistaProvincia;


                Otabla.ClienteDireccionSecuencia = Otabla.ClienteDireccionDespacho;

                context.CO_Documento.Add(Otabla);
                context.SaveChanges();


                response = c;


                foreach (var impuesto in c.DetalleImpuestos)
                {
                    var OtablaImpuesto = new CO_DocumentoImpuesto();
                    OtablaImpuesto.CompaniaSocio = c.CompaniaSocio;
                    OtablaImpuesto.TipoDocumento = c.TipoDocumento;
                    OtablaImpuesto.NumeroDocumento = c.NumeroDocumento;
                    OtablaImpuesto.TipoRegistro = impuesto.TipoRegistro;
                    OtablaImpuesto.Impuesto = impuesto.Impuesto;
                    OtablaImpuesto.Porcentaje = impuesto.Porcentaje;
                    OtablaImpuesto.Monto = impuesto.Monto;

                    context.CO_DocumentoImpuesto.Add(OtablaImpuesto);
                    context.SaveChanges();
                }

                if (c.Detalle != null)
                {
                    foreach (var detalle in c.Detalle)
                    {

                        var secuenciaId = context.CO_DocumentoDetalle.Where(x => x.CompaniaSocio == c.CompaniaSocio && x.TipoDocumento == c.TipoDocumento && x.NumeroDocumento == c.NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Linea);
                        var OtablaDetalle = new CO_DocumentoDetalle();

                        OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                        OtablaDetalle.TipoDocumento = c.TipoDocumento;
                        OtablaDetalle.NumeroDocumento = c.NumeroDocumento;
                        OtablaDetalle.Linea = secuenciaId + 1;
                        OtablaDetalle.TipoDetalle = detalle.TipoDetalle;



                        OtablaDetalle.ItemCodigo = FuncPrinc.trimValor(detalle.ItemCodigo);
                        OtablaDetalle.Descripcion = detalle.Descripcion;

                        OtablaDetalle.CantidadPedida = detalle.CantidadPedida;
                        OtablaDetalle.CantidadEntregada = 0;
                        OtablaDetalle.CantidadSOD = 0;

                        OtablaDetalle.PrecioUnitario = detalle.PrecioUnitario;
                        OtablaDetalle.PrecioUnitarioFinal = detalle.PrecioUnitarioFinal;
                        OtablaDetalle.PrecioUnitarioFlete = detalle.PrecioUnitarioFlete;
                        OtablaDetalle.Monto = (detalle.PrecioUnitario * detalle.CantidadPedida);
                        OtablaDetalle.MontoFinal = (detalle.PrecioUnitarioFinal * detalle.CantidadPedida);


                        OtablaDetalle.MontoFlete = (detalle.PrecioUnitarioFlete * detalle.CantidadPedida);


                        /// LÓGICA PARA AMAZON
                        if (detalle.IGVExoneradoFlag == "S")
                        {
                            OtablaDetalle.IGVExoneradoFlag = "N";
                        }

                        if (detalle.IGVExoneradoFlag == "N")
                        {
                            OtablaDetalle.IGVExoneradoFlag = "S";
                        }

                        //OtablaDetalle.IGVExoneradoFlag = detalle.IGVExoneradoFlag;

                        OtablaDetalle.ImprimirPUFlag = "S";
                        OtablaDetalle.Estado = "PR";
                        OtablaDetalle.UltimoUsuario = c.UltimoUsuario;


                        OtablaDetalle.UltimaFechaModif = DateTime.Now;
                        OtablaDetalle.CantidadPedidaDoble = 0;
                        OtablaDetalle.AlmacenCodigo = detalle.AlmacenCodigoDetalle;

                        if (OtablaDetalle.TipoDetalle == "S" || Otabla.Estado == "PR")
                        {
                            OtablaDetalle.Lote = "00";
                            OtablaDetalle.PrecioModificadoFlag = "S";
                            OtablaDetalle.UnidadCodigo = detalle.UnidadCodigo;
                        }
                        else
                        {
                            OtablaDetalle.Lote = UtilsDAO.getValuString("select Lote from WH_ItemAlmacenLote where Item='" + OtablaDetalle.ItemCodigo + "' and AlmacenCodigo='" + Otabla.AlmacenCodigo + "'", null);
                            OtablaDetalle.PrecioModificadoFlag = "N";
                            OtablaDetalle.UnidadCodigo = detalle.UnidadCodigo;
                        }



                        OtablaDetalle.FlujodeCaja = detalle.FlujodeCaja;


                        OtablaDetalle.CantidadEntregadaDoble = 0;
                        OtablaDetalle.DespachoUnidadEquivalenteFlag = "N";

                        OtablaDetalle.ClienteDireccionDespacho = c.ClienteDireccionDespacho;
                        OtablaDetalle.RutaDespacho = Otabla.RutaDespacho;



                        OtablaDetalle.Condicion = "0";
                        OtablaDetalle.PrecioUnitarioDoble = 0;
                        OtablaDetalle.PrecioNumeroRegistro = detalle.PrecioNumeroRegistro;
                        OtablaDetalle.PrecioUnitarioOriginal = detalle.PrecioUnitarioOriginal;
                        OtablaDetalle.PrecioUnitarioFleteOriginal = detalle.PrecioUnitarioFleteOriginal;

                        OtablaDetalle.TransferenciaGratuitaFlag = "N";
                        OtablaDetalle.PrecioUnitarioGratuito = 0;

                        //OtablaDetalle.TransferenciaGratuitaFlag = detalle.TransferenciaGratuitaFlag;
                        //if (detalle.TransferenciaGratuitaFlag == "S")
                        //{
                        //    detalle.TransferenciaBonificacionFlag = "S";
                        //}
                        //else
                        //{
                        //    detalle.TransferenciaBonificacionFlag = "N";
                        //}
                        OtablaDetalle.TransferenciaBonificacionFlag = detalle.TransferenciaBonificacionFlag;
                        //OtablaDetalle.PrecioUnitarioGratuito = detalle.PrecioUnitarioGratuito;
                        OtablaDetalle.DocumentoRelacTipoDocumento = detalle.DocumentoRelacTipoDocumento;
                        OtablaDetalle.TipoPromocion = detalle.TipoPromocion;
                        OtablaDetalle.DocumentoRelacNumeroDocumento = detalle.DocumentoRelacNumeroDocumento;
                        OtablaDetalle.DocumentoRelacLinea = detalle.DocumentoRelacLinea;



                        OtablaDetalle.PromocionNumero = detalle.PromocionNumero;
                        OtablaDetalle.PorcentajeDescuento01 = detalle.PorcentajeDescuento01;
                        //OtablaDetalle.PorcentajeDescuento02 = detalle.PorcentajeDescuento02;
                        //OtablaDetalle.PorcentajeDescuento03 = detalle.PorcentajeDescuento03;
                        OtablaDetalle.PorcentajeDescuento02 = 0;
                        OtablaDetalle.PorcentajeDescuento03 = 0;
                        OtablaDetalle.MargenPorcentaje = detalle.MargenPorcentaje;
                        OtablaDetalle.ComprometeFlag = "N";
                        OtablaDetalle.RutaDespachoFechaAsignacion = detalle.RutaDespachoFechaAsignacion;
                        OtablaDetalle.MontoDescuento = detalle.MontoDescuento;
                        OtablaDetalle.PesoUnitario = detalle.PesoUnitario;
                        OtablaDetalle.CodigoFlete = detalle.CodigoFlete;
                        OtablaDetalle.Promocion = detalle.Promocion;
                        OtablaDetalle.IndBonificacion = detalle.IndBonificacion;
                        OtablaDetalle.PrecioUnitarioFlete02 = 0;
                        OtablaDetalle.LineaCorte = 1;
                        OtablaDetalle.NroCortes = 0;
                        OtablaDetalle.GuiaSecuencia = 0;
                        OtablaDetalle.CantidadPedidaFinal = 1;
                        OtablaDetalle.NroCortesFinal = 0;

                        OtablaDetalle.MontoConvenio = detalle.MontoConvenio;
                        OtablaDetalle.PrecioUnitarioConvenio = 0;


                        OtablaDetalle.PrecioUnitarioConvenio = 0;


                        context.CO_DocumentoDetalle.Add(OtablaDetalle);
                        context.SaveChanges();

                        /// INSERTAR DESPACHODETALLE

                        //var secuenciaIddespacho = context.CO_DocumentoDetalleDespacho.Where(x => x.CompaniaSocio == c.CompaniaSocio && x.TipoDocumento == c.TipoDocumento && x.NumeroDocumento == c.NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Secuencia);

                        if (Otabla.Estado != "PR")
                        {

                            var OtablaDetalleDespacho = new CO_DocumentoDetalleDespacho();

                            OtablaDetalleDespacho.CompaniaSocio = c.CompaniaSocio;
                            OtablaDetalleDespacho.TipoDocumento = c.TipoDocumento;
                            OtablaDetalleDespacho.NumeroDocumento = c.NumeroDocumento;
                            OtablaDetalleDespacho.Linea = OtablaDetalle.Linea;
                            OtablaDetalleDespacho.Secuencia = 1;
                            OtablaDetalleDespacho.Turno = 1;


                            OtablaDetalleDespacho.Cantidad = detalle.CantidadPedida;
                            OtablaDetalleDespacho.AlmacenCodigo = detalle.AlmacenCodigoDetalle; ;
                            OtablaDetalleDespacho.AlmacenPrincipal = detalle.AlmacenCodigoDetalle; ;
                            OtablaDetalleDespacho.FechaEntrega = c.ComercialPedidoFechaRequerida;
                            OtablaDetalleDespacho.Estado = "PE";
                            OtablaDetalleDespacho.UltimaFechaModif = DateTime.Now;
                            OtablaDetalleDespacho.UltimoUsuario = c.UltimoUsuario;

                            OtablaDetalleDespacho.CantidadSOD = 0;
                            OtablaDetalleDespacho.CantidadMerma = 0;
                            OtablaDetalleDespacho.CantidadTotal = detalle.CantidadPedida;
                            OtablaDetalleDespacho.Item = detalle.ItemCodigo;
                            OtablaDetalleDespacho.AlmacenPrincipal = detalle.AlmacenCodigoDetalle; ;
                            OtablaDetalleDespacho.CantidadRecibida = 0;
                            OtablaDetalleDespacho.ClienteDireccionDespacho = c.ClienteDireccionDespacho;
                            OtablaDetalleDespacho.TipoRegistro = "P";
                            OtablaDetalleDespacho.ComprometeFlag = "N";
                            OtablaDetalleDespacho.FechaEntrega = Otabla.FechaDocumento;


                            context.CO_DocumentoDetalleDespacho.Add(OtablaDetalleDespacho);
                            context.SaveChanges();

                        }




                    }

                }

            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.CodigoError = 500;
                        error.MensajeError = ve.ErrorMessage;

                        response.lstErrores.Add(error);
                    }

                }

                return response;

            }
            return response;
        }

        private ModelTransac_CO_Documento InsertarLetras(ModelTransac_CO_Documento c, BdEntityGenerico context)
        {

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            ErrorObj error = new ErrorObj();



            var company = "999999";
            c.TipoDocumento = "SY";
            c.Sucursal = "COCA";
            var NumeroDocumento = Convert.ToInt32(c.NumeroDocumento);

            if (c.Accion == "InsertarBlog")
            {
                if (c.LstBLogs != null)
                {
                    foreach (var detalle in c.LstBLogs)
                    {

                        try
                        {


                            var validaUpdate = context.CO_OperacionCanjeComentario.SingleOrDefault(x => x.OperacionCanjeNumero == NumeroDocumento && x.Secuencia == detalle.Linea);
                            //var validaUpdate = context.CO_OperacionCanjeComentario.Where(x => x.OperacionCanjeNumero == NumeroDocumento).FirstOrDefault();

                            if (validaUpdate == null)
                            {
                                var secuenciaId = context.CO_OperacionCanjeComentario.Where(x => x.OperacionCanjeNumero == NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Secuencia);
                                var OtablaDetalle = new CO_OperacionCanjeComentario();
                                //OtablaDetalle.TipoDocumento = c.TipoDocumento;
                                OtablaDetalle.OperacionCanjeNumero = Convert.ToInt32(c.NumeroDocumento);
                                OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                                OtablaDetalle.Comentario = detalle.Comentario;
                                OtablaDetalle.Estado = "A";
                                OtablaDetalle.UltimoUsuario = c.UltimoUsuario;
                                OtablaDetalle.UltimaFechaModif = detalle.UltimaFechaModif;
                                OtablaDetalle.Secuencia = secuenciaId + 1;

                                context.CO_OperacionCanjeComentario.Add(OtablaDetalle);
                                context.SaveChanges();
                            }
                            else
                            {

                                if (detalle.Estado == "EL")
                                {
                                    context.Entry(validaUpdate).State = System.Data.Entity.EntityState.Deleted;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    validaUpdate.Comentario = detalle.Comentario;
                                    context.Entry(validaUpdate).State = System.Data.Entity.EntityState.Modified;
                                    context.SaveChanges();
                                }




                            }



                        }
                        catch (DbEntityValidationException e)
                        {

                            foreach (var eve in e.EntityValidationErrors)
                            {
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    error.CodigoError = 500;
                                    error.MensajeError = ve.ErrorMessage;

                                    response.lstErrores.Add(error);
                                }

                            }

                            return response;

                        }


                    }

                    return c;
                }
            }

            var CorrelativosMast = context.CorrelativosMast.DefaultIfEmpty().Where(t => t.CompaniaCodigo == company
            && t.TipoComprobante == c.TipoDocumento && t.Serie == c.Sucursal).ToList();
            int correlativo = 0;
            int? NumeroInterno = 0;
            if (CorrelativosMast.Count > 0)
            {
                correlativo = (int)((CorrelativosMast[0].CorrelativoNumero) + 1);
                c.NumeroDocumento = correlativo.ToString("D10");
            }
            else
            {
                error.CodigoError = 500;
                error.MensajeError = "Error al crear el correlativo, correlativo no encontrado .. Parametros Comapnia : " + company + " Tipo Documento : " + c.TipoDocumento + " Serie :" + c.Sucursal;
                response.lstErrores.Add(error);
                return response;
            }

            try
            {

                var OtablaCorreclativo = context.CorrelativosMast.SingleOrDefault(t => t.CompaniaCodigo == company && t.TipoComprobante == c.TipoDocumento && t.Serie == c.Sucursal);
                if (OtablaCorreclativo != null)
                {

                    OtablaCorreclativo.CorrelativoNumero = correlativo;

                    context.Entry(OtablaCorreclativo).State = System.Data.Entity.EntityState.Modified;
                }




                var Otabla = new CO_OperacionCanje();



                c.NumeroDocumento = "" + correlativo;
                NumeroDocumento = correlativo;
                Otabla.CompaniaSocio = c.CompaniaSocio;
                Otabla.OperacionCanjeNumero = Convert.ToInt32(c.NumeroDocumento);
                Otabla.Cliente = c.ClienteNumero;
                Otabla.ClienteCobrarA = c.ClienteNumero;
                Otabla.ClienteDireccionSecuencia = c.ClienteDireccionDespacho;
                Otabla.Vendedor = c.Vendedor;
                Otabla.LetrasCantidad = c.CantidadLetras;
                Otabla.FechaBase = c.FechaBaseLetras;
                Otabla.FechaMaxima = Convert.ToDateTime(c.FechaBaseLetras).AddDays((double)c.DiasCredito);
                Otabla.DiasCanje = c.DiasCredito;
                Otabla.Comentarios = c.Comentarios;
                Otabla.Estado = "PR";
                Otabla.PreparadoPor = c.IdPersonaUsuario;
                Otabla.FechaPreparacion = DateTime.Now;
                Otabla.UltimaFechaModif = Otabla.FechaPreparacion;
                Otabla.UltimoUsuario = c.UltimoUsuario;
                string dateString = DateTime.Now.ToString("yyyyMM", CultureInfo.InvariantCulture);
                Otabla.VoucherPeriodo = dateString;
                Otabla.Procedencia = c.Procedencia;
                Otabla.VoucherNo = null;
                Otabla.DiasAdicionales = null;
                Otabla.NumeroSolicitud = null;
                Otabla.TipoOperacion = null;
                Otabla.FinanciamientoNumeroDocumento = null;
                Otabla.FinanciamientoTipoDocumento = null;

                context.CO_OperacionCanje.Add(Otabla);
                context.SaveChanges();


                response = c;

                var FactorDivisor = UtilsDAO.getValueDecimal("select Numero from ParametrosMast where ParametroClave = 'VENTA%VEN' and AplicacionCodigo='CO'", null);


                if (c.Detalle != null)
                {

                    foreach (var detalle in c.Detalle)
                    {

                        var secuenciaId = context.CO_OperacionCanjeDetalle.Where(x => x.OperacionCanjeNumero == NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Linea);
                        var OtablaDetalle = new CO_OperacionCanjeDetalle();


                        OtablaDetalle.OperacionCanjeNumero = Convert.ToInt32(c.NumeroDocumento);
                        OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                        OtablaDetalle.Linea = secuenciaId + 1;
                        OtablaDetalle.InputOutputFlag = "I";
                        OtablaDetalle.NumeroDocumento = detalle.Descripcion;
                        OtablaDetalle.TipoDocumento = detalle.TipoDocumento;
                        OtablaDetalle.Monto = detalle.MontoFinal;
                        OtablaDetalle.FechaVencimiento = FuncPrinc.ConvertDateFromString(detalle.FechaVencimiento);
                        OtablaDetalle.MontoComision = (detalle.MontoFinal * FactorDivisor) / 100;

                        context.CO_OperacionCanjeDetalle.Add(OtablaDetalle);
                        context.SaveChanges();

                    }

                }

                if (c.LstLetras != null)
                {
                    foreach (var detalle in c.LstLetras)
                    {

                        //var secuenciaId = context.CO_LetraCompromisoLetra.Where(x => x.NumeroSolicitud == c.NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Secuencia);
                        //var OtablaDetalle = new CO_LetraCompromisoLetra(); 
                        //OtablaDetalle.NumeroSolicitud = c.NumeroDocumento;
                        //OtablaDetalle.Secuencia = secuenciaId+1;
                        //OtablaDetalle.FechaEmision = (DateTime)c.FechaBaseLetras;
                        //OtablaDetalle.FechaVencimiento = (DateTime)detalle.FechaVencimientoDate;
                        //OtablaDetalle.MontoLetra = (decimal)detalle.MontoTotalLetras;


                        //context.CO_LetraCompromisoLetra.Add(OtablaDetalle);
                        //context.SaveChanges();


                        var secuenciaId = context.CO_OperacionCanjeDetalle.Where(x => x.OperacionCanjeNumero == NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Linea);
                        var OtablaDetalle = new CO_OperacionCanjeDetalle();

                        OtablaDetalle.Linea = secuenciaId + 1;
                        OtablaDetalle.OperacionCanjeNumero = Convert.ToInt32(c.NumeroDocumento);
                        OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                        OtablaDetalle.InputOutputFlag = "O";
                        //OtablaDetalle.NumeroDocumento = detalle.Descripcion;
                        //OtablaDetalle.TipoDocumento = detalle.TipoDocumento;
                        OtablaDetalle.Monto = (decimal)detalle.MontoTotalLetras;
                        OtablaDetalle.MontoComision = (OtablaDetalle.Monto * FactorDivisor) / 100;

                        context.CO_OperacionCanjeDetalle.Add(OtablaDetalle);
                        context.SaveChanges();

                    }

                }

            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.CodigoError = 500;
                        error.MensajeError = ve.ErrorMessage;

                        response.lstErrores.Add(error);
                    }

                }

                return response;

            }
            return response;
        }
        
        private ModelTransac_CO_Documento ActualizarLetras(ModelTransac_CO_Documento c, BdEntityGenerico context)
        {

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            ErrorObj error = new ErrorObj();

             

            try
            {

                 

                var Otabla = new CO_OperacionCanje();

                var OperacionCanjeNumero = c.OperacionCanjeNumero;

                Otabla = context.CO_OperacionCanje.Where(t => t.CompaniaSocio == c.CompaniaSocio  && t.OperacionCanjeNumero == OperacionCanjeNumero).FirstOrDefault();

 

                //var cabecera = Newtonsoft.Json.JsonConvert.SerializeObject(CO_DOCUMENTO, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                // Otabla = (CO_OperacionCanje)Newtonsoft.Json.JsonConvert.DeserializeObject(cabecera, typeof(CO_OperacionCanje));

                 
                Otabla.LetrasCantidad = c.CantidadLetras;
                Otabla.FechaBase = c.FechaBaseLetras;
                Otabla.FechaMaxima = Convert.ToDateTime(c.FechaBaseLetras).AddDays((double)c.DiasCredito);
                Otabla.DiasCanje = c.DiasCredito;
                Otabla.Comentarios = c.Comentarios;  
                Otabla.UltimaFechaModif = DateTime.Now;
                Otabla.UltimoUsuario = c.UltimoUsuario;

                //Otabla.CO_OperacionCanjeDetalle.Clear();
                //Otabla.CO_OperacionCanjeDetalle1.Clear();
                //Otabla.CO_OperacionCanjeDetalle2.Clear();
                //Otabla.CO_OperacionCanjeDetalle3.Clear();
                //Otabla.CO_OperacionCanjeDetalle4.Clear();
                //Otabla.CO_OperacionCanjeDetalle5.Clear();
                //Otabla.CO_OperacionCanjeDetalle6.Clear();
                //Otabla.CO_OperacionCanjeDetalle7.Clear();

                context.Entry(Otabla).State = System.Data.Entity.EntityState.Modified;

                 
                context.SaveChanges();


                response = c;

                var FactorDivisor = UtilsDAO.getValueDecimal("select Numero from ParametrosMast where ParametroClave = 'VENTA%VEN' and AplicacionCodigo='CO'", null);


         

                 
               var  OtablaDetalleObBorrar = context.CO_OperacionCanjeDetalle.Where(t => t.CompaniaSocio == c.CompaniaSocio && t.OperacionCanjeNumero == OperacionCanjeNumero).ToList();
                 
                foreach (var detalle in OtablaDetalleObBorrar)
                { 
                    context.Entry(detalle).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }

                if (c.Detalle != null)
                {

                    foreach (var detalle in c.Detalle)
                    {

                        var secuenciaId = context.CO_OperacionCanjeDetalle.Where(x => x.OperacionCanjeNumero == Otabla.OperacionCanjeNumero).DefaultIfEmpty().Max(t => t == null ? 0 : t.Linea);
                        var OtablaDetalle = new CO_OperacionCanjeDetalle();


                        OtablaDetalle.OperacionCanjeNumero = Otabla.OperacionCanjeNumero;
                        OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                        OtablaDetalle.Linea = secuenciaId + 1;
                        OtablaDetalle.InputOutputFlag = "I";
                        OtablaDetalle.NumeroDocumento = detalle.Descripcion;
                        OtablaDetalle.TipoDocumento = detalle.TipoDocumento;
                        OtablaDetalle.Monto = detalle.MontoFinal;
                        OtablaDetalle.FechaVencimiento = FuncPrinc.ConvertDateFromString(detalle.FechaVencimiento);
                        OtablaDetalle.MontoComision = (detalle.MontoFinal * FactorDivisor) / 100;

                        context.CO_OperacionCanjeDetalle.Add(OtablaDetalle);
                        context.SaveChanges();

                    }

                }

                if (c.LstLetras != null)
                {
                    foreach (var detalle in c.LstLetras)
                    {

                         

                        var secuenciaId = context.CO_OperacionCanjeDetalle.Where(x => x.OperacionCanjeNumero == Otabla.OperacionCanjeNumero).DefaultIfEmpty().Max(t => t == null ? 0 : t.Linea);
                        var OtablaDetalle = new CO_OperacionCanjeDetalle();

                        OtablaDetalle.Linea = secuenciaId + 1;
                        OtablaDetalle.OperacionCanjeNumero = Otabla.OperacionCanjeNumero;
                        OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                        OtablaDetalle.InputOutputFlag = "O";
                        //OtablaDetalle.NumeroDocumento = detalle.Descripcion;
                        //OtablaDetalle.TipoDocumento = detalle.TipoDocumento;
                        OtablaDetalle.Monto = (decimal)detalle.MontoTotalLetras;
                        OtablaDetalle.MontoComision = (OtablaDetalle.Monto * FactorDivisor) / 100;
                        OtablaDetalle.FechaVencimiento = detalle.FechaVencimientoDate;
                        context.CO_OperacionCanjeDetalle.Add(OtablaDetalle);
                        context.SaveChanges();

                    }

                }

            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.CodigoError = 500;
                        error.MensajeError = ve.ErrorMessage;

                        response.lstErrores.Add(error);
                    }

                }

                return response;

            }

            //response.IdPersonaUsuario = 0;
            return response;
        }

        private ModelTransac_CO_Documento InsertarFacturacion(ModelTransac_CO_Documento c, BdEntityGenerico context)
        {

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            ErrorObj error = new ErrorObj();

            String Compania = c.CompaniaSocio;
            String TipoDocumento = c.TipoDocumento;
            String NumeroDOcumento = c.NumeroDocumento;

            String Sucursal = "";


            var companyAll = c.CompaniaSocio;
            var company = c.CompaniaSocio.Substring(0, 6);

            String validaDocumento = UtilsDAO.getValuString("select codigofiscal from ClienteMast cli  inner join CO_TipoDocumento  doc on doc.TipoDocumento = cli.TipoDocumento  where Cliente = " + c.ClienteNumero + "", null);

            if (validaDocumento == "")
            {
                error.CodigoError = 500;
                error.MensajeError = "El Cliente : " + c.ClienteNombre + ", no tiene configuracion de TipoDocumento en ClienteMast, verifique el tipo de documento con CO_TipoDocumento";
                response.lstErrores.Add(error);
                return response;
            }

            if (validaDocumento == "01")
            {
                Sucursal = UtilsDAO.getValuString("select valorstring from SY_Preferences with(nolock) where AplicacionCodigo='CO' and Preference='Serie-FC' and Usuario='" + c.UltimoUsuario + "'", null);
            }

            if (validaDocumento == "03")
            {
                Sucursal = UtilsDAO.getValuString("select valorstring from SY_Preferences with(nolock) where AplicacionCodigo='CO' and Preference='Serie-BV' and Usuario='" + c.UltimoUsuario + "'", null);
            }

            if (Sucursal == "")
            {
                error.CodigoError = 500;
                error.MensajeError = "El Usuario : " + c.UltimoUsuario + ", las boletas y facturas en sus preferencias";
                response.lstErrores.Add(error);
                return response;
            }


            //c.Sucursal = "BOA1";
            c.Sucursal = Sucursal;
            //c.TipoDocumento = "BV";
            c.TipoDocumento = c.TipoDocumentoPedidoCLiente;



            var CorrelativosMast = context.CorrelativosMast.DefaultIfEmpty().Where(t => t.CompaniaCodigo == company
             && t.TipoComprobante == c.TipoDocumento && t.Serie == c.Sucursal).ToList();
            int correlativo = 0;
            int? NumeroInterno = 0;
            if (CorrelativosMast.Count > 0)
            {
                correlativo = (int)((CorrelativosMast[0].CorrelativoNumero) + 1);
                var serie = (CorrelativosMast[0].Serie);
                //serie = serie.Substring(2, 2);
                c.NumeroDocumento = serie + "-" + correlativo.ToString("D7");
            }
            else
            {
                error.CodigoError = 500;
                error.MensajeError = "Error al crear el correlativo ";
                response.lstErrores.Add(error);
                return response;
            }

            try
            {

                var OtablaCorreclativo = context.CorrelativosMast.SingleOrDefault(t => t.CompaniaCodigo == company && t.TipoComprobante == c.TipoDocumento && t.Serie == c.Sucursal);
                if (OtablaCorreclativo != null)
                {

                    OtablaCorreclativo.CorrelativoNumero = correlativo;

                    context.Entry(OtablaCorreclativo).State = System.Data.Entity.EntityState.Modified;
                }


                var CO_DOCUMENTO = context.CO_Documento.Where(t => t.CompaniaSocio == Compania && t.TipoDocumento == TipoDocumento && t.NumeroDocumento == NumeroDOcumento).FirstOrDefault();
                var cabecera = Newtonsoft.Json.JsonConvert.SerializeObject(CO_DOCUMENTO, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                var Otabla = (CO_Documento)Newtonsoft.Json.JsonConvert.DeserializeObject(cabecera, typeof(CO_Documento));


                Otabla.CO_DocumentoDetalle.Clear();
                Otabla.CO_DocumentoDetalle1.Clear();
                Otabla.CO_DocumentoDetalle2.Clear();
                Otabla.CO_DocumentoDetalle3.Clear();
                Otabla.CO_DocumentoDetalleDespacho.Clear();
                Otabla.CO_DocumentoImpuesto.Clear();
                Otabla.CO_DocumentoImpuesto1.Clear();
                Otabla.CO_DocumentoImpuesto2.Clear();
                Otabla.CO_DocumentoImpuesto3.Clear();


                Otabla.ComercialPedidoNumero = Otabla.TipoDocumento + Otabla.NumeroDocumento;
                Otabla.CompaniaSocio = c.CompaniaSocio;
                Otabla.TipoDocumento = c.TipoDocumento;
                Otabla.NumeroDocumento = c.NumeroDocumento;
                Otabla.FechaDocumento = DateTime.Now;
                Otabla.TipodeCambio = c.TipodeCambio;
                Otabla.CodigoQR = FuncPrinc.GenerarQRSunat(Otabla, "20603858329", "03", "03");
                Otabla.APProcesoNumero = 0;
                Otabla.APProcesoSecuencia = 0;
                Otabla.MontoPercepcion = 0;
                Otabla.Estado = "PR";
                //Otabla.DocumentoSinCantidadFlag = "N";
                Otabla.MontoRetenidoFlag = "N";
                Otabla.UltimoUsuario = c.UltimoUsuario;
                Otabla.UltimaFechaModif = Otabla.FechaDocumento;
                context.CO_Documento.Add(Otabla);
                context.SaveChanges();

                c.ComercialPedidoNumero = Otabla.ComercialPedidoNumero;
                response = c;


                var CO_DOCUMENTOIMPUESTO = context.CO_DocumentoImpuesto.Where(t => t.CompaniaSocio == Compania && t.TipoDocumento == TipoDocumento && t.NumeroDocumento == NumeroDOcumento).ToList();
                var cabeceraImpuesto = Newtonsoft.Json.JsonConvert.SerializeObject(CO_DOCUMENTOIMPUESTO, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                var OtablaImpuestos = (List<CO_DocumentoImpuesto>)Newtonsoft.Json.JsonConvert.DeserializeObject(cabeceraImpuesto, typeof(List<CO_DocumentoImpuesto>));



                foreach (var impuesto in OtablaImpuestos)
                {
                    impuesto.CO_Documento = Otabla;
                    impuesto.CO_Documento1 = Otabla;
                    impuesto.CO_Documento2 = Otabla;
                    impuesto.CO_Documento3 = Otabla;
                    impuesto.CompaniaSocio = c.CompaniaSocio;
                    impuesto.TipoDocumento = c.TipoDocumento;
                    impuesto.NumeroDocumento = c.NumeroDocumento;

                    context.CO_DocumentoImpuesto.Add(impuesto);
                    context.SaveChanges();
                }


                var CO_DOCUMENTODETALLE = context.CO_DocumentoDetalle.Where(t => t.CompaniaSocio == Compania && t.TipoDocumento == TipoDocumento && t.NumeroDocumento == NumeroDOcumento).ToList();
                var detallePedido = Newtonsoft.Json.JsonConvert.SerializeObject(CO_DOCUMENTODETALLE, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                var OtablaDetallePedido = (List<CO_DocumentoDetalle>)Newtonsoft.Json.JsonConvert.DeserializeObject(detallePedido, typeof(List<CO_DocumentoDetalle>));



                foreach (var OtablaDetalle in OtablaDetallePedido)
                {

                    //OtablaDetalle.CO_Documento = Otabla;
                    OtablaDetalle.CO_Documento = Otabla;
                    OtablaDetalle.CO_Documento1 = Otabla;
                    OtablaDetalle.CO_Documento2 = Otabla;
                    OtablaDetalle.CO_Documento3 = Otabla;
                    OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                    OtablaDetalle.TipoDocumento = c.TipoDocumento;
                    OtablaDetalle.NumeroDocumento = c.NumeroDocumento;
                    OtablaDetalle.UltimoUsuario = c.UltimoUsuario;
                    OtablaDetalle.UltimaFechaModif = Otabla.FechaDocumento;
                    OtablaDetalle.DocumentoRelacTipoDocumento = "PE";
                    OtablaDetalle.DocumentoRelacNumeroDocumento = NumeroDOcumento;
                    OtablaDetalle.DocumentoRelacLinea = OtablaDetalle.Linea;


                    context.CO_DocumentoDetalle.Add(OtablaDetalle);
                    context.SaveChanges();

                }


                var CO_DOCUMENTODETALLEDESPACHO = context.CO_DocumentoDetalleDespacho.Where(t => t.CompaniaSocio == Compania && t.TipoDocumento == TipoDocumento && t.NumeroDocumento == NumeroDOcumento).ToList();
                var detallePedidoDespacho = Newtonsoft.Json.JsonConvert.SerializeObject(CO_DOCUMENTODETALLEDESPACHO, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                var OtablaDetallePedidoDespacho = (List<CO_DocumentoDetalleDespacho>)Newtonsoft.Json.JsonConvert.DeserializeObject(detallePedidoDespacho, typeof(List<CO_DocumentoDetalleDespacho>));


                foreach (var OtablaDetalleDespacho in OtablaDetallePedidoDespacho)
                {

                    //OtablaDetalleDespacho.CO_Documento = Otabla;
                    OtablaDetalleDespacho.CO_Documento = Otabla;
                    OtablaDetalleDespacho.CompaniaSocio = c.CompaniaSocio;
                    OtablaDetalleDespacho.TipoDocumento = c.TipoDocumento;
                    OtablaDetalleDespacho.NumeroDocumento = c.NumeroDocumento;
                    OtablaDetalleDespacho.FechaEntrega = Otabla.FechaDocumento;
                    OtablaDetalleDespacho.Turno = 1;
                    OtablaDetalleDespacho.Estado = "PE";
                    OtablaDetalleDespacho.CantidadMerma = 0;
                    OtablaDetalleDespacho.Secuencia = 1;

                    context.CO_DocumentoDetalleDespacho.Add(OtablaDetalleDespacho);
                    context.SaveChanges();

                }


                //ACTUALIZAR ESTADOS

                CO_DOCUMENTO.Estado = "FA";
                CO_DOCUMENTO.ComercialPedidoNumero = c.TipoDocumento + c.NumeroDocumento;
                context.Entry(CO_DOCUMENTO).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                response.Estado = "FA";
                response.Plataforma = c.Plataforma;
                response.CodigoQR = Otabla.CodigoQR;

                foreach (var item in CO_DOCUMENTODETALLE.ToList())
                {
                    item.CantidadEntregada = item.CantidadEntregada + 1;
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.CodigoError = 500;
                        error.MensajeError = ve.ErrorMessage;

                        response.lstErrores.Add(error);
                    }

                }

                return response;

            }
            return response;
        }

        private ModelTransac_CO_Documento ActualizarPedido(ModelTransac_CO_Documento c, BdEntityGenerico context)
        {

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            ErrorObj error = new ErrorObj();


            try
            {
                c.NumeroDocumento = c.NumeroDocumento.Trim();
                var Otabla = context.CO_Documento.Where(t => t.CompaniaSocio == c.CompaniaSocio && t.TipoDocumento == c.TipoDocumento && t.NumeroDocumento == c.NumeroDocumento).FirstOrDefault();
                if (Otabla != null)
                {

                    Otabla.MontoAfecto = c.MontoAfecto;
                    Otabla.MontoNoAfecto = c.MontoNoAfecto;
                    Otabla.MontoImpuestoVentas = c.MontoImpuestoVentas;
                    Otabla.MontoImpuestos = c.MontoImpuestos;
                    Otabla.MontoDescuentos = c.MontoDescuentos;
                    Otabla.MontoTotal = c.MontoTotal;
                    Otabla.FormadePago = c.FormadePago;
                    Otabla.RecojoFlag = c.RecojoFlag;
                    Otabla.MonedaDocumento = c.MonedaDocumento;

                    Otabla.TransportistaProvincia = c.TransportistaProvincia;
                    Otabla.Comentarios = c.Comentarios;


                    String creditoFlag = UtilsDAO.getValuString("select CreditoFlag from MA_FormadePAgo where FormadePago = '" + c.FormadePago + "'", null);

                    if (c.CreditoFlag == "S")
                    {
                        Otabla.FechaVencimientoOriginal = UtilsDAO.getValueDatetime("select GETDATE()+MA_FormadePagoDetalle.NumeroDias from MA_FormadePago inner join MA_FormadePagoDetalle on MA_FormadePago.FormadePago=MA_FormadePagoDetalle.FormadePago where MA_FormadePago.FormadePago='" + c.FormadePago + "' and MA_FormadePAgo.CreditoFlag='S'", null);
                    }
                    else
                    {
                        Otabla.FechaVencimientoOriginal = DateTime.Now;
                    }


                    Otabla.UltimaFechaModif = DateTime.Now;
                    Otabla.UltimoUsuario = c.UltimoUsuario;
                    context.Entry(Otabla).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();


                    response.TipoDocumento = Otabla.TipoDocumento;
                    response.NumeroDocumento = Otabla.NumeroDocumento;
                    response.CompaniaSocio = Otabla.CompaniaSocio;
                }
                //response = c; 

                foreach (var impuesto in c.DetalleImpuestos)
                {
                    var OtablaImpuesto = context.CO_DocumentoImpuesto.Where(x => x.CompaniaSocio == c.CompaniaSocio && x.TipoDocumento == c.TipoDocumento && x.NumeroDocumento == c.NumeroDocumento).FirstOrDefault();

                    //OtablaImpuesto.CompaniaSocio = c.CompaniaSocio;
                    //OtablaImpuesto.TipoDocumento = c.TipoDocumento;
                    //OtablaImpuesto.NumeroDocumento = c.NumeroDocumento;
                    OtablaImpuesto.TipoRegistro = impuesto.TipoRegistro;
                    //OtablaImpuesto.Impuesto = impuesto.Impuesto;
                    OtablaImpuesto.Porcentaje = impuesto.Porcentaje;
                    OtablaImpuesto.Monto = impuesto.Monto;
                    context.Entry(OtablaImpuesto).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                if (c.Detalle != null)
                {
                    foreach (var detalle in c.Detalle)
                    {

                        //context.CO_Documento.SingleOrDefault
                        var objdetalle = context.CO_DocumentoDetalle.SingleOrDefault(x => x.CompaniaSocio == c.CompaniaSocio && x.TipoDocumento == c.TipoDocumento && x.NumeroDocumento == c.NumeroDocumento && x.Linea == detalle.Linea);

                        if (objdetalle != null)
                        {

                            objdetalle.UnidadCodigo = detalle.UnidadCodigo;
                            objdetalle.CantidadPedida = detalle.CantidadPedida;
                            objdetalle.CantidadEntregada = 0;
                            objdetalle.CantidadSOD = 0;
                            objdetalle.PrecioUnitario = detalle.PrecioUnitario;
                            objdetalle.PrecioUnitarioFinal = detalle.PrecioUnitarioFinal;
                            objdetalle.PrecioUnitarioFlete = detalle.PrecioUnitarioFlete;
                            objdetalle.Monto = (detalle.PrecioUnitario * detalle.CantidadPedida);
                            objdetalle.MontoFinal = (detalle.PrecioUnitarioFinal * detalle.CantidadPedida);
                            objdetalle.MontoFlete = (detalle.PrecioUnitarioFlete * detalle.CantidadPedida);
                            objdetalle.PrecioUnitarioDoble = 0;
                            objdetalle.PrecioNumeroRegistro = detalle.PrecioNumeroRegistro;
                            objdetalle.PrecioUnitarioOriginal = detalle.PrecioUnitarioOriginal;
                            objdetalle.PrecioUnitarioFleteOriginal = detalle.PrecioUnitarioFleteOriginal;
                            objdetalle.TransferenciaGratuitaFlag = "N";
                            objdetalle.PrecioUnitarioGratuito = 0;
                            objdetalle.UltimaFechaModif = DateTime.Now;
                            objdetalle.UltimoUsuario = c.UltimoUsuario;



                            if (objdetalle.TipoDetalle == "S")
                            {

                                objdetalle.PrecioModificadoFlag = "S";
                                objdetalle.UnidadCodigo = "UND";
                            }
                            else
                            {
                                objdetalle.UnidadCodigo = detalle.UnidadCodigo;
                            }

                            context.Entry(objdetalle).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();

                            if (Otabla.Estado != "PR")
                            {
                                var objdetalleDespacho = context.CO_DocumentoDetalleDespacho.Where(x => x.CompaniaSocio == c.CompaniaSocio && x.TipoDocumento == c.TipoDocumento && x.NumeroDocumento == c.NumeroDocumento).FirstOrDefault();


                                objdetalleDespacho.UltimaFechaModif = DateTime.Now;
                                objdetalleDespacho.UltimoUsuario = c.UltimoUsuario;
                                objdetalleDespacho.CantidadSOD = 0;
                                objdetalleDespacho.CantidadMerma = 0;
                                objdetalleDespacho.CantidadTotal = detalle.CantidadPedida;
                                context.Entry(objdetalleDespacho).State = System.Data.Entity.EntityState.Modified;
                                context.SaveChanges();
                            }



                        }
                        else
                        {

                            var secuenciaId = context.CO_DocumentoDetalle.Where(x => x.CompaniaSocio == c.CompaniaSocio && x.TipoDocumento == c.TipoDocumento && x.NumeroDocumento == c.NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Linea);
                            var OtablaDetalle = new CO_DocumentoDetalle();
                            OtablaDetalle.CompaniaSocio = c.CompaniaSocio;
                            OtablaDetalle.TipoDocumento = c.TipoDocumento;
                            OtablaDetalle.NumeroDocumento = c.NumeroDocumento;
                            OtablaDetalle.Linea = secuenciaId + 1;
                            OtablaDetalle.TipoDetalle = detalle.TipoDetalle;
                            OtablaDetalle.ItemCodigo = FuncPrinc.trimValor(detalle.ItemCodigo);
                            OtablaDetalle.Descripcion = detalle.Descripcion;
                            OtablaDetalle.UnidadCodigo = detalle.UnidadCodigo;
                            OtablaDetalle.CantidadPedida = detalle.CantidadPedida;
                            OtablaDetalle.CantidadEntregada = 0;
                            OtablaDetalle.CantidadSOD = 0;
                            OtablaDetalle.PrecioUnitario = detalle.PrecioUnitario;
                            OtablaDetalle.PrecioUnitarioFinal = detalle.PrecioUnitarioFinal;
                            OtablaDetalle.PrecioUnitarioFlete = detalle.PrecioUnitarioFlete;
                            OtablaDetalle.Monto = (detalle.PrecioUnitario * detalle.CantidadPedida);
                            OtablaDetalle.MontoFinal = (detalle.PrecioUnitarioFinal * detalle.CantidadPedida);
                            OtablaDetalle.MontoFlete = (detalle.PrecioUnitarioFlete * detalle.CantidadPedida);
                            /// LÓGICA PARA AMAZON
                            if (detalle.IGVExoneradoFlag == "S")
                            {
                                OtablaDetalle.IGVExoneradoFlag = "N";
                            }

                            if (detalle.IGVExoneradoFlag == "N")
                            {
                                OtablaDetalle.IGVExoneradoFlag = "S";
                            }

                            //OtablaDetalle.IGVExoneradoFlag = detalle.IGVExoneradoFlag;

                            OtablaDetalle.ImprimirPUFlag = "S";
                            OtablaDetalle.Estado = "PR";
                            OtablaDetalle.UltimoUsuario = c.UltimoUsuario;
                            OtablaDetalle.UltimaFechaModif = DateTime.Now;
                            OtablaDetalle.CantidadPedidaDoble = 0;
                            OtablaDetalle.AlmacenCodigo = detalle.AlmacenCodigoDetalle;

                            OtablaDetalle.FlujodeCaja = detalle.FlujodeCaja;
                            OtablaDetalle.CantidadEntregadaDoble = 0;
                            OtablaDetalle.DespachoUnidadEquivalenteFlag = "N";
                            OtablaDetalle.PrecioModificadoFlag = "N";
                            OtablaDetalle.ClienteDireccionDespacho = c.ClienteDireccionDespacho;
                            OtablaDetalle.RutaDespacho = Otabla.RutaDespacho;
                            OtablaDetalle.Condicion = "0";
                            OtablaDetalle.PrecioUnitarioDoble = 0;
                            OtablaDetalle.PrecioNumeroRegistro = detalle.PrecioNumeroRegistro;
                            OtablaDetalle.PrecioUnitarioOriginal = detalle.PrecioUnitarioOriginal;
                            OtablaDetalle.PrecioUnitarioFleteOriginal = detalle.PrecioUnitarioFleteOriginal;
                            OtablaDetalle.TransferenciaGratuitaFlag = "N";
                            OtablaDetalle.PrecioUnitarioGratuito = 0;
                            //OtablaDetalle.TransferenciaGratuitaFlag = detalle.TransferenciaGratuitaFlag;
                            //if (detalle.TransferenciaGratuitaFlag == "S")
                            //{
                            //    detalle.TransferenciaBonificacionFlag = "S";
                            //}
                            //else
                            //{
                            //    detalle.TransferenciaBonificacionFlag = "N";
                            //}
                            OtablaDetalle.TransferenciaBonificacionFlag = detalle.TransferenciaBonificacionFlag;
                            //OtablaDetalle.PrecioUnitarioGratuito = detalle.PrecioUnitarioGratuito;
                            OtablaDetalle.DocumentoRelacTipoDocumento = detalle.DocumentoRelacTipoDocumento;
                            OtablaDetalle.TipoPromocion = detalle.TipoPromocion;
                            OtablaDetalle.DocumentoRelacNumeroDocumento = detalle.DocumentoRelacNumeroDocumento;
                            OtablaDetalle.DocumentoRelacLinea = detalle.DocumentoRelacLinea;
                            OtablaDetalle.PromocionNumero = detalle.PromocionNumero;
                            OtablaDetalle.PorcentajeDescuento01 = detalle.PorcentajeDescuento01;
                            //OtablaDetalle.PorcentajeDescuento02 = detalle.PorcentajeDescuento02;
                            //OtablaDetalle.PorcentajeDescuento03 = detalle.PorcentajeDescuento03;
                            OtablaDetalle.PorcentajeDescuento02 = 0;
                            OtablaDetalle.PorcentajeDescuento03 = 0;
                            OtablaDetalle.MargenPorcentaje = detalle.MargenPorcentaje;
                            OtablaDetalle.ComprometeFlag = "N";
                            OtablaDetalle.RutaDespachoFechaAsignacion = detalle.RutaDespachoFechaAsignacion;
                            OtablaDetalle.MontoDescuento = detalle.MontoDescuento;
                            OtablaDetalle.PesoUnitario = detalle.PesoUnitario;
                            OtablaDetalle.CodigoFlete = detalle.CodigoFlete;
                            OtablaDetalle.Promocion = detalle.Promocion;
                            OtablaDetalle.IndBonificacion = detalle.IndBonificacion;
                            OtablaDetalle.PrecioUnitarioFlete02 = 0;
                            OtablaDetalle.LineaCorte = 1;
                            OtablaDetalle.NroCortes = 0;
                            OtablaDetalle.GuiaSecuencia = 0;
                            OtablaDetalle.CantidadPedidaFinal = 1;
                            OtablaDetalle.NroCortesFinal = 0;
                            OtablaDetalle.MontoConvenio = detalle.MontoConvenio;
                            OtablaDetalle.PrecioUnitarioConvenio = 0;
                            OtablaDetalle.PrecioUnitarioConvenio = 0;



                            if (OtablaDetalle.TipoDetalle == "S")
                            {
                                OtablaDetalle.Lote = "00";
                                OtablaDetalle.PrecioModificadoFlag = "S";
                                OtablaDetalle.UnidadCodigo = "UND";
                            }
                            else
                            {
                                OtablaDetalle.Lote = UtilsDAO.getValuString("select Lote from WH_ItemAlmacenLote where Item='" + OtablaDetalle.ItemCodigo + "' and AlmacenCodigo='" + Otabla.AlmacenCodigo + "'", null);
                                OtablaDetalle.PrecioModificadoFlag = "N";
                                OtablaDetalle.UnidadCodigo = detalle.UnidadCodigo;
                            }


                            context.CO_DocumentoDetalle.Add(OtablaDetalle);
                            context.SaveChanges();

                            /// INSERTAR DESPACHODETALLE

                            //var secuenciaIddespacho = context.CO_DocumentoDetalleDespacho.Where(x => x.CompaniaSocio == c.CompaniaSocio && x.TipoDocumento == c.TipoDocumento && x.NumeroDocumento == c.NumeroDocumento).DefaultIfEmpty().Max(t => t == null ? 0 : t.Secuencia);

                            if (Otabla.Estado != "PR")
                            {
                                var OtablaDetalleDespacho = new CO_DocumentoDetalleDespacho();

                                OtablaDetalleDespacho.CompaniaSocio = c.CompaniaSocio;
                                OtablaDetalleDespacho.TipoDocumento = c.TipoDocumento;
                                OtablaDetalleDespacho.NumeroDocumento = c.NumeroDocumento;
                                OtablaDetalleDespacho.Linea = OtablaDetalle.Linea;
                                OtablaDetalleDespacho.Secuencia = 1;
                                OtablaDetalleDespacho.Turno = 1;
                                OtablaDetalleDespacho.Cantidad = detalle.CantidadPedida;
                                OtablaDetalleDespacho.AlmacenCodigo = detalle.AlmacenCodigoDetalle; ;
                                OtablaDetalleDespacho.AlmacenPrincipal = detalle.AlmacenCodigoDetalle; ;
                                OtablaDetalleDespacho.FechaEntrega = c.ComercialPedidoFechaRequerida;
                                OtablaDetalleDespacho.Estado = "PE";
                                OtablaDetalleDespacho.UltimaFechaModif = DateTime.Now;
                                OtablaDetalleDespacho.UltimoUsuario = c.UltimoUsuario;
                                OtablaDetalleDespacho.CantidadSOD = 0;
                                OtablaDetalleDespacho.CantidadMerma = 0;
                                OtablaDetalleDespacho.CantidadTotal = detalle.CantidadPedida;
                                OtablaDetalleDespacho.Item = detalle.ItemCodigo;
                                OtablaDetalleDespacho.AlmacenPrincipal = detalle.AlmacenCodigoDetalle; ;
                                OtablaDetalleDespacho.CantidadRecibida = 0;
                                OtablaDetalleDespacho.ClienteDireccionDespacho = c.ClienteDireccionDespacho;
                                OtablaDetalleDespacho.TipoRegistro = "P";
                                OtablaDetalleDespacho.ComprometeFlag = "N";
                                OtablaDetalleDespacho.FechaEntrega = Otabla.FechaDocumento;
                                context.CO_DocumentoDetalleDespacho.Add(OtablaDetalleDespacho);
                                context.SaveChanges();
                            }




                        }


                    }

                }

            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.CodigoError = 500;
                        error.MensajeError = ve.ErrorMessage;

                        response.lstErrores.Add(error);
                    }

                }

                return response;

            }
            return response;
        }


        private ModelTransac_CO_Documento AnularPedido(ModelTransac_CO_Documento c, BdEntityGenerico context)
        {

            ModelTransac_CO_Documento response = new ModelTransac_CO_Documento();
            ErrorObj error = new ErrorObj();


            try
            {

                var Otabla = context.CO_Documento.SingleOrDefault(t => t.CompaniaSocio == c.CompaniaSocio && t.TipoDocumento == c.TipoDocumento && t.NumeroDocumento == c.NumeroDocumento);
                if (Otabla != null)
                {

                    if (Otabla.Estado == "FA")
                    {

                        error.CodigoError = 500;
                        error.MensajeError = "No se puede anular el pedido, porque se encuentra facturado";
                        c.lstErrores.Add(error);

                        return c;
                    }

                    if (Otabla.Estado == "AP")
                    {
                        Otabla.Estado = "PR";
                        c.Estado = "PR";
                        context.Entry(Otabla).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return c;
                    }
                    if (Otabla.Estado == "PR")
                    {
                        Otabla.Estado = "AN";
                        c.Estado = "AN";
                        context.Entry(Otabla).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return c;
                    }

                }

            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.CodigoError = 500;
                        error.MensajeError = ve.ErrorMessage;

                        response.lstErrores.Add(error);
                    }

                }

                return response;

            }
            return response;
        }
    }
}