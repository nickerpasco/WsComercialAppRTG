using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient; 
using System.Linq; 
using System.Diagnostics;
using System.Data.Entity.Validation;
using WsComercialApp.Utils;
using WsComercialApp.Models;
using WsComercialApp.Models.Bd;

namespace WsComercialApp.Controllers
{
    public class UsuarioDao
    {

        public ModelUsuario GetUserLogin(string queryname, List<SqlParameter> parameters)
        {
            ModelUsuario resul = (ModelUsuario)UtilsDAO.getDataObjectByQueryWithParameters<ModelUsuario>(queryname, parameters);

            return resul;

        }

        public List<ModelSeguridadConcepto> getDataSeguridad(string queryname, List<SqlParameter> parameters)
        {
            var result = UtilsDAO.getDataByQueryWithParameters<ModelSeguridadConcepto>(queryname, parameters);

            return result;

        }
      

        public void LogginSessionControl (ModelUsuario usuario, String SesionFlag)
        {

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.UpdateSession"); 
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@SesionFlag", SesionFlag));
            parametros.Add(new SqlParameter("@Usuario", usuario.Usuario));
            UtilsDAO.ExecuteQueryCRUD(sqlString, parametros); 

        }

        internal List<ModelParametros> getParametrosSistema(ModelUsuario response)
        {
            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getParametrosSistema");

            List<SqlParameter> parametrosUserPass = new List<SqlParameter>();
            var company = response.CompaniaSocio.Substring(0, 6);
            parametrosUserPass.Add(new SqlParameter("@Persona", response.Persona));
            parametrosUserPass.Add(new SqlParameter("@Usuario", response.Usuario));
            parametrosUserPass.Add(new SqlParameter("@Compania", company));


            var result = UtilsDAO.getDataByQueryWithParameters<ModelParametros>(sqlString, parametrosUserPass);

            return result;
        }

        internal List<ModelMiscelaneos> getMiscelaneos(ModelUsuario response)
        {
            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getMiscelaneos");

            List<SqlParameter> parametrosUserPass = new List<SqlParameter>();
            parametrosUserPass.Add(new SqlParameter("@Usuario", response.Usuario));


            var result = UtilsDAO.getDataByQueryWithParameters<ModelMiscelaneos>(sqlString, parametrosUserPass);
            //var result = UtilsDAO.getDataByQuery<ModelMiscelaneos>(sqlString);

            return result;
        }


        public ModelTransacPersona InsertPersona(ModelTransacPersona persona)
        {
            ModelTransacPersona response = new ModelTransacPersona();
            ErrorObj error = new ErrorObj();
            using (var context = new BdEntityGenerico())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {


                        try
                        {
                             
                            var valida = context.PersonaMast.FirstOrDefault(b => b.Documento == persona.Documento);

                            if (valida != null)
                            {

                                error.CodigoError = 500;
                                error.MensajeError = "El Cliente con el Documento " + persona.Documento + " ya existe en la base de datos.."; 
                                response.lstErrores.Add(error);
                                return response;
                            }
                            else
                            {

                                #region Insertar



                                //var sqlString3 = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", " PersonaMast.getDataEmpleado");
                                //List<SqlParameter> parametros3 = new List<SqlParameter>();
                                //parametros3.Add(new SqlParameter("@Empleado", persona.Vendedor));
                                //ModelTransac_CO_Documento dataUsuario = (ModelTransac_CO_Documento)UtilsDAO.getDataObjectByQueryWithParameters<ModelTransac_CO_Documento>(sqlString3, parametros3);



                                var result = new PersonaMast();
                                var maxId = context.PersonaMast.DefaultIfEmpty().Max(t => t == null ? 0 : t.Persona);
                                result.Persona = maxId + 1;
                                var Origen = UtilsGlobal.ConvertLinesSqlXml("InitConfig", "UnidadReplicacion");
                                result.Origen = Origen;
                                persona.Origen = Origen;
                                result.Telefono = persona.Telefono;
                                result.Celular = persona.Celular;
                                result.ApellidoPaterno = persona.ApellidoPaterno.ToUpper();
                                result.ApellidoMaterno = persona.ApellidoMaterno.ToUpper();
                                result.Nombres = persona.Nombres.ToUpper();
                                if (persona.TipoPersona == "N")
                                {
                                    result.NombreCompleto = persona.ApellidoPaterno.ToUpper() + " " + persona.ApellidoMaterno.ToUpper() + ", " + persona.Nombres.ToUpper();
                                    result.Busqueda = persona.ApellidoPaterno.ToUpper() + " " + persona.ApellidoMaterno.ToUpper() + ", " + persona.Nombres.ToUpper();
                                    result.Documento = persona.Documento;
                                    result.DocumentoIdentidad = persona.Documento;
                                    
                                }
                                else
                                {
                                    result.NombreCompleto = persona.Nombres.ToUpper();
                                    result.Busqueda = persona.Nombres.ToUpper();
                                    result.DocumentoFiscal = persona.Documento;
                                    result.Documento = persona.Documento;

                                }
                                result.TipoDocumento = persona.TipoDocumentoPersona;                                
                                result.Direccion = persona.Direccion;
                                result.CorreoElectronico = persona.CorreoElectronico;
                                result.EsCliente = "S";
                                result.FlagConsultaRUC = "S";
                                result.SUNATDomiciliado = "S";
                                result.PersonaClasificacion = "S";
                                result.EsProveedor = "N";
                                result.EsEmpleado = "N";
                                result.EsOtro = "N";
                                result.TipoPersona = persona.TipoPersona;
                                result.FechaNacimiento = persona.FechaNacimiento;
                                result.Sexo = persona.Sexo;
                                result.Direccion = persona.Direccion;
                                result.CodigoPostal = persona.CodigoPostal;
                                result.Provincia = persona.Provincia;
                                result.Departamento = persona.Departamento;
                                result.EnfermedadGraveFlag = "N";
                                result.PYMEFlag = "N";
                                result.Estado = "A";
                                result.IngresoAplicacionCodigo = "CO";
                                result.UltimoUsuario = persona.UltimoUsuario;
                                result.IngresoUsuario = persona.UltimoUsuario;
                                result.UltimaFechaModif = DateTime.Now;
                                result.IngresoFechaRegistro = DateTime.Now;
                                //result.IngresoFechaRegistro = DateTime.Now;
                                //result.IngresoAplicacionCodigo = persona.IngresoAplicacionCodigo;
                                //result.IngresoUsuario = persona.IngresoUsuario;
                                //result.PYMEFlag = persona.PYMEFlag;
                                //result.PersonaClasificacion = persona.PersonaClasificacion;
                                //result.SUNATUbigeo = persona.SUNATUbigeo;
                                //result.SUNATNacionalidad = persona.SUNATNacionalidad;
                                result.EstadoCivil = persona.EstadoCivil;
                                context.PersonaMast.Add(result);
                                context.SaveChanges();



                                var OtablaCliente = new ClienteMast();

                                var company = persona.CompaniaUsuario.Substring(0, 6);

                                OtablaCliente.Cliente = result.Persona;
                                OtablaCliente.TipoCliente = persona.TipoCliente;
                                OtablaCliente.FechaRegistro = DateTime.Now;
                                OtablaCliente.CentroCosto = UtilsDAO.getValuString("select CO_VentaEquipo.centrocosto from CO_Vendedor inner join CO_VentaEquipo on CO_Vendedor.VentaEquipo=CO_VentaEquipo.VentaEquipo where CO_Vendedor.Vendedor="+persona.Vendedor+"", null); ;
                                OtablaCliente.RutaDespacho = UtilsDAO.getValuString("select RutaDespacho from CO_RutaDespachoUbigeo where Departamento='"+persona.Departamento+ "' and Provincia='" + persona.Provincia + "' and CodigoPostal='" + persona.CodigoPostal + "'", null); ;
                                OtablaCliente.Vendedor = persona.Vendedor;                    
                                OtablaCliente.TipoDocumento = UtilsDAO.getParametroString(FuncPrinc.trimValor("999999"), "DOCTIPODOC");
                                OtablaCliente.FormaFacturacion = UtilsDAO.getParametroString(FuncPrinc.trimValor(company), "DOCFORFACT");
                                OtablaCliente.TipoFacturacion = UtilsDAO.getParametroString(FuncPrinc.trimValor(company), "DOCTIPOFAC");
                                //OtablaCliente.TipoVenta = persona.TipoVenta;
                                OtablaCliente.TipoVenta = UtilsDAO.getValuString("select tipoventa from CO_Vendedor where Vendedor="+ persona.Vendedor + "", null);
                                OtablaCliente.FormadePago = UtilsDAO.getValuString("select MAX(FormadePago) from MA_FormadePago where CreditoFlag='N'",null);
                                OtablaCliente.ConceptoFacturacion = UtilsDAO.getParametroString(FuncPrinc.trimValor(company), "DOCCONCEPT");
                                OtablaCliente.FlagEsMaestro = "S";
                                OtablaCliente.BloquearVendedorPedidoFlag = "N";
                                OtablaCliente.AcumularPuntosFlag = "N";
                                OtablaCliente.BloquearVendedorPedidoFlag = "N";
                                OtablaCliente.FlagEsMaestro = "N";
                                OtablaCliente.BloquearPedidoFlag = "N";
                                OtablaCliente.Nacionalidad = "N";
                                OtablaCliente.PracticoTMCFlag = "T";
                                OtablaCliente.Clasificacion = "B";
                                OtablaCliente.PagoEfectivoFlag = "N";
                                OtablaCliente.SuspensionFlag = "N";
                                OtablaCliente.InfoCorpPorcentaje01 = 0;
                                OtablaCliente.InfoCorpPorcentaje02 = 0;
                                OtablaCliente.InfoCorpPorcentaje03 = 0;
                                OtablaCliente.InfoCorpPorcentaje04 = 0;
                                OtablaCliente.InfoCorpPorcentaje05 = 0;

                                OtablaCliente.Recepcion01Dia01Flag = "N";
                                OtablaCliente.Recepcion01Dia02Flag = "N";
                                OtablaCliente.Recepcion01Dia03Flag = "N";
                                OtablaCliente.Recepcion01Dia04Flag = "N";
                                OtablaCliente.Recepcion01Dia05Flag = "N";

                                OtablaCliente.Recepcion02Dia01Flag = "N";
                                OtablaCliente.Recepcion02Dia02Flag = "N";
                                OtablaCliente.Recepcion02Dia03Flag = "N";
                                OtablaCliente.Recepcion02Dia04Flag = "N";
                                OtablaCliente.Recepcion02Dia05Flag = "N";




                                OtablaCliente.Timestamp = BitConverter.GetBytes(DateTime.Now.Ticks);

                                if (OtablaCliente.CentroCosto == "")
                                {
                                    error.CodigoError = 500;  
                                    error.MensajeError = "No se encontró CentroCosto para el vendedor " + persona.Vendedor + " "; 
                                    response.lstErrores.Add(error);
                                    dbContextTransaction.Rollback();
                                    return response;
                                 }

                                if (OtablaCliente.TipoFacturacion == "")
                                {
                                    error.CodigoError = 500;
                                    error.MensajeError = "No se encontró TipoFacturacion para la Compania " + persona.CompaniaUsuario + " ";
                                    response.lstErrores.Add(error);
                                    dbContextTransaction.Rollback();
                                    return response;
                                }
                                if (OtablaCliente.ConceptoFacturacion == "")
                                {
                                    error.CodigoError = 500;
                                    error.MensajeError = "No se encontró ConceptoFacturacion para la Compania " + persona.CompaniaUsuario + " ";
                                    response.lstErrores.Add(error);
                                    dbContextTransaction.Rollback();
                                    return response;
                                }  
                                
                                if (OtablaCliente.FormaFacturacion == "")
                                {
                                    error.CodigoError = 500;
                                    error.MensajeError = "No se encontró FormaFacturacion para la Compania " + persona.CompaniaUsuario + " ";
                                    response.lstErrores.Add(error);
                                    dbContextTransaction.Rollback();
                                    return response;
                                }


                                context.ClienteMast.Add(OtablaCliente);
                                context.SaveChanges();


                                var OtablaClienteVendedor = new MA_ClienteVendedor();
                                OtablaClienteVendedor.Cliente = OtablaCliente.Cliente;
                                OtablaClienteVendedor.Secuencia =1;
                                OtablaClienteVendedor.Vendedor = OtablaCliente.Vendedor;
                                OtablaClienteVendedor.Linea = "$$";
                                OtablaClienteVendedor.Estado ="A";
                                context.MA_ClienteVendedor.Add(OtablaClienteVendedor);
                                context.SaveChanges();

                                //if (persona.lstDireccion != null)
                                //{
                                var secuenciaId = context.Database.SqlQuery<int>("select isnull(MAX(Secuencia),0) from Direccion with(nolock) where Persona= " + result.Persona + "").FirstOrDefault();
                                    var max1 = secuenciaId + 1;
                                    //foreach (var item in persona.lstDireccion)
                                    //{

                                        var OtablaDireccion = new Direccion();
                                        OtablaDireccion.Persona = result.Persona;
                                        OtablaDireccion.Secuencia = max1;
                                        OtablaDireccion.Direccion1 = result.Direccion;
                                        OtablaDireccion.EsCobranza = "S";
                                        OtablaDireccion.EsRemision = "S";
                                        OtablaDireccion.Departamento = persona.Departamento;
                                        OtablaDireccion.CodigoPostal = persona.CodigoPostal;
                                        OtablaDireccion.EsDomicilio = "N";
                                        OtablaDireccion.EsOtros = "N";
                                        OtablaDireccion.EsTrabajo = "N";
                                        OtablaDireccion.CondicionDireccion = "P";  
                                        OtablaDireccion.EsOtros = "N";
                                        OtablaDireccion.Pais = "PER";
                                        OtablaDireccion.Estado = result.Estado;
                                        OtablaDireccion.UltimaFechaModif = DateTime.Now;
                                        OtablaDireccion.Vendedor = persona.Vendedor;
                                        OtablaDireccion.UltimoUsuario = persona.UltimoUsuario;
                                        OtablaDireccion.Provincia = persona.Provincia;
                                        OtablaDireccion.RutaDespacho = OtablaCliente.RutaDespacho;
                                        context.Direccion.Add(OtablaDireccion);
                                        context.SaveChanges();

                                    //    max1++;

                                    //}
                                //}

                                #endregion

                                //return direccion actualizada 




                            }





                        }
                        catch (DbEntityValidationException e)
                        {
                            var st = new StackTrace(e, true);
                            // Get the top stack frame
                            var frame = st.GetFrame(0);
                            // Get the line number from the stack frame
                            var line = frame.GetFileLineNumber();

                            foreach (var eve in e.EntityValidationErrors)
                            {
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    error.CodigoError = 500;
                                    error.MensajeError = ve.ErrorMessage;
                                    error.LineaError = line;
                                    response.lstErrores.Add(error);
                                }
                            }

                            dbContextTransaction.Rollback();

                            return response;

                        }
                        dbContextTransaction.Commit();

                        //var sqlStringdirec = UtilsGlobal.ConvertLinesSqlXml("Query_PersonaMast", "PersonaMast.getDireccionByCLienteUpdate");
                        //List<SqlParameter> parametrosdire = new List<SqlParameter>();
                        //parametrosdire.Add(new SqlParameter("@Persona", persona.Persona));
                        //var resultadoDireccionesdir = UtilsDAO.getDataByQueryWithParameters<ModelTranscDireccion>(sqlStringdirec, parametrosdire);
                        //if (resultadoDireccionesdir.Count > 0)
                        //{

                        //    persona.lstDireccion = resultadoDireccionesdir;
                        //}


                    }
                    catch (Exception ex)
                    {
                        String MensajeError = "";

                        var st = new StackTrace(ex, true);
                        var frame = st.GetFrame(0);
                        var line = frame.GetFileLineNumber();


                        if (ex.InnerException != null)
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.InnerException.InnerException.Message;
                            error.LineaError = line;
                            MensajeError = ex.InnerException.InnerException.Message + " - ";
                        }
                        else
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.Message;
                            error.LineaError = line;
                            MensajeError += ex.Message + " - ";

                        }

                        response.lstErrores.Add(error); if (ex.InnerException != null)
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.InnerException.InnerException.Message;
                            MensajeError += ex.InnerException.InnerException.Message + " - ";
                        }
                        else
                        {
                            error.CodigoError = 500;
                            error.MensajeError = ex.Message;
                            error.LineaError = line;
                            MensajeError += ex.Message + " - ";
                        }

                        try
                        {
                            dbContextTransaction.Rollback();
                        }
                        catch (Exception w)
                        {
                            error.CodigoError = 500;
                            MensajeError += w.Message;
                            error.LineaError = line;
                            error.MensajeError = MensajeError;

                        }
                        response.lstErrores.Add(error);
                        return response;
                    }
                }
            }


            return persona;
        }
    }
}

