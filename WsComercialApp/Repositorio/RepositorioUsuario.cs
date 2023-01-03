using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
using WsComercialApp.Models;
using WsComercialApp.Utils;
using WsComercialApp.Security;
using System.Linq; 
using WsComercialApp.Models.Bd;

namespace WsComercialApp.Controllers
{
    public class RepositorioUsuario
    {

        public ModelUsuario GetAllByID(ModelUsuario bean)
        {
            ModelUsuario response = new ModelUsuario();
            ErrorObj error = new ErrorObj();


            if (bean == null)
            {
                response = new ModelUsuario();
                error.CodigoError = 500;
                error.MensajeError = "Verifique el objecto Request, es nulo";
                response.lstErrores.Add(error);
                return response;
            }

            try
            {

                bean = InitFuncClave(bean); // ENCRIPTAR CLAVE      

                UsuarioDao dao = new UsuarioDao();

                var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.GetAllParameters");

                List<SqlParameter> parametrosUserPass = new List<SqlParameter>();
                parametrosUserPass.Add(new SqlParameter("@UserName", bean.Usuario));


                var dataValidaUsuario = dao.GetUserLogin(sqlString, parametrosUserPass);

                if (dataValidaUsuario == null)
                {
                    response = new ModelUsuario();
                    error.CodigoError = 500;
                    error.MensajeError = "El Usuario : " + bean.Usuario + " no está registrado";
                    response.lstErrores.Add(error);
                    return response;
                }

                response = ValidarExistenciaUsuario(dataValidaUsuario, bean.Clave);


                if (response.lstErrores.Count > 0)
                {
                    return response;
                }

                ModelUsuario userpass = response;


                if (userpass != null)
                {


                    if (userpass.EstadoEmpleado == "I")
                    {
                        response = new ModelUsuario();
                        error.CodigoError = 500;
                        error.MensajeError = "La persona asociada al usuario : " + bean.Usuario + ", se encuentra inactivo.";
                        response.lstErrores.Add(error);
                        return response;
                    }


                    if (userpass.FechaCeseEmpleado != null)
                    {
                        response = new ModelUsuario();
                        error.CodigoError = 500;
                        error.MensajeError = "El empleado asociado al usuario : " + bean.Usuario + ", se encuentra Cesado desde : " + userpass.FechaCeseEmpleado;
                        response.lstErrores.Add(error);
                        return response;
                    }

                }
                else
                {
                    response = new ModelUsuario();
                    error.CodigoError = 500;
                    error.MensajeError = "Verifique el usuario en las siguentes tablas : EmpleadoMast,PersonaMast,FT_Vehiculo_Conductor,FT_Vehiculo_Detalle";
                    response.lstErrores.Add(error);
                    return response;
                }


                List<ModelParametros> parametrosSistema = dao.getParametrosSistema(response);
                List<ModelMiscelaneos> miscelaneosAll = dao.getMiscelaneos(response);

                var objttoken = TokenGenerator.GenerateTokenJwt(FuncPrinc.trimValor(response.Usuario));
                response.Token = objttoken.Token;
                response.TokenFechaExpiracion = objttoken.TokenFechaExpiracion;
                response.TokenFechaExpiracionString = objttoken.TokenFechaExpiracion.ToString("dd/MM/yyyy h:mm tt");
                response.lstparametros = parametrosSistema;
                response.lstmiscelaneos = miscelaneosAll;
                //response.lstSeguridad = dataSeguridad; 
                return response;

            }
            catch (Exception e)
            {

                error.CodigoError = 500;
                error.MensajeError = e.Message;
                response.lstErrores.Add(error);
                return response;
            }

            return response;

        }

        internal ReponseStoreLetras getValidarDiasPendientesFactura(Model_CO_Documento c)
        {

            //var sqlString4 = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "PersonaMast.getDataValidacionDocumentosVencidos");
            //List<SqlParameter> parametros4 = new List<SqlParameter>();
            //parametros4.Add(new SqlParameter("@ClienteNumero", c.ClienteNumero));
            //parametros4.Add(new SqlParameter("@CompaniaSocio", c.CompaniaSocio));

            var sqlString4 = "execute usp_co_consulta_Letras_pend '"+ c.CompaniaSocio + "', "+ c.ClienteNumero + "";

             
           ReponseStoreLetras obj2 = (ReponseStoreLetras)UtilsDAO.getDataByQuery<ReponseStoreLetras>(sqlString4).FirstOrDefault();

            return obj2;


        }

        internal List<CabeceraLineasCreditoDetalle> CabereceraLineaCreditoDetalle(ModelTransac_CO_Documento request)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Persona", request.ClienteNumero)); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getLineaCreditosCabeceraListaDetalle");

            var resultado = UtilsDAO.getDataByQueryWithParameters<CabeceraLineasCreditoDetalle>(sqlString, parametros);
            return resultado;
        }

        internal PaginacionGenerico CabereceraLineaCredito(FiltroGenerico request)
        {
            PaginacionGenerico response = new PaginacionGenerico();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Vendedor", Convert.ToInt32(request.BusquedaAvanzada)));
            parametros.Add(new SqlParameter("@Index", request.paginacion.page));
            parametros.Add(new SqlParameter("@PageSize", request.paginacion.limit));
            
            List<SqlParameter> parametrosCOunt = new List<SqlParameter>();
            parametrosCOunt.Add(new SqlParameter("@Vendedor", Convert.ToInt32(request.BusquedaAvanzada))); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getLineaCreditosCabeceraLista"); 
            var sqlStringCount = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getLineaCreditosCabeceraListaCount"); 

            var resultado = UtilsDAO.getDataByQueryWithParameters<CabeceraLineasCredito>(sqlString, parametros);


            var ResultadoCount = UtilsDAO.getValueInt(sqlStringCount, parametrosCOunt); 

            response.LStCabeceraLineasCredito = resultado;
            response.countBD = ResultadoCount;

            return response;

        }
        
        internal PaginacionGenerico DocumentosEmitidosByDay(FiltroGenerico request)
        {
            PaginacionGenerico response = new PaginacionGenerico();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Vendedor", Convert.ToInt32(request.BusquedaAvanzada)));
            parametros.Add(new SqlParameter("@FechaDocumento", request.FechaInicio));
            parametros.Add(new SqlParameter("@Index", request.paginacion.page));
            parametros.Add(new SqlParameter("@PageSize", request.paginacion.limit));
            
            List<SqlParameter> parametrosCOunt = new List<SqlParameter>();
            parametrosCOunt.Add(new SqlParameter("@Vendedor", Convert.ToInt32(request.BusquedaAvanzada)));
            parametrosCOunt.Add(new SqlParameter("@FechaDocumento", request.FechaInicio));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getCantidadDocumetosEmitidosFactura"); 
            var sqlStringCount = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getCantidadDocumetosEmitidosFacturaCount"); 

            var resultado = UtilsDAO.getDataByQueryWithParameters<CabeceraLineasCredito>(sqlString, parametros);
             
              
            var ResultadoCount = UtilsDAO.getValueInt(sqlStringCount, parametrosCOunt); 

            response.LStCabeceraLineasCredito = resultado;
            response.countBD = ResultadoCount;

            return response;

        }


        internal List<ModelTransac_CO_Documento> DocumentosEmitidosByDayDetalle(FiltroGenerico request)
        {
             
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Persona", Convert.ToInt32(request.Persona)));
            parametros.Add(new SqlParameter("@FechaDocumento", request.FechaInicio));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getCantidadDocumetosEmitidosFacturaDetalle");
            var resultado = UtilsDAO.getDataByQueryWithParameters<ModelTransac_CO_Documento>(sqlString, parametros);
             
             

            return resultado;

        } 
        
        internal List<ModelTransac_CO_Documento> ReporteCobranzasByPeriodoDetalle(FiltroGenerico request)
        {
             
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Cliente", Convert.ToInt32(request.Persona)));
            parametros.Add(new SqlParameter("@FechaCobranza", request.Periodo));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getCobranzasEmitidadasDetalle");
            var resultado = UtilsDAO.getDataByQueryWithParameters<ModelTransac_CO_Documento>(sqlString, parametros);
             
             

            return resultado;

        }


        internal PaginacionGenerico ReporteCobranzasByPeriodo(FiltroGenerico request)
        {
            PaginacionGenerico response = new PaginacionGenerico();
            List<SqlParameter> parametros = new List<SqlParameter>(); 

            parametros.Add(new SqlParameter("@Periodo", request.Periodo));
            parametros.Add(new SqlParameter("@Index", request.paginacion.page));
            parametros.Add(new SqlParameter("@PageSize", request.paginacion.limit));

            List<SqlParameter> parametrosCOunt = new List<SqlParameter>();
            parametrosCOunt.Add(new SqlParameter("@Periodo", Convert.ToInt32(request.Periodo))); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getCobranzasEmitidadas");
            var sqlStringCount = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getCobranzasEmitidadasCount");

            var resultado = UtilsDAO.getDataByQueryWithParameters<CabeceraLineasCredito>(sqlString, parametros);


            var ResultadoCount = UtilsDAO.getValueInt(sqlStringCount, parametrosCOunt);

            response.LStCabeceraLineasCredito = resultado;
            response.countBD = ResultadoCount;

            return response;

        }

        internal List<ModelTransac_CO_Documento> ReporteRutasDespachoDetalle(FiltroGenerico request)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Cliente", Convert.ToInt32(request.Persona)));
            parametros.Add(new SqlParameter("@FechaEntrega", request.Periodo));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getRutasDespachoDetalle");
            var resultado = UtilsDAO.getDataByQueryWithParameters<ModelTransac_CO_Documento>(sqlString, parametros);



            return resultado;

        }

        internal PaginacionGenerico ReporteRutasDespacho(FiltroGenerico request)
        {
            PaginacionGenerico response = new PaginacionGenerico();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Vendedor", Convert.ToInt32(request.BusquedaAvanzada)));
            parametros.Add(new SqlParameter("@FechaDocumento", request.FechaInicio));
            parametros.Add(new SqlParameter("@Index", request.paginacion.page));
            parametros.Add(new SqlParameter("@PageSize", request.paginacion.limit));

            List<SqlParameter> parametrosCOunt = new List<SqlParameter>();
            parametrosCOunt.Add(new SqlParameter("@Vendedor", Convert.ToInt32(request.BusquedaAvanzada)));
            parametrosCOunt.Add(new SqlParameter("@FechaDocumento", request.FechaInicio));


            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getRutasDespacho");
            var sqlStringCount = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getRutasDespachoCount");

            var resultado = UtilsDAO.getDataByQueryWithParameters<CabeceraLineasCredito>(sqlString, parametros);


            var ResultadoCount = UtilsDAO.getValueInt(sqlStringCount, parametrosCOunt);

            response.LStCabeceraLineasCredito = resultado;
            response.countBD = ResultadoCount;

            return response;

        }

        

        internal ReponseStoreLetras getDiasVencidoFacturas(Model_CO_Documento c)
        {

            //var sqlString4 = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "PersonaMast.getDataValidacionDocumentosVencidos");
            //List<SqlParameter> parametros4 = new List<SqlParameter>();
            //parametros4.Add(new SqlParameter("@ClienteNumero", c.ClienteNumero));
            //parametros4.Add(new SqlParameter("@CompaniaSocio", c.CompaniaSocio));

            var sqlString4 = "execute usp_co_consulta_clte_moroso '" + c.CompaniaSocio + "', "+ c.ClienteNumero + "";

             
           ReponseStoreLetras obj2 = (ReponseStoreLetras)UtilsDAO.getDataByQuery<ReponseStoreLetras>(sqlString4).FirstOrDefault();

            return obj2;


        }

        internal string getDocumentoSUNAT(ModelUsuario bean)
        {
            ServicioSUNAT.ServiceClient onClient = new ServicioSUNAT.ServiceClient();
            var respuesta = onClient.ConsutarRUC(bean.Documento);

            return respuesta;
        }

        internal List<DetalleFacturasLinea> getDetalleFacturasLinea(ModelUsuario bean)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CompaniaSocio", bean.CompaniaSocio));
            parametros.Add(new SqlParameter("@Persona", bean.Persona)); 

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getDetalleFacturasLinea");
            var resultado = UtilsDAO.getDataByQueryWithParameters<DetalleFacturasLinea>(sqlString, parametros);


            return resultado;
        }

        internal List<Model_MontosLineaCredito> getLineaCreditoClienteMonto(ModelUsuario bean)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CompaniaSocio", bean.CompaniaSocio));
            parametros.Add(new SqlParameter("@Persona", bean.Persona));
            parametros.Add(new SqlParameter("@TipoCambio", bean.TipoCambioValor));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getLineaCreditoMonto");
            var resultado = UtilsDAO.getDataByQueryWithParameters<Model_MontosLineaCredito>(sqlString, parametros);


            return resultado;
        }

        internal InfoCliente_LCredito getLineaCreditoCliente(ModelUsuario bean)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CompaniaSocio", bean.CompaniaSocio));
            parametros.Add(new SqlParameter("@Persona", bean.Persona));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getLineaCreditoCabecera");
            var resultado = UtilsDAO.getDataByQueryWithParameters<InfoCliente_LCredito>(sqlString, parametros);


            return resultado.FirstOrDefault();

        }

        public List<Model_Sy_Preferences> getPreferencias(ModelUsuario bean)
        {
            ModelUsuario response = new ModelUsuario();
            ErrorObj error = new ErrorObj();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Usuario", bean.Usuario));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getPreferenciasUsuario");
            var resultado = UtilsDAO.getDataByQueryWithParameters<Model_Sy_Preferences>(sqlString, parametros);


            return resultado;
        }

        public List<Model_Dashboard> getMontoMes(ModelUsuario bean)
        {
            ModelUsuario response = new ModelUsuario();
            ErrorObj error = new ErrorObj();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Vendedor", bean.Persona));

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getMontoDashBorad");
            var resultado = UtilsDAO.getDataByQueryWithParameters<Model_Dashboard>(sqlString, parametros);


            return resultado;
        }


        public ModelUsuario CambioClave(ModelUsuario bean)
        {
            ModelUsuario response = new ModelUsuario();
            ErrorObj error = new ErrorObj();
            try
            {
                using (var context = new BdEntityGenerico())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        var DataUsuario = context.Usuario.Where(x => x.Usuario1 == bean.Usuario).FirstOrDefault();
                        if (DataUsuario == null)
                        {
                            error.CodigoError = 500;
                            error.MensajeError = "El usuario : " + bean.Usuario + ", no existe en la base de datos..";
                            response.lstErrores.Add(error);
                            dbContextTransaction.Rollback();
                            return response;
                        }
                        else
                        {
                            if (bean.Clave != bean.ConfirmarClave)
                            {
                                error.CodigoError = 500;
                                error.MensajeError = "Las contraseñas ingresadas no coinciden...";
                                response.lstErrores.Add(error);
                                dbContextTransaction.Rollback();
                                return response;
                            }
                            DataUsuario.Clave = FuncPrinc.springEncriptar(bean.Clave);
                            context.Entry(DataUsuario).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            dbContextTransaction.Commit();

                        }
                    }
                }
            }
            catch (Exception e)
            {
                error.CodigoError = 500;
                error.MensajeError = e.Message;
                response.lstErrores.Add(error);
                return response;
            }
            return response;
        }

        internal List<ModelMiscelaneos> getMiscelaneos(ModelTransacPersona bean)
        {
            String sqlString = "";

            if (bean.FiltroUbigeo == "DEPARTAMENTOS")
            {
                sqlString = "select 'DEPARTAMENTOS' AS CodigoTabla, TRIM(Departamento) as CodigoElemento,RTRIM(DescripcionCorta) as DescripcionLocal  from Departamento";
            }

            if (bean.FiltroUbigeo == "PROVINCIAS")
            {
                sqlString = "select 'PROVINCIAS' AS CodigoTabla, TRIM(Departamento) as CodigoElemento,RTRIM(DescripcionCorta) as DescripcionLocal  ,RTRIM(Provincia)  AS ValorCadena , '' AS ValorCadena2  from Provincia where Departamento = '" + bean.Departamento + "'";
            }
            if (bean.FiltroUbigeo == "ZONASPOSTALES")
            {
                sqlString = "select 'ZONASPOSTALES' AS CodigoTabla, TRIM(Departamento) as CodigoElemento,RTRIM(DescripcionCorta) as DescripcionLocal  ,RTRIM(Provincia)  AS ValorCadena , RTRIM(CodigoPostal)  AS ValorCadena2 from ZonaPostal where Departamento = '" + bean.Departamento + "' and Provincia = '" + bean.Provincia + "'";
            }

            var resul = UtilsDAO.getDataByQuery<ModelMiscelaneos>(sqlString);

            return resul;

        }

        public ModelTransacPersona InsertPersona(ModelTransacPersona persona)
        {
            UsuarioDao dao = new UsuarioDao();
            return dao.InsertPersona(persona);
        }

        public PaginacionGenerico getPersonasOnline(FiltroGenerico bean)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            List<SqlParameter> parametroscount = new List<SqlParameter>();
            PaginacionGenerico p = new PaginacionGenerico();

            string sqlString = "";
            string sqlStringcount = "";

            if(bean.TipoSelector == "Personas" || bean.TipoSelector == null)
            {
                sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getPersonasOnnline");
                sqlStringcount = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getPersonasOnlineCount");

                parametros.Add(new SqlParameter("@Vendedor", bean.Vendedor)); 
                parametroscount.Add(new SqlParameter("@Vendedor", bean.Vendedor));
            }
            else if(bean.TipoSelector == "Vendedor")
            {
                sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getVendedorOnnline");
                sqlStringcount = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.getVendedorOnnlineCount");
            }


            //bean.paginacion.page = 1;
            //bean.paginacion.limit = 10;

            //var jsondetails = JsonConvert.SerializeObject(bean);

            parametros.Add(new SqlParameter("@Persona", bean.Persona));
            parametros.Add(new SqlParameter("@Busqueda", bean.BusquedaAvanzada));
            parametros.Add(new SqlParameter("@Index", bean.paginacion.page));
            parametros.Add(new SqlParameter("@PageSize", bean.paginacion.limit));

            parametroscount.Add(new SqlParameter("@Persona", bean.Persona));
            parametroscount.Add(new SqlParameter("@Busqueda", bean.BusquedaAvanzada));
            parametroscount.Add(new SqlParameter("@Index", bean.paginacion.page));
            parametroscount.Add(new SqlParameter("@PageSize", bean.paginacion.limit));

            var resul = UtilsDAO.getDataByQueryWithParameters<Model_PersonaMast>(sqlString, parametros);


            var Resultado = UtilsDAO.getValueInt(sqlStringcount, parametroscount);
            List<ModelDireccion> ListaDirecciones = new List<ModelDireccion>();

            if (bean.TipoSelector == "Personas")
            {
                if (resul.Count > 0)
                {
                    ListaDirecciones = getDireccionCliente(resul);
                }
                else
                {
                    
                }

                            
                 
            }
            else if (bean.TipoSelector == "Vendedor")
            {
                 
            } 

            p.countBD = Resultado;
            p.page = bean.paginacion.page;
            p.limit = bean.paginacion.limit;
            p.lstPersonas = resul;
            p.ListaDirecciones = ListaDirecciones;

            return p;
        }


        public List<ModelDireccion> getDireccionCliente(List<Model_PersonaMast> lstpersona)
        {
            List<ModelDireccion> lstdireccion = new List<ModelDireccion>();

            List<int> list = new List<int>();

            StringBuilder cuerpo = new StringBuilder();
            StringBuilder queryalmado = new StringBuilder();

            int countMax = lstpersona.Count();
            int vecesEjecucion = (countMax / 2);
            foreach (var item in lstpersona)
            {
                list.Add((int)item.Persona);
                cuerpo.Append(item.Persona + ",");
            }

            String Ver = cuerpo.ToString();
            int charcan = (Ver.Length - 1);
            String queryRealIn = Ver.Substring(0, charcan);
             
            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Personas.DireccionByClienteAll");
            //List<SqlParameter> parametros = new List<SqlParameter>(); 


            queryalmado.Append(sqlString +
                               " where Estado ='A' and Persona in(" + queryRealIn + ") ;");

            lstdireccion = UtilsDAO.getDataByQuery<ModelDireccion>(queryalmado.ToString());

            return lstdireccion;
        }

        private ModelUsuario ValidarExistenciaUsuario(ModelUsuario bean, String clavedigitada)
        {

            ModelUsuario response = new ModelUsuario();
            ErrorObj error = new ErrorObj();

            bean.Usuario = FuncPrinc.trimValor(bean.Usuario);

            if (bean == null)
            {
                response = new ModelUsuario();
                error.CodigoError = 500;
                error.MensajeError = "El Usuario : " + bean.Usuario + " no está registrado";
                response.lstErrores.Add(error);
                return response;
            }
            else
            {
                String claveDescripciptada = "" + FuncPrinc.trimValor(bean.Clave);// FuncPrinc.springDesencriptar(FuncPrinc.trimValor(response.Clave).ToUpper());
                String claveingresada = "" + FuncPrinc.trimValor(clavedigitada);// FuncPrinc.trimValor(bean.Clave).ToUpper();



                if (claveDescripciptada != claveingresada)
                {
                    error.CodigoError = 800;
                    error.MensajeError = "La contraseña ingresada, no coincide con el Usuario : " + bean.Usuario;
                    response.lstErrores.Add(error);
                    return response;
                }


                if (bean.Estado != "A")
                {


                    error.CodigoError = 800;
                    error.MensajeError = "El Usuario : " + bean.Usuario + " está inactivo";
                    response.lstErrores.Add(error);
                    return response;
                }


                if (bean.SesionFlag == "S")
                {
                    error.CodigoError = 1000;
                    error.MensajeError = "El Usuario : " + bean.Usuario + ", Ya se encuentra activo en otro dispositivo.";
                    response.lstErrores.Add(error);
                    //return response;
                }

            }

            return bean;
        }

        public ModelUsuario LoginEndSession(ModelUsuario bean)
        {



            UsuarioDao dao = new UsuarioDao();
            ErrorObj error = new ErrorObj();


            ModelUsuario response = new ModelUsuario();

            try
            {
                dao.LogginSessionControl(bean, "N");
            }
            catch (Exception e)
            {

                error.CodigoError = 500;
                error.MensajeError = e.Message;
                response.lstErrores.Add(error);
                return response;
            }

            return response;

        }  
        
        public Model_PersonaMast getDiasCreditoByTipo(Model_PersonaMast bean)
        {



            UsuarioDao dao = new UsuarioDao();
           

            return dao.getDataDiasCredito(bean);

        }






        public string getTokenString(Model_SpringUsuario usuario)
        {
            string token = "404";


            string apiUri = usuario.URL_LOGIN;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUri);
            httpWebRequest.Method = "PUT";
            httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Accept = "application/json";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(usuario);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_SpringUsuario>(streamReader.ReadToEnd());
                token = response.token;
                // return response; // do something with the response...
            }

            return token;
        }


        public ModelUsuario InitFuncClave(ModelUsuario data)
        {
            ModelUsuario returndata = new ModelUsuario();
            String claveenciptada = FuncPrinc.springEncriptar(data.Clave);
            //String descriptar = FuncPrinc.springDesencriptar(data.Clave);

            returndata.Usuario = data.Usuario;
            returndata.Clave = claveenciptada;

            return returndata;
        }
    }
}

