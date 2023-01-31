using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web; 
using WsComercialApp.Models;
using WsComercialApp.Models.Bd;
using WsComercialApp.Utils;

namespace WsComercialApp
{
    public class FuncPrinc
    {

        public static List<T> ConvertList<T>(List<object> value) where T : class
        {
            List<T> newlist = value.Cast<T>().ToList();
            return newlist;
        }

        public static byte[] ReadFully(Stream input)
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

         
        public static object ConvertListToObject<T>(List<object> value) where T : class
        {
            var newlist = value.Cast<T>().ToList();
            return newlist;
        }

        public static int[] ASCII_DEC = { 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
            52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78,
            79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104,
            105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125,
            126, 128, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 142, 145, 146, 147, 148, 149, 150, 151,
            152, 153, 154, 155, 156, 158, 159, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 174, 175,
            176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196,
            197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217,
            218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238,
            239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255 };

        

        public static string FormatDateTimeString(DateTime tiempoLLegada)
        {
            String ret = tiempoLLegada.ToString("dd/MM/yyyy HH:mm");
            return ret;
        }

        public static char[] ASCII_CHAR = { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.',
            '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C',
            'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
            'Y', 'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~'

    }; 

        public static string trimValor(string valor)
        {
            if (valor  != null && valor != "")
            {
                return valor.Trim();
            }
            return "";
        }

        public static int parseInt(Nullable<int> valor)
        {
            if (valor != null)
            {
                return (int)valor;
            }
            return 0;
        }


        public static String getTipoDocumentoSUNAT(String tipoDocumentoSPRING)
        {


            ////SUNAT CÓDIGOS
            //01 -- LIBRETA ELECTORAL O DNI
            //04 -- CARNET DE EXTRANJERIA
            //06 -- REG. UNICO DE CONTRIBUYENTES
            //07 -- PASAPORTE
            //11 -- PART. DE NACIMIENTO-IDENTIDAD
            //00 -- OTROS

            if (tipoDocumentoSPRING==("R"))
            {
                return "6";

            }
            else if (tipoDocumentoSPRING == ("D"))
            {
                return "1";
            }
            else if (tipoDocumentoSPRING == ("O"))
            {
                return "0";
            }
            return "0";
        }




        public static decimal parseDecimal(Nullable<decimal> valor)
        {
            if (valor != null)
            {
                return (decimal)valor;
            }
            return 0;
        }

        

        public static Boolean ValidarRuc(String ruc)
        {
            if (ruc == null)
            {
                return false;
            }

             int[] multipliers = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
             String[] prefixes = getRucPrefixes();
             int length = multipliers.Length + 1;

            if (ruc.Length != length)
            {
                return false;
            }

            Boolean isPrefixOk = false;

            foreach (String prefix in prefixes)
            {
                if (ruc.Substring(0, 2).Equals(prefix))
                {
                    isPrefixOk = true;
                    break;
                }
            }

            if (!isPrefixOk)
            {
                return false;
            }

            double sum = 0;

            for (int i = 0; i < multipliers.Length; i++)
            {
                  char section = ruc[i];

                //char caracter = texto[i];

                if (!Char.IsDigit(section))
                {
                    return false;
                }

               
                var res = Char.GetNumericValue(ruc[i]) * multipliers[i];

                sum += res;
            }
            double lentdouble = Convert.ToDouble(length);
              var rest = sum % lentdouble;
              String response = ""+ (lentdouble - rest);

            //return response.charAt(response.Length - 1) == ruc.charAt(ruc.Length - 1);
            return response[response.Length - 1] == ruc[ruc.Length - 1];
        }

        internal static ModelTransacPersona GetObjetoPersona(string Texto)
        {
            ModelTransacPersona returnData = new ModelTransacPersona();

            string[] LstApellidos = Texto.Split('@');

            returnData.ApellidoPaterno = LstApellidos[0];
            returnData.ApellidoMaterno = LstApellidos[1];

            int vueltas = 1; 

            foreach(var data in LstApellidos)
            { 
                 

                if (vueltas >= 3)
                {
                    returnData.Nombres += " "+ data;
                } 
                vueltas++;
            }  
            return returnData;
        }

        

        public static string SepararApellidosCompuestos(String rng)
        {
            string SEPARARAPELLIDOSRet = default;

            string[] nombreArr;
            var nuevaCadena = default(string);
            int i;

            // Dvidir el nombre por palabras en un arreglo
            //nombreArr = Microsoft.VisualBasic.Strings.Split(Microsoft.VisualBasic.Strings.Trim(rng.Value));

            nombreArr = Microsoft.VisualBasic.Strings.Split(Microsoft.VisualBasic.Strings.Trim(rng));

            // Analizar cada palabra dentro del arreglo 

            var loopTo = Microsoft.VisualBasic.Information.UBound(nombreArr);
            for (i = 0; i <= loopTo; i++)
            {
                switch (Microsoft.VisualBasic.Strings.LCase(nombreArr[i]) ?? "")
                {

                    // Palabras que forman parte de un apellido compuesto
                    // Agregar nuevas palabras separadas por una coma
                    case "de":
                    case "del":
                    case "la":
                    case "las":
                    case "los":
                    case "san":
                    case "de la":
                    case "de las":
                    case "y":
                        {
                            // Insertar espacio en blanco
                            nuevaCadena = nuevaCadena + nombreArr[i] + " ";
                            break;
                        }

                    default:
                        {
                            // Insertar caracter delimitador
                            nuevaCadena = nuevaCadena + nombreArr[i] + "@";
                            break;
                        }

                }
            }

            // Remover el último caracter delimitador de la cadena
            if (Microsoft.VisualBasic.Strings.Right(nuevaCadena, 1) == "@")
            {
                nuevaCadena = Microsoft.VisualBasic.Strings.Left(nuevaCadena, Microsoft.VisualBasic.Strings.Len(nuevaCadena) - 1);
            }

            SEPARARAPELLIDOSRet = nuevaCadena;
            return SEPARARAPELLIDOSRet;

        }

        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(original.Length - numberCharacters);
        }


        //public string SEPARARAPELLIDOS(String rng)
        //{
        //    string SEPARARAPELLIDOSRet = default;

        //    string[] nombreArr;
        //    var nuevaCadena = default(string);
        //    int i;

        //    // Dvidir el nombre por palabras en un arreglo
        //    nombreArr = Strings.Split(Strings.Trim(rng));

        //    // Analizar cada palabra dentro del arreglo
        //    var loopTo = Information.UBound(nombreArr);
        //    for (i = 0; i <= loopTo; i++)
        //    {
        //        switch (Strings.LCase(nombreArr[i]) ?? "")
        //        {

        //            // Palabras que forman parte de un apellido compuesto
        //            // Agregar nuevas palabras separadas por una coma
        //            case "de":
        //            case "del":
        //            case "la":
        //            case "las":
        //            case "los":
        //            case "san":
        //                {
        //                    // Insertar espacio en blanco
        //                    nuevaCadena = nuevaCadena + nombreArr[i] + " ";
        //                    break;
        //                }

        //            default:
        //                {
        //                    // Insertar caracter delimitador
        //                    nuevaCadena = nuevaCadena + nombreArr[i] + "@";
        //                    break;
        //                }

        //        }
        //    }

        //    // Remover el último caracter delimitador de la cadena
        //    if (String.Right(nuevaCadena, 1) == "@")
        //    {
        //        nuevaCadena = Strings.Left(nuevaCadena, Strings.Len(nuevaCadena) - 1);
        //    }

        //    SEPARARAPELLIDOSRet = nuevaCadena;
        //    return SEPARARAPELLIDOSRet;

        //}

        public static String[] getRucPrefixes()
        {
            return new String[] { "10", "15", "17", "20" };
        }

        public static int convertChar2Ascii(char caracter)
        {
            for (int i = 0; i < ASCII_CHAR.Length; i++)
            {
                if (ASCII_CHAR[i] == caracter)
                {
                    return ASCII_DEC[i];
                }
            }

            return (int)caracter;
        }

        public static char convertAscii2Char(int ascii)
        {
            for (int i = 0; i < ASCII_DEC.Length; i++)
            {
                if (ASCII_DEC[i] == ascii)
                {
                    return ASCII_CHAR[i];
                }
            }
            return (char)ascii;
        }

        public static String springEncriptar(String texto)
        {

            String desencriptado = null;

            if (texto != null && !texto.Equals(""))
            {

                desencriptado = "";
                // Contador que sirve para agregar valor al ascii generado
                // por cada caracter del texto
                int plus = 1;

                for (int i = 0; i < texto.Length; i++)
                {
                    char caracter = texto[i];

                    int ascii = convertChar2Ascii(caracter) + plus;

                    desencriptado = desencriptado + convertAscii2Char(ascii);

                    plus++;
                }

                return desencriptado;
            }
            return null;
        }
        public static String springDesencriptar(string password)
        {

            String encriptado = null;

            if (password != null && !password.Equals(""))
            {

                encriptado = "";
                int minus = 1;

                for (int i = 0; i < password.Length; i++)
                {
                    char caracter = password[i];

                    int ascii = convertChar2Ascii(caracter) - minus;
                    encriptado = encriptado + convertAscii2Char(ascii);

                    minus++;
                }

                return encriptado;
            }
            return null;
        }

        public static string returnNull(string valor)
        {

            if (valor == null || valor =="")
            {
                return "Valor Vacío";
            }
            else
            {
                return valor;
            }

            
        }



        public static ErrorObj CorreoMailArchivo(string str_pTo, string str_pCC, string str_pBCC, string asunto, string mensaje, string UserName, string Password, List<ModelTransac_CO_Pedido> lstFIles)
        {
            ErrorObj error = new ErrorObj();

            try
            {
                MailMessage obj_Mail = new MailMessage();
                obj_Mail.From = new MailAddress(UserName, "Administrador");

                if (str_pTo != null)
                {
                    if (str_pTo.Length > 0)
                    {
                        obj_Mail.To.Add(new MailAddress(str_pTo));
                    }
                }
                if (str_pCC != null)
                {
                    if (str_pCC.Length > 0)
                    {
                        obj_Mail.To.Add(new MailAddress(str_pCC));
                    }
                }
                if (str_pBCC != null)
                {
                    if (str_pBCC.Length > 0)
                    {
                        string[] Contenido = str_pBCC.Split('|');
                        for (int i = 0; i < Contenido.Length; i++)
                        {
                            if (Contenido[i].Length > 0)
                            {
                                obj_Mail.To.Add(new MailAddress(Contenido[i].Trim()));
                            }
                        }
                    }
                }
                obj_Mail.BodyEncoding = System.Text.Encoding.UTF8;
                obj_Mail.Subject = asunto;
                obj_Mail.IsBodyHtml = true; 
                obj_Mail.Body = mensaje;



               
                if (lstFIles.Count > 0)
                {
                    long lng_AdjuntosTotal = 0;
                    //FileStream obj_Stream = new FileStream(ruta_archivo_adjunto, FileMode.Open, FileAccess.Read);

                    //lng_AdjuntosTotal += obj_Stream.Length;


                    foreach(var item in lstFIles)
                    {
                        byte[] bytes = System.Convert.FromBase64String(item.BASE64Certificado);
                        Attachment att = new Attachment(new MemoryStream(bytes), item.NumeroDocumento+".pdf");
                        //Attachment obj_Adjunto = new Attachment(att, nom_archivo, MediaTypeNames.Application.Octet);
                        obj_Mail.Attachments.Add(att);
                    }


                    

                }



                SmtpClient obj_Smtp = new SmtpClient();
                obj_Smtp.Host = "smtp.gmail.com";
                obj_Smtp.Port = 587;
                obj_Smtp.EnableSsl = true;
                obj_Smtp.UseDefaultCredentials = false;
                obj_Smtp.Credentials = new System.Net.NetworkCredential(UserName, Password);

                 

                obj_Smtp.Send(obj_Mail);


               



                error.CodigoError = 200;
                error.MensajeError = "ENVIO OK";
            }
            catch (Exception e)
            {
                e.Message.ToString();
                error.CodigoError = 500;
                error.MensajeError = e.Message.ToString();

                 
                FuncPrinc.SaveLog("XX", 1, e.Message, "", 0, str_pTo, "ENVIO_CORREO");



                return error;
            }
            return error;
        }

        public static void SaveLog(String documento,int tipoerror,String mensaje, String JsonSeguimiento, int lineaerror, String usuario, String Proceso)
        {
             

           //var sqlString = UtilsGlobal.ConvertLinesSqlXml("QueryMiscelaneos", "Miscelaneos.ControlErroresLOG");
             

           // List<SqlParameter> parametros = new List<SqlParameter>();
           // parametros.Add(new SqlParameter("@Documento", documento));
           // parametros.Add(new SqlParameter("@TipoError", tipoerror));
           // parametros.Add(new SqlParameter("@MensajeError", mensaje));
           // parametros.Add(new SqlParameter("@JsonSeguimiento", JsonSeguimiento));
           // parametros.Add(new SqlParameter("@LineaError", lineaerror));
           // parametros.Add(new SqlParameter("@Usuario", usuario));
           // parametros.Add(new SqlParameter("@Proceso", Proceso));
           // UtilsDAO.ExecuteQueryCRUD(sqlString, parametros);

            
        }

        public static decimal? returnZeroifnull(decimal? cantidadVendidaAdicional)
        {
            if (cantidadVendidaAdicional == null)
            {
                return 0;
            }
            return 0;
        }

        internal static string GenerarQRSunat(CO_Documento otabla, String RUCCompania, String TipoDocumentoCliente,String DocumentoFiscalTipo)
        {
            QRCoder.QRCodeGenerator QR = new QRCoder.QRCodeGenerator();
            ASCIIEncoding ASSCII = new ASCIIEncoding();
            var z = QR.CreateQrCode(ASSCII.GetBytes(getTextoConcatenado(otabla, RUCCompania, TipoDocumentoCliente)), QRCoder.QRCodeGenerator.ECCLevel.H);
            QRCoder.PngByteQRCode png = new QRCoder.PngByteQRCode();
            png.SetQRCodeData(z);
            var bytes = png.GetGraphic(10);

            string temp_inBase64 = Convert.ToBase64String(bytes);

            //MemoryStream ms = new MemoryStream();
            //ms.Write(bytes, 0, bytes.Length);
            //Bitmap b = new Bitmap(ms);


            String ruta = @"C:\PDF\";
            String nombrearchivo = RUCCompania+"-"+ DocumentoFiscalTipo+"-"+ otabla.NumeroDocumento.Trim()+ ".BMP";
            String rutaarchivo = ruta+ nombrearchivo;


            if (bytes != null)
            {

                BorrarFile(ruta);

                System.IO.FileStream pdfFile = new System.IO.FileStream(ruta + @"\"+ nombrearchivo, System.IO.FileMode.Create);
                pdfFile.Write(bytes, 0, bytes.Length);
                pdfFile.Close();
            }
            else
            {
                rutaarchivo = "";
            }


            return rutaarchivo;
        }

        private static void BorrarFile(string ruta)
        {
            if (System.IO.File.Exists(ruta))
            {
                while (System.IO.File.Exists(ruta))
                {
                    //Si existe borramos el archivo d
                    System.IO.File.Delete(ruta);
                }
            }
        }

        private static String getTextoConcatenado(CO_Documento o, String RUCCompania,String TipoDocumentoCliente)
        {

            String seriesss = o.NumeroDocumento.Substring(0, 4);
            String nross = o.NumeroDocumento.Substring(5);

            //DateTime dt = DateTime.ParseExact(o.FechaDocumento.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string dateeee = DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

            //ModelConfiguracion ss = (ModelConfiguracion) FuncPrinc.getDataConfiguracion(context,o.getTipoDocumento());
            String rucEmpresa = RUCCompania;
            String tipodocempresa = o.TipoDocumento;
            String serie = seriesss.ToUpper();
            String nrodoc = nross;
            String impuestoventas = Convert.ToDouble(o.MontoImpuestoVentas).ToString("#,##0.00");
            String Montototal = Convert.ToDouble(o.MontoTotal).ToString("#,##0.00");
            String fecha = dateeee;// o.FechaDocumento;
            String TipoDocSunat = FuncPrinc.getTipoDocumentoSUNAT(TipoDocumentoCliente);
            String rucpersona = "" + o.ClienteRUC;
            String codeHash = "";//Globales.CodigoHashResponse;

            String salida = rucEmpresa + "|" + tipodocempresa + "|" + serie.ToUpper() + "|"
                    + nrodoc + "|" + impuestoventas + "|" + Montototal + "|" + fecha + "|" + TipoDocSunat + "|" + rucpersona;
            String salidahsh = rucEmpresa + "|" + tipodocempresa + "|" + serie.ToUpper() + "|"
                    + nrodoc + "|" + impuestoventas + "|" + Montototal + "|" + fecha + "|" + TipoDocSunat + "|" + rucpersona + "|" + codeHash;


            if (!codeHash.Equals(""))
            {
                return salida;
            }
            else
            {
                return salidahsh;
            }


        }

        internal static DateTime? ConvertDateFromString(string fechaVencimiento)
        {
            DateTime dateTime = DateTime.Parse(fechaVencimiento);
            return dateTime;
        }
    }
}