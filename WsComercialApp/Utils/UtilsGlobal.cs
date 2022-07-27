using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace WsComercialApp.Utils
{
    public class UtilsGlobal
    {
        public static string ExtensionTxt = ".txt";
        public static string ExtensionXml = ".xml";
        public static string RutaPathFile = @".\Models\Querys";



        public static string ConvertLinesSql(string[] sqlRuta)
        {
            string retunsql = "";
            foreach (string line in sqlRuta)
            {

                retunsql += line + " ";
            }

            return retunsql;
        }

        public static string ConvertLinesSqlXml(string QueryFile, string queryMethod)
        {
            string retunsql = "";

            var Queryfolder = UtilsGlobal.ConvertNameQueryXml(QueryFile.Trim());
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Queryfolder);
            XmlNodeList xP = xDoc.GetElementsByTagName("sql-query");
            XmlNodeList xLista = ((XmlElement)xP[0]).GetElementsByTagName(queryMethod.Trim());

            foreach (XmlElement nodo in xLista)
            {
                retunsql += nodo.InnerText + " ";
            }

            return retunsql.Trim();
        }





        public static string ConvertNameQueryText(string nameQuery)
        {

            var ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Models\Querys\" + nameQuery + "" + UtilsGlobal.ExtensionTxt + "");

            return ruta;
        }
        public static string ConvertNameQueryXml(string nameQuery)
        {

            var ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Models\Querys\" + nameQuery + "" + UtilsGlobal.ExtensionXml + "");

            return ruta;
        }


        #region UsoArchivoTexto


        //var Queryfolder = UtilsGlobal.ConvertNameQueryText("QueryMiscelaneos");
        //string[] sqlRead = System.IO.File.ReadAllLines(@Queryfolder);
        //string sqlString = UtilsGlobal.ConvertLinesSql(sqlRead);            


        #endregion
    }
}