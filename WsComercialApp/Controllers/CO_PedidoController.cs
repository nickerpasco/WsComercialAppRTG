using System;
using System;
using System.Web.Http;
using System.Collections.Generic; 
using System.Data.SqlClient;
using WsComercialApp.Utils;
using WsComercialApp.Repositorio;
using WsComercialApp.Models;
using System.Linq;

namespace WsComercialApp.Controllers
{
    public class CO_PedidoController : ApiController
    {
        // GET:  CO_Pedido



        //[HttpPost]
        //[Route("api/CO_Pedido/SavePedido")]
        //public ModelTransac_CO_Pedido SavePedido([FromBody] ModelTransac_CO_Pedido documento)
        //{
        //    RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
        //    var response = repositorio.InsertCo_Documento(documento); // 
        //    return response;
        //}

        [HttpPost]
        [Route("api/Co_Documento/getCabecera")]
        public PaginacionGenerico getCabeceraDocumento(FiltroGenerico bean)
        {
            RepositorioCO_Pedido co_pedido = new RepositorioCO_Pedido();
            var resultlist = co_pedido.getCabeceraCodoc(bean);
            return resultlist;
        }

        [HttpPost]
        [Route("api/Co_Documento/getDetalle")]
        public List<Model_CO_DocumentoDetalle> getDetalle(FiltroGenerico bean)
        {
            RepositorioCO_Pedido co_pedido = new RepositorioCO_Pedido();
            List<Model_CO_DocumentoDetalle> resultlist = co_pedido.getDataDetalle(bean);
            return resultlist;
        }  



        [HttpPost]
        [Route("api/SavePedido")]
        public ModelTransac_CO_Documento SavePedido([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.InsertCo_Documento(documento); // 
            return response; 
        }
         

        [HttpPost]
        [Route("api/SaveFacturacion")]
        public ModelTransac_CO_Documento SaveFacturacion([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.SaveFacturacion(documento); // 
            return response;
        }



         [HttpPost]
        [Route("api/ExportarPdfComprobante")]
        public ModelTransac_CO_Pedido ExportarPdfComprobante([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.ExportarComprobantePdf(documento); // 
            return response;
        }


        [HttpPost]
        [Route("api/AnularPedido")]
        public ModelTransac_CO_Documento AnularPedido([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.AnularPedido(documento); // 
            return response;
        }
        
        [HttpPost]
        [Route("api/ExportarPdf")]
        public ModelTransac_CO_Pedido ExportarPdf([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.ExportarPdf(documento); // 
            return response;

        }

        [Route("api/getTipoCambio")]
        public List<ModelMiscelaneos> getTipoCambio()
        {
            RepositorioCO_Pedido g = new RepositorioCO_Pedido();

            var lstMicelaneo = g.getTipoCambio().ToList();

            return lstMicelaneo;
        }


        [HttpPost]
        [Route("api/getMiscelaneosUbigeo")]
        public List<ModelMiscelaneos> getMiscelaneos([FromBody] ModelTransacPersona bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.getMiscelaneos(bean);
            return resultlist;

        }

        [HttpPost]
        [Route("api/getDescuentos")]
        public List<DescuentoModel> getDescuentos([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.getDescuentosList(documento); // 
            return response;
        }
        
        [HttpPost]
        [Route("api/getDescuentosReglas")]
        public List<DescuentoModel> getDescuentosReglas([FromBody] ModelTransac_CO_Documento documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.getDescuentosReglas(documento); // 
            return response;
        } 

        [HttpPost]
        [Route("api/getAgencias")]
        public PaginacionGenerico getAgencias([FromBody] FiltroGenerico documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
           var response = repositorio.getAgencias(documento); // 
            return response;
        }

        [HttpPost]
        [Route("api/getItemAlmacen")]
        public List<ModelItemAlmacen> getItemAlmacen([FromBody] FiltroGenerico documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.getItemAlmacen(documento); // 
            return response;
        }

        [HttpPost]
        [Route("api/getLetrasCabecera")]
        public PaginacionGenerico getLetrasCabecera([FromBody] FiltroGenerico documento)
        {
            RepositorioCO_Pedido repositorio = new RepositorioCO_Pedido();
            var response = repositorio.getLetrasCabecera(documento); // 
            return response;
        }


    }
}

