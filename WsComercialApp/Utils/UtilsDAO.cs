using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WsComercialApp.Models.Bd; 

namespace WsComercialApp.Utils
{
    public class UtilsDAO
    {

        public static List<T> getDataByQuery<T>(string sqlquery)
        {
            List<T> lstLinea = new List<T>();
            using (var ctx = new BdEntityGenerico())
            {
                lstLinea = ctx.Database.SqlQuery<T>(sqlquery).ToList();
            }
            return lstLinea;
        }
        public static List<T> getDataByQueryWithParameters<T>(string sqlquery, List<SqlParameter> parameters)
        {
            List<T> lstLinea = new List<T>();
            using (var ctx = new BdEntityGenerico())
            {

                lstLinea = ctx.Database.SqlQuery<T>(sqlquery, parameters.ToArray()).ToList();
            }
            return lstLinea;
        }



        public static object getDataObjectByQueryWithParameters<T>(string sqlquery, List<SqlParameter> parameters)
        {

            using (var ctx = new BdEntityGenerico())
            {
                return ctx.Database.SqlQuery<T>(sqlquery, parameters.ToArray()).FirstOrDefault();
            }

        }


        public static int ExecuteQueryCRUD(string nameQuery, List<SqlParameter> parameters)
        {
            var valueReturn = 0;
            using (BdEntityGenerico context = new BdEntityGenerico())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    {
                        try
                        {
                            valueReturn = context.Database.ExecuteSqlCommand(nameQuery, parameters.ToArray());
                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            return valueReturn;
        }
        
        public static int ExecuteQueryCRUD2(string nameQuery, List<SqlParameter> parameters)
        {
            var valueReturn = 0;
            using (BdEntityGenerico context = new BdEntityGenerico())
            {
                try
                {
                    //valueReturn = context.Database.SqlQuery(nameQuery, parameters.ToArray());
                    var data = context.Database.SqlQuery<int>(nameQuery, parameters.ToArray());
                    valueReturn = data.First();
                }
                catch (Exception ex)
                {
                     
                    throw ex;
                }
            }
            return valueReturn;
        }


        public static int ExecuteByStoreReturnINT(SqlCommand objComando, string nameProcedure, SqlParameter outputParam)
        {
            SqlConnection objConexion = default(SqlConnection);
           // SqlCommand objComando = default(SqlCommand);
            SqlDataAdapter objResultado = default(SqlDataAdapter);
            int IdValue = 0;
            //try
            //{

                String conexionString = GetADOConnectionString();
                objConexion = new SqlConnection(conexionString);
                objComando = new SqlCommand(nameProcedure, objConexion);
                objComando.CommandType = CommandType.StoredProcedure;
                objResultado = new SqlDataAdapter(objComando);

                // add OutputParameterS
               
                objConexion.Open();
                objComando.ExecuteNonQuery();
                if (outputParam.Value != null)
                {
                    IdValue = int.Parse(outputParam.Value.ToString()); }
                else
                {
                    IdValue = 0; 
                }



                objConexion.Close();
               


            //}
            //catch (Exception ex)
            //{

            //    String error = ex.Message;
            //    IdValue = -1;


            //}
            return IdValue;
        }

        public static string GetADOConnectionString()
        {

            var connection =    System.Configuration.ConfigurationManager.    ConnectionStrings["BdEntityGenerico"].ConnectionString;
            string abc = ConfigurationManager.ConnectionStrings["BdEntityGenerico"].ConnectionString;

             string cadenaSQL = WebConfigurationManager.ConnectionStrings["BdEntityGenerico"].ConnectionString;

            return "data source=168.62.183.84,62691;initial catalog=Llamagas_Upgrade;user id=sa;password=royal2016";
            //using (BdEntityGenerico context = new BdEntityGenerico())
            //{
            //    using (var dbContextTransaction = context.Database.BeginTransaction())
            //    {
            //        {
            //            try
            //            {

            //                String ca = context.Database.Connection.ConnectionString;

            //                return ca;

            //            }
            //            catch (Exception ex)
            //            {

            //                throw ex;
            //            }
            //        }
            //    }
            //} 

        }

        public static decimal getValueDecimal(string sqlname, List<SqlParameter> parametros)
        {
            using (BdEntityGenerico context = new BdEntityGenerico())
            {
                if ( parametros == null || parametros.Count() == 0 )
                {
                    return context.Database.SqlQuery<decimal>(sqlname).FirstOrDefault();
                }
                else
                {
                    return context.Database.SqlQuery<decimal>(sqlname, parametros.ToArray()).FirstOrDefault();
                }
            }
        }
        public static int getValueInt(string sqlname, List<SqlParameter> parametros)
        {

          
                using (BdEntityGenerico context = new BdEntityGenerico())
                {
                    if (parametros == null)
                    {
                        return context.Database.SqlQuery<int>(sqlname).FirstOrDefault();
                    }
                    else
                    {
                        return context.Database.SqlQuery<int>(sqlname, parametros.ToArray()).FirstOrDefault();
                    }
                }
           
           
           
        }

        public static int getValueIntOnly(string sqlname)
        {


            using (BdEntityGenerico context = new BdEntityGenerico())
            {
                return context.Database.SqlQuery<int>(sqlname).FirstOrDefault();
            }



        }


        public static ErrorObj ExecuteQueryResponse(string sqlname, List<SqlParameter> parametros)
        {
            ErrorObj error = new ErrorObj();
            try
            {
                using (BdEntityGenerico context = new BdEntityGenerico())
                {
                    if (parametros == null)
                    {
                        var res= context.Database.SqlQuery<string>(sqlname).FirstOrDefault();

                        error.CodigoError = 200;
                        error.ValorDevolucion = res;
                        error.MensajeError = "OK";

                        return error;
                    }
                    else
                    {
                        var res = context.Database.SqlQuery<string>(sqlname, parametros.ToArray()).FirstOrDefault();

                        error.CodigoError = 200;
                        error.ValorDevolucion = res;
                        error.MensajeError = "OK";

                        return error;
                    }
                }
            }
            catch (Exception e)
            {
                error.CodigoError = 1500; // CPN E
                error.MensajeError = e.Message;


                return error;
            }

        }

        public static string getValuString(string sqlname, List<SqlParameter> parametros)
        {
            using (BdEntityGenerico context = new BdEntityGenerico())
            {
                if ( parametros == null || parametros.Count() == 0 )
                {
                    return FuncPrinc.trimValor(context.Database.SqlQuery<string>(sqlname).FirstOrDefault());
                }
                else
                {

                    

                    return FuncPrinc.trimValor(context.Database.SqlQuery<string>(sqlname, parametros.ToArray()).FirstOrDefault());
                }
            }
        }

        public static DateTime getValueDatetime(string sqlname, List<SqlParameter> parametros)
        {
            using (BdEntityGenerico context = new BdEntityGenerico())
            {
                if ( parametros == null || parametros.Count() == 0 )
                {
                    return context.Database.SqlQuery<DateTime>(sqlname).FirstOrDefault();
                }
                else
                {
                    return context.Database.SqlQuery<DateTime>(sqlname, parametros.ToArray()).FirstOrDefault();
                }
            }
        }

        public static string getParametroString(string company, string parametroclave)
        {

            List<SqlParameter> parametros = new List<SqlParameter>(); 
            string sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Configuracion.getParametroTextto"); 

            parametros.Add(new SqlParameter("@Compania", company));
            parametros.Add(new SqlParameter("@ParametroClave", parametroclave)); 
            return FuncPrinc.trimValor(getValuString(sqlString, parametros));
        }

        public static string getParametroInt(string company, string parametroclave)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            string sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Configuracion.getParametroNumero");

            parametros.Add(new SqlParameter("@Compania", company));
            parametros.Add(new SqlParameter("@ParametroClave", parametroclave));
            return getValuString(sqlString, parametros);
        }
    }
}