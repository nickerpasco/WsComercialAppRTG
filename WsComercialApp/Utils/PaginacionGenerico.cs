using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsComercialApp.Models;

namespace WsComercialApp.Utils
{
    public class PaginacionGenerico
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int countBD { get; set; }
        public List<ModelDireccion> ListaDirecciones { get; internal set; }

        public List<Model_CO_Documento> lstCabeceraPedidos = new List<Model_CO_Documento>();
        public List<Model_PersonaMast> lstPersonas = new List<Model_PersonaMast>();
        public List<AgenciasModel> lstAgencias = new List<AgenciasModel>();
        
    }
}