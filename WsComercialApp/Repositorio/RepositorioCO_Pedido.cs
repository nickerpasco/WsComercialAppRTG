using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using WsComercialApp.Dao;
using WsComercialApp.Models;
using WsComercialApp.Utils;

namespace WsComercialApp.Repositorio
{
    public class RepositorioCO_Pedido
    {

        public PaginacionGenerico getCabeceraCodoc(FiltroGenerico bean)
        {
            var p = new PaginacionGenerico();



            String sqlString = "";

            if (bean.TipoDocumento == "PE")
            {
                sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.Cabecera");
            }
            else
            {
                sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.CabeceraComprobante");
            }

       

            var sqlStringcount = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.CabeceraCount");
            

            String queryArmado = sqlString + RetornoQuery(bean);
            String queryArmadoCount = sqlStringcount + RetornoQueryCount(bean);


            var resul = UtilsDAO.getDataByQuery<Model_CO_Documento>(queryArmado);
            var Resultado = UtilsDAO.getValueIntOnly(queryArmadoCount);

             
            p.countBD = Resultado;
            p.page = bean.paginacion.page;
            p.limit = bean.paginacion.limit;
            p.lstCabeceraPedidos = resul;




            return p;

        }

         public PaginacionGenerico getFacturasLetras(FiltroGenerico bean)
        {
            var p = new PaginacionGenerico();



            String sqlString = "";

            sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.Cabecera");

            var sqlStringcount = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.CabeceraCount");

            bean.Periodo = DateTime.Now.ToString("yyyyMM", CultureInfo.InvariantCulture);

            String queryArmado = sqlString + RetornoQueryFactura(bean);
            String queryArmadoCount = sqlStringcount + RetornoQueryFacturaCount(bean);


            var resul = UtilsDAO.getDataByQuery<Model_CO_Documento>(queryArmado);
            var Resultado = UtilsDAO.getValueIntOnly(queryArmadoCount);

             
            p.countBD = Resultado;
            p.page = bean.paginacion.page;
            p.limit = bean.paginacion.limit;
            p.lstCabeceraPedidos = resul;




            return p;

        }

        public List<ModelMiscelaneos> getTipoCambio()
        {
            var sqlquery = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Miscelaneos.getTipoCambios");
            var ls = UtilsDAO.getDataByQuery<ModelMiscelaneos>(sqlquery);
            return ls;
        }

        internal List<Model_CO_DocumentoDetalle> getDataDetalle(FiltroGenerico bean)
        {

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.Detalle");
            String queryArmado = sqlString + " where documento.CompaniaSocio = '"+ bean .CompaniaSocio+ "' and documento.TipoDocumento = '" + bean.TipoDocumento + "' and documento.NumeroDocumento = '" + bean.NumeroDocumento + "'";


            var resul = UtilsDAO.getDataByQuery<Model_CO_DocumentoDetalle>(queryArmado);


            return resul;

          
        }

        private string RetornoQueryCount(FiltroGenerico bean)
        {

            if (bean.TipoDocumento == "PE")
            {
                String returnString = " where pedido.Vendedor = " + bean.Vendedor + " and  pedido.CompaniaSocio = '" + bean.CompaniaSocio + "' " + " and  pedido.TipoDocumento = '" + bean.TipoDocumento + "' " +
         " and ('" + bean.FechaInicio + "' IS NULL OR pedido.FechaDocumento >= '" + bean.FechaInicio + "') " +
         " AND('" + bean.FechaFin + "' IS NULL OR pedido.FechaDocumento < DATEADD(DAY, 1, '" + bean.FechaFin + "')) " +
           " AND ('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' ='' or UPPER(pedido.ClienteNombre+pedido.NumeroDocumento)like '%'+upper('" + bean.BusquedaAvanzada + "' )+'%') " +
         "  AND ('" + bean.Estado + "' is null or '" + bean.Estado + "' ='' or UPPER(pedido.Estado)like '%'+upper('" + bean.Estado + "' )+'%') ";

                return returnString;
            }
            else
            {
                String returnString = " where pedido.Vendedor = " + bean.Vendedor + " and  pedido.CompaniaSocio = '" + bean.CompaniaSocio + "' " + " and  CO_TipoDocumento.Clasificacion='DC' " +
         " and ('" + bean.FechaInicio + "' IS NULL OR pedido.FechaDocumento >= '" + bean.FechaInicio + "') " +
         " AND('" + bean.FechaFin + "' IS NULL OR pedido.FechaDocumento < DATEADD(DAY, 1, '" + bean.FechaFin + "')) " +
           " AND ('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' ='' or UPPER(pedido.ClienteNombre+pedido.NumeroDocumento)like '%'+upper('" + bean.BusquedaAvanzada + "' )+'%') " +
         "  AND ('" + bean.Estado + "' is null or '" + bean.Estado + "' ='' or UPPER(pedido.Estado)like '%'+upper('" + bean.Estado + "' )+'%') ";

                return returnString;
            }

               
        }


        internal ModelTransac_CO_Pedido ExportarComprobantePdf(ModelTransac_CO_Documento request)
        {

            ModelTransac_CO_Pedido error = new ModelTransac_CO_Pedido();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CompaniaSocio", request.CompaniaSocio));
            parametros.Add(new SqlParameter("@TipoDocumento", request.TipoDocumento));
            parametros.Add(new SqlParameter("@NumeroDocumento", request.NumeroDocumento));


            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getPdfComprobanteDocumento");
            var resultado = UtilsDAO.getDataByQueryWithParameters<Model_ComprobantePDF>(sqlString, parametros);



            if (resultado.Count == 0)
            {
                error.objerror.CodigoError = 500;
                error.objerror.MensajeError = "No se encotraron datos";
                return error;
            }
            else
            {


              
                try
                {

                    ReportDocument rd = new ReportDocument();

                    rd.Load(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Reportes"), "rpt_ComprobanteVenta.rpt"));
                    rd.SetDataSource(resultado);
                    rd.SetParameterValue("imgFirma", request.CodigoQR);

                    System.Web.HttpContext.Current.Response.Buffer = false;
                    System.Web.HttpContext.Current.Response.ClearContent();
                    System.Web.HttpContext.Current.Response.ClearHeaders();
                    String BASE = null;



                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    var test = ReadFully(stream);
                    String file = Convert.ToBase64String(test);
                    BASE = file;


                    if (request.Plataforma == "UWP")
                    {

                        String pdfPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/FilesPdfExport"));
                        String rutita = pdfPath + @"\" + request.NumeroDocumento.Trim() + ".pdf";

                        if (test != null)
                        {
                           
                            BorrarFile(rutita);

                            System.IO.FileStream pdfFile = new System.IO.FileStream(pdfPath + @"\" + request.NumeroDocumento.Trim() + ".pdf", System.IO.FileMode.Create);
                            pdfFile.Write(test, 0, test.Length);
                            pdfFile.Close();
                        }

                        error.objerror.CodigoError = 200;
                        error.objerror.MensajeError = rutita;
                        error.BASE64Certificado = BASE;
                        error.objerror.ValorByte = test;
                    }
                    else
                    {
                        error.objerror.CodigoError = 200;
                        error.objerror.MensajeError = BASE;
                        error.BASE64Certificado = BASE;
                        error.objerror.ValorByte = test;
                    }


                    return error;
                }
                catch (Exception e)
                {

                    var st = new StackTrace(e, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();

                    error.objerror.CodigoError = 500;
                    error.objerror.MensajeError = "ERROR_GENRACION_rEPORTE   : " + e.Message  +  "   MÁS DATA :  " + e.StackTrace + "     MENSAJE EXACTO : " +e.InnerException;
                    error.objerror.ValorDevolucion = "";
                    error.objerror.LineaError = line;




                    return error;
                }


            }

        }

        internal List<ModelItemAlmacen> getItemAlmacen(FiltroGenerico documento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();  
            parametros.Add(new SqlParameter("@ItemCodigo", documento.BusquedaAvanzada)); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getItemAlmacen"); 
            var resultado = UtilsDAO.getDataByQueryWithParameters<ModelItemAlmacen>(sqlString, parametros);

            return resultado;
        }

        internal PaginacionGenerico getAgencias(FiltroGenerico request)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            List<SqlParameter> parametroscount = new List<SqlParameter>();

            PaginacionGenerico p = new PaginacionGenerico();

            parametros.Add(new SqlParameter("@Busqueda", request.BusquedaAvanzada));
            parametros.Add(new SqlParameter("@Index", request.paginacion.page));
            parametros.Add(new SqlParameter("@PageSize", request.paginacion.limit)); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getAgencias");
            var sqlStringcount = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getAgenciasCount");
            var resultado = UtilsDAO.getDataByQueryWithParameters<AgenciasModel>(sqlString, parametros);

            parametroscount.Add(new SqlParameter("@Busqueda", request.BusquedaAvanzada));
            parametroscount.Add(new SqlParameter("@Index", request.paginacion.page));
            parametroscount.Add(new SqlParameter("@PageSize", request.paginacion.limit));            


            var Resultado = UtilsDAO.getValueInt(sqlStringcount, parametroscount);
             
            
            p.countBD = Resultado;
            p.page = request.paginacion.page;
            p.limit = request.paginacion.limit;
            p.lstAgencias = resultado; 

            return p;
             

        }  
        internal PaginacionGenerico getLetrasCabecera(FiltroGenerico request)
        {
           

            PaginacionGenerico p = new PaginacionGenerico(); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getLetrasCabecera");
            var sqlStringcount = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getLetrasCabeceraCount");


            String queryArmado = sqlString + RetornoQueryLetras(request);
            String queryArmadoCount = sqlStringcount + RetornoQueryLetrasCount(request); 

            var resultado = UtilsDAO.getDataByQuery<Model_LetrasCabecera>(queryArmado);
            var Resultado = UtilsDAO.getValueIntOnly(queryArmadoCount);



            p.countBD = Resultado;
            p.page = request.paginacion.page;
            p.limit = request.paginacion.limit;
            p.lstLetrasCabecera = resultado; 

            return p;
             

        } 
        
        internal List<Model_CO_DocumentoDetalle> getComentariosDetalleLetras(Model_CO_Documento request)
        {
           

            PaginacionGenerico p = new PaginacionGenerico(); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getLetrasCabecera");
            var sqlStringcount = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getLetrasCabeceraCount");


            String queryArmado = " select CompaniaSocio,CONVERT(varchar, OperacionCanjeNumero) as NumeroDocumento , Secuencia as Linea , Comentario from CO_OperacionCanjeComentario "+
                                 " where CompaniaSocio = '"+ request .CompaniaSocio+ "' and CONVERT(varchar, OperacionCanjeNumero) = '"+ request.NumeroDocumento+ "'";
            var resultado = UtilsDAO.getDataByQuery<Model_CO_DocumentoDetalle>(queryArmado); 


             

            return resultado;
             

        }

        public void BorrarFile(String rutita)
        {
            if (System.IO.File.Exists(rutita))
            {
                while (System.IO.File.Exists(rutita))
                {
                    //Si existe borramos el archivo d
                    System.IO.File.Delete(rutita);
                }
            }
        }

        internal ModelTransac_CO_Pedido ExportarPdf(ModelTransac_CO_Documento request)
        {

            ModelTransac_CO_Pedido error = new ModelTransac_CO_Pedido();
            List<SqlParameter> parametros = new List<SqlParameter>(); 
            parametros.Add(new SqlParameter("@CompaniaSocio", request.CompaniaSocio));
            parametros.Add(new SqlParameter("@TipoDocumento", request.TipoDocumento));
            parametros.Add(new SqlParameter("@NumeroDocumento", request.NumeroDocumento));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getReporte");
            var resultado = UtilsDAO.getDataByQueryWithParameters<ReportePedidoObj>(sqlString, parametros);



            if (resultado.Count == 0)
            {
                error.objerror.CodigoError = 500;
                error.objerror.MensajeError = "No se encotraron datos";
                return error;
            }
            else
            {


                ReportDocument rd = new ReportDocument();               

                rd.Load(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Reportes"), "rpt_Pedido.rpt"));
                rd.SetDataSource(resultado); 

                System.Web.HttpContext.Current.Response.Buffer = false;
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                String BASE = null;

                try
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    var test = ReadFully(stream);
                    String file = Convert.ToBase64String(test);
                    BASE = file;

                    if (request.Plataforma == "UWP")
                    {

                        String pdfPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/FilesPdfExport"));
                        String rutita = pdfPath + @"\" + request.NumeroDocumento.Trim() + ".pdf";

                        if (test != null)
                        {

                            BorrarFile(rutita);

                            System.IO.FileStream pdfFile = new System.IO.FileStream(pdfPath + @"\" + request.NumeroDocumento.Trim() + ".pdf", System.IO.FileMode.Create);
                            pdfFile.Write(test, 0, test.Length);
                            pdfFile.Close();
                        }

                        error.objerror.CodigoError = 200;
                        error.objerror.MensajeError = rutita;
                        error.BASE64Certificado = BASE;
                        error.objerror.ValorByte = test;
                    }
                    else
                    {
                        error.objerror.CodigoError = 200;
                        error.objerror.MensajeError = BASE;
                        error.BASE64Certificado = BASE;
                        error.objerror.ValorByte = test;
                    }

                    //error.objerror.CodigoError = 200;
                    //error.objerror.MensajeError = BASE;
                    //error.objerror.ValorByte = test;
                    return error;
                }
                catch (Exception e)
                {

                    var st = new StackTrace(e, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();

                    error.objerror.CodigoError = 500;
                    error.objerror.MensajeError = "ERROR_GENRACION_rEPORTE   : " + e.Message;
                    error.objerror.ValorDevolucion = BASE;
                    error.objerror.LineaError = line;

 


                    return error;
                }


            }

        }   
        
        internal List<DescuentoModel> getDescuentosList(ModelTransac_CO_Documento request)
        {

            ModelTransac_CO_Pedido error = new ModelTransac_CO_Pedido();
            List<SqlParameter> parametros = new List<SqlParameter>();

            request.Sucursal = UtilsDAO.getValuString("select sucursal from WH_AlmacenMast with(nolock) where AlmacenCodigo='"+request.AlmacenItemDescuento+"'", null);
            request.EquipoVenta = UtilsDAO.getValuString("select VentaEquipo from CO_Vendedor with(nolock) where vendedor="+request.Vendedor+"", null);
          

            parametros.Add(new SqlParameter("@CompaniaSocio", request.CompaniaSocio));
            parametros.Add(new SqlParameter("@TipoCliente", request.TipoCliente));
            parametros.Add(new SqlParameter("@NumeroCLiente", request.ClienteNumero));
            parametros.Add(new SqlParameter("@TipoRecojo", request.RecojoFlag));
            parametros.Add(new SqlParameter("@ItemCodigo", request.ItemDescuento));
            parametros.Add(new SqlParameter("@AlmacenCodigo", request.AlmacenItemDescuento));
            parametros.Add(new SqlParameter("@Monto", request.MontoTotal * request.CantidadItemDescuento  ));
            parametros.Add(new SqlParameter("@Cantidad", request.CantidadItemDescuento));
            parametros.Add(new SqlParameter("@Sucursal", request.Sucursal));
            parametros.Add(new SqlParameter("@VentaEquipo", request.EquipoVenta));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getDescuentosItem");
            var resultado = UtilsDAO.getDataByQueryWithParameters<DescuentoModel>(sqlString, parametros);



            return resultado;
             

        }
         internal List<DescuentoModel> getDescuentosReglas(ModelTransac_CO_Documento request)
        {

            ModelTransac_CO_Pedido error = new ModelTransac_CO_Pedido();
            List<SqlParameter> parametros = new List<SqlParameter>();


            request.Sucursal = UtilsDAO.getValuString("select sucursal from WH_AlmacenMast with(nolock) where AlmacenCodigo='" + request.AlmacenItemDescuento + "'", null);
            request.EquipoVenta = UtilsDAO.getValuString("select VentaEquipo from CO_Vendedor with(nolock) where vendedor=" + request.Vendedor + "", null);


            parametros.Add(new SqlParameter("@Compania", request.CompaniaSocio));
            parametros.Add(new SqlParameter("@TipoCliente", request.TipoCliente));
            parametros.Add(new SqlParameter("@Cliente", request.ClienteNumero));
            parametros.Add(new SqlParameter("@VentaEquipo", request.EquipoVenta));
            parametros.Add(new SqlParameter("@Sucursal", request.Sucursal));
            parametros.Add(new SqlParameter("@Recojo", request.RecojoFlag));
            parametros.Add(new SqlParameter("@Item", request.ItemDescuento));
            parametros.Add(new SqlParameter("@Almacen", request.AlmacenItemDescuento)); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_CO_Pedido", "Co_Documento.getDescuentosItemRegla");
            var resultado = UtilsDAO.getDataByQueryWithParameters<DescuentoModel>(sqlString, parametros);



            return resultado;
             

        }

        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private string RetornoQuery(FiltroGenerico bean)
        {

            if (bean.TipoDocumento == "PE")
            {
                String returnString = " where pedido.Vendedor = " + bean.Vendedor + " and  pedido.CompaniaSocio = '" + bean.CompaniaSocio + "' " + " and  pedido.TipoDocumento = '" + bean.TipoDocumento + "' " +
      " and ('" + bean.FechaInicio + "' IS NULL OR pedido.FechaDocumento >= '" + bean.FechaInicio + "') " +
      " AND('" + bean.FechaFin + "' IS NULL OR pedido.FechaDocumento < DATEADD(DAY, 1, '" + bean.FechaFin + "')) " +
       " AND ('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' ='' or UPPER(pedido.ClienteNombre+pedido.NumeroDocumento)like '%'+upper('" + bean.BusquedaAvanzada + "' )+'%') " +
        "  AND ('" + bean.Estado + "' is null or '" + bean.Estado + "' ='' or UPPER(pedido.Estado)like '%'+upper('" + bean.Estado + "' )+'%') " +
      " order by  NumeroDocumento desc " +
      " OFFSET(" + bean.paginacion.page + " - 1) * " + bean.paginacion.limit + "  ROWS " +
      " FETCH NEXT " + bean.paginacion.limit + " ROWS ONLY";


                return returnString;
            }
            else
            {
                String returnString = " where pedido.Vendedor = " + bean.Vendedor + " and  pedido.CompaniaSocio = '" + bean.CompaniaSocio + "' " + " and  CO_TipoDocumento.Clasificacion='DC' " +
      " and ('" + bean.FechaInicio + "' IS NULL OR pedido.FechaDocumento >= '" + bean.FechaInicio + "') " +
      " AND('" + bean.FechaFin + "' IS NULL OR pedido.FechaDocumento < DATEADD(DAY, 1, '" + bean.FechaFin + "')) " +
       " AND ('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' ='' or UPPER(pedido.ClienteNombre+pedido.NumeroDocumento)like '%'+upper('" + bean.BusquedaAvanzada + "' )+'%') " +
        "  AND ('" + bean.Estado + "' is null or '" + bean.Estado + "' ='' or UPPER(pedido.Estado)like '%'+upper('" + bean.Estado + "' )+'%') " +
      " order by  NumeroDocumento desc " +
      " OFFSET(" + bean.paginacion.page + " - 1) * " + bean.paginacion.limit + "  ROWS " +
      " FETCH NEXT " + bean.paginacion.limit + " ROWS ONLY";


                return returnString;
            }

         
        }



        private string RetornoQueryFactura(FiltroGenerico bean)
        {
            bean.Persona = bean.Vendedor;
            String returnString = "  WHERE(pedido.Estado = 'PR' and pedido.ClienteNumero = "+bean.Persona+ " AND pedido.MonedaDocumento = '"+bean.MonedaDocumento+"'  AND CO_TipoDocumento.Clasificacion <> 'PE' AND CO_TipoDocumento.Clasificacion <> 'AD' " +

        "  AND formapago.CuotaCreditoFlag = 'S') Or(pedido.TipoDocumento = 'NC' and pedido.ClienteNumero = " + bean.Persona + "  and pedido.Estado = 'PR')" +

       "  and pedido.VoucherPeriodo = '"+bean.Periodo+"'" +

       " AND('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' = '' or UPPER(pedido.ClienteNombre + pedido.NumeroDocumento)like '%' + upper('" + bean.BusquedaAvanzada + "') + '%')" +

      "      and('" + bean.FechaInicio + "' IS NULL OR pedido.FechaDocumento >= '" + bean.FechaInicio + "')" +
      "  AND('" + bean.FechaFin + "' IS NULL OR pedido.FechaDocumento < DATEADD(DAY, 1, '" + bean.FechaFin + "'))" +


     " order by  pedido.FechaDocumento desc " +
     " OFFSET(" + bean.paginacion.page + " - 1) * " + bean.paginacion.limit + "  ROWS " +
     " FETCH NEXT " + bean.paginacion.limit + " ROWS ONLY";


            return returnString;


        }

        private string RetornoQueryFacturaCount(FiltroGenerico bean)
        {
            bean.Persona = bean.Vendedor;

            String returnString = "  WHERE(pedido.Estado = 'PR' and pedido.ClienteNumero = " + bean.Persona + " AND pedido.MonedaDocumento = '" + bean.MonedaDocumento + "' AND CO_TipoDocumento.Clasificacion <> 'PE' AND CO_TipoDocumento.Clasificacion <> 'AD' " +

    "  AND formapago.CuotaCreditoFlag = 'S') Or(pedido.TipoDocumento = 'NC' and pedido.ClienteNumero = " + bean.Persona + "  and pedido.Estado = 'PR')" +

   "  and pedido.VoucherPeriodo = '" + bean.Periodo + "'" +

   " AND('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' = '' or UPPER(pedido.ClienteNombre + pedido.NumeroDocumento)like '%' + upper('" + bean.BusquedaAvanzada + "') + '%')" +

  "      and('" + bean.FechaInicio + "' IS NULL OR pedido.FechaDocumento >= '" + bean.FechaInicio + "')" +
  "  AND('" + bean.FechaFin + "' IS NULL OR pedido.FechaDocumento < DATEADD(DAY, 1, '" + bean.FechaFin + "'))";

            return returnString;

        }

        private string RetornoQueryLetras(FiltroGenerico bean)
        {
          String query = " where CO_OperacionCanje.Vendedor = " + bean.Vendedor + " and  CO_OperacionCanje.CompaniaSocio = '" + bean.CompaniaSocio + "'  " +
      " and ('" + bean.FechaInicio + "' IS NULL OR CO_OperacionCanje.FechaPreparacion >= '" + bean.FechaInicio + "') " +
      " AND('" + bean.FechaFin + "' IS NULL OR CO_OperacionCanje.FechaPreparacion < DATEADD(DAY, 1, '" + bean.FechaFin + "')) " +
       " AND ('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' ='' or UPPER(Cliente.Busqueda+convert(varchar,CO_OperacionCanje.OperacionCanjeNumero))like '%'+upper('" + bean.BusquedaAvanzada + "' )+'%') " +
        " AND ('" + bean.Estado + "' is null or '" + bean.Estado + "' ='' or UPPER(CO_OperacionCanje.Estado)like '%'+upper('" + bean.Estado + "' )+'%') " +
      " order by  CO_OperacionCanje.OperacionCanjeNumero desc " +
      " OFFSET(" + bean.paginacion.page + " - 1) * " + bean.paginacion.limit + "  ROWS " +
      " FETCH NEXT " + bean.paginacion.limit + " ROWS ONLY";

            return query;


        }

        private string RetornoQueryLetrasCount(FiltroGenerico bean)
        {
            String query = " where CO_OperacionCanje.Vendedor = " + bean.Vendedor + " and  CO_OperacionCanje.CompaniaSocio = '" + bean.CompaniaSocio + "'  " +
        " and ('" + bean.FechaInicio + "' IS NULL OR CO_OperacionCanje.FechaPreparacion >= '" + bean.FechaInicio + "') " +
        " AND('" + bean.FechaFin + "' IS NULL OR CO_OperacionCanje.FechaPreparacion < DATEADD(DAY, 1, '" + bean.FechaFin + "')) " +
         " AND ('" + bean.BusquedaAvanzada + "' is null or '" + bean.BusquedaAvanzada + "' ='' or UPPER(Cliente.Busqueda+convert(varchar,CO_OperacionCanje.OperacionCanjeNumero))like '%'+upper('" + bean.BusquedaAvanzada + "' )+'%') " +
          " AND ('" + bean.Estado + "' is null or '" + bean.Estado + "' ='' or UPPER(CO_OperacionCanje.Estado)like '%'+upper('" + bean.Estado + "' )+'%') ";

            return query;


        }

        public ModelTransac_CO_Documento InsertCo_Documento(ModelTransac_CO_Documento c)
        {
            Co_DocumentoDao dao = new Co_DocumentoDao();
            return dao.InsertCo_Documento(c);
        }  
        
        public ModelTransac_CO_Documento SaveLetras(ModelTransac_CO_Documento c)
        {
            Co_DocumentoDao dao = new Co_DocumentoDao();
            return dao.SaveLetras(c);
        }
        
        public ModelTransac_CO_Documento SaveFacturacion(ModelTransac_CO_Documento c)
        {
            Co_DocumentoDao dao = new Co_DocumentoDao();
            return dao.SaveFacturacion(c);
        }
        
        public ModelTransac_CO_Documento AnularPedido(ModelTransac_CO_Documento c)
        {
            Co_DocumentoDao dao = new Co_DocumentoDao();
            return dao.AnularPedido(c);
        }
    }
}