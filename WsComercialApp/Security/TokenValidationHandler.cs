using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using WsComercialApp.Utils; 
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression; 
using System.Web.Http;
using System.Threading;

namespace WsComercialApp.Security
{
    public class TokenValidationHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                
                var myError = new ErrorResponse
                {
                    Status = "Unauthorized",
                    Message = "Necesita añadir Authorization en las cabeceras para acceder a esta petición..!!",

                };

                //return new ErrorResult(myError, Request);


                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized, "value");
                response.Content = new ObjectContent<ErrorResponse>(myError, new JsonMediaTypeFormatter());
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            // determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {


                var validaPath = request.RequestUri.LocalPath;

                //if (validaPath!= "/api/login/authenticate")
                //{
                //    var myError = new Error
                //    {
                //        Status = "Unauthorized",
                //        Message = "Debe añadir el Authorization en las cabeceras para poder usar la petición..!!",

                //    };

                //    //return new ErrorResult(myError, Request);


                //    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized, "value");
                //    response.Content = new ObjectContent<Error>(myError, new JsonMediaTypeFormatter());
                //    response.Headers.CacheControl = new CacheControlHeaderValue()
                //    {
                //        MaxAge = TimeSpan.FromMinutes(20)
                //    };
                //    return Task<HttpResponseMessage>.FromResult(response);
                //}

                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // Extract and assign Current Principal and user
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;

                var myError = new ErrorResponse
                {
                    Status = "Unauthorized",
                    Message = "Token Caducado..!!",

                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized, "value");
                response.Content = new ObjectContent<ErrorResponse>(myError, new JsonMediaTypeFormatter());
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };
                return Task<HttpResponseMessage>.FromResult(response);


            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;

                var res = ex.InnerException;


                var myError = new ErrorResponse
                {
                    Status = "Error",
                    Message = "Token inválido, verique su token e intente nuevamente..!!",

                };

                //return new ErrorResult(myError, Request);


                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "value");
                response.Content = new ObjectContent<ErrorResponse>(myError, new JsonMediaTypeFormatter());
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };
                return Task<HttpResponseMessage>.FromResult(response);



            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }



        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }


    }
}