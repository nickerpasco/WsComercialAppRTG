using System;
using System.Web.Http;
using System.Collections.Generic;
using WsComercialApp.Models;
using WsComercialApp.Utils;
using Newtonsoft.Json;

namespace WsComercialApp.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET:  Usuario

        [HttpPost]
        [Route("api/Usuario/Login")]
        public ModelUsuario GetAllByID([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.GetAllByID(bean);
            return resultlist;

        }

        [HttpPost]
        [Route("api/Usuario/CambioClave")]
        public ModelUsuario CambioClave([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.CambioClave(bean);
            return resultlist;

        }


        [HttpPost]
        [Route("api/Usuario/getPreferencias")]
        public List<Model_Sy_Preferences> getPreferencias([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.getPreferencias(bean);
            return resultlist;

        }


        [HttpPost]
        [Route("api/Usuario/getMontosMes")]
        public List<Model_Dashboard> getMontosMes([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.getMontoMes(bean);
            return resultlist;

        }



        [Authorize]
        [HttpPost]
        [Route("api/Usuario/Test")]
        public ModelUsuario Test([FromBody] ModelUsuario bean)
        {

            return null;

        }


        [Authorize]
        [HttpPost]
        [Route("api/Usuario/LoginEndSession")]
        public ModelUsuario LoginEndSession([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.LoginEndSession(bean);
            return resultlist;

        }


        [HttpPost]
        [Route("api/Usuario/getDiasCreditoByTipo")]
        public Model_PersonaMast getDiasCreditoByTipo([FromBody] Model_PersonaMast bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.getDiasCreditoByTipo(bean);
            return resultlist;

        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("api/Personas/getOnline")]
        public PaginacionGenerico gestionGuiaCabecera([FromBody] FiltroGenerico bean)
        {

            RepositorioUsuario repositorio = new RepositorioUsuario();
            var resultlist = repositorio.getPersonasOnline(bean);
            return resultlist;

        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("api/SavePersona")]
        public ModelTransacPersona SavePersona([FromBody] ModelTransacPersona persona)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.InsertPersona(persona); // 
            return response;
        }


        [HttpPost]
        [Route("api/Usuario/getLineaCreditoCliente")]
        public InfoCliente_LCredito getLineaCreditoCliente([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            InfoCliente_LCredito credito = new InfoCliente_LCredito();

            InfoCliente_LCredito resultlist = usuario.getLineaCreditoCliente(bean);
            List<Model_MontosLineaCredito> resultlistMonto = usuario.getLineaCreditoClienteMonto(bean);

            credito = resultlist;
            credito.lst = resultlistMonto;

            return credito;

        }

        [HttpPost]
        [Route("api/Usuario/getValidarDiasPendientesFactura")]
        public ReponseStoreLetras getLineaCreditoCliente([FromBody] Model_CO_Documento bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();

            ReponseStoreLetras respuesta = usuario.getValidarDiasPendientesFactura(bean);

            return respuesta;

        }
        [HttpPost]
        [Route("api/Usuario/getDiasVencidoFacturas")]
        public ReponseStoreLetras getDiasVencidoFacturas([FromBody] Model_CO_Documento bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();

            ReponseStoreLetras respuesta = usuario.getDiasVencidoFacturas(bean);

            return respuesta;

        }


        [HttpPost]
        [Route("api/CabereceraLineaCredito")]
        public PaginacionGenerico CabereceraLineaCredito([FromBody] FiltroGenerico documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.CabereceraLineaCredito(documento); // 
            return response;
        }  
        
        [HttpPost]
        [Route("api/CabereceraLineaCreditoDetalle")]
        public List<CabeceraLineasCreditoDetalle> CabereceraLineaCreditoDetalle([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.CabereceraLineaCreditoDetalle(documento); // 
            return response;
        }


        [HttpPost]
        [Route("api/Usuario/getDetalleFacturasLinea")]
        public List<DetalleFacturasLinea> getDetalleFacturasLinea([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();      
            List<DetalleFacturasLinea> resultlist = usuario.getDetalleFacturasLinea(bean); 
            return resultlist;

        }

        [HttpPost]
        [Route("api/Usuario/getDocumentoSUNAT")]
        public SunatDataObject getDocumentoSUNAT([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();

            bool mensajeValidacion = FuncPrinc.ValidarRuc(bean.Documento);
            ErrorObj errorObj = new ErrorObj();
            SunatDataObject dataConvertida = new SunatDataObject();
            if (mensajeValidacion==false)
            {
                errorObj.CodigoError = 500;
                errorObj.MensajeError = "Ruc Inválido...";
                dataConvertida.lstErrores.Add(errorObj);
                return dataConvertida;               

            }
            try
            {
                String ResultJson = usuario.getDocumentoSUNAT(bean);                     
                dataConvertida = JsonConvert.DeserializeObject<SunatDataObject>(ResultJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                String Texto = dataConvertida.RazonSocial;
                string[] lsttexto = Texto.Split('-');
                dataConvertida.RazonSocial = lsttexto[1].Trim() ;

                String dataNombre = FuncPrinc.SepararApellidosCompuestos(dataConvertida.RazonSocial);
                ModelTransacPersona personaData = FuncPrinc.GetObjetoPersona(dataNombre);

                dataConvertida.Nombres = personaData.Nombres;
                dataConvertida.ApellidoPaterno = personaData.ApellidoPaterno;
                dataConvertida.ApellidoMaterno = personaData.ApellidoMaterno;



            }
            catch (Exception e)
            {
                errorObj.CodigoError = 500;
                errorObj.MensajeError = e.Message;

                if (errorObj.MensajeError == "Value cannot be null.\r\nParameter name: input")
                {
                    errorObj.MensajeError = "El Ruc ingresado no se encuentra registrado..";

                }

                dataConvertida.lstErrores.Add(errorObj);
                return dataConvertida;
            }
          
            return dataConvertida;

        }


        //REPORTES

        [HttpPost]
        [Route("api/DocumentosEmitidosByDay")]
        public PaginacionGenerico DocumentosEmitidosByDay([FromBody] FiltroGenerico documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.DocumentosEmitidosByDay(documento); // 
            return response;
        }
        
        [HttpPost]
        [Route("api/DocumentosEmitidosByDayDetalle")]
        public List<ModelTransac_CO_Documento> DocumentosEmitidosByDayDetalle([FromBody] FiltroGenerico documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.DocumentosEmitidosByDayDetalle(documento); // 
            return response;
        }

        [HttpPost]
        [Route("api/ReporteCobranzasByPeriodoDetalle")]
        public List<ModelTransac_CO_Documento> ReporteCobranzasByPeriodoDetalle([FromBody] FiltroGenerico documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.ReporteCobranzasByPeriodoDetalle(documento); // 
            return response;
        }

        [HttpPost]
        [Route("api/ReporteCobranzasByPeriodo")]
        public PaginacionGenerico ReporteCobranzasByPeriodo([FromBody] FiltroGenerico documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.ReporteCobranzasByPeriodo(documento); // 
            return response;
        }

        [HttpPost]
        [Route("api/ReporteRutasDespachoDetalle")]
        public List<ModelTransac_CO_Documento> ReporteRutasDespachoDetalle([FromBody] FiltroGenerico documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.ReporteRutasDespachoDetalle(documento); // 
            return response;
        }

        [HttpPost]
        [Route("api/ReporteRutasDespacho")]
        public PaginacionGenerico ReporteRutasDespacho([FromBody] FiltroGenerico documento)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            var response = repositorio.ReporteRutasDespacho(documento); // 
            return response;
        }




    }
}

