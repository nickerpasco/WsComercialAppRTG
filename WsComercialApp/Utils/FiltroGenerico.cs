using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Utils
{
    public class FiltroGenerico 
    {

        public string CompaniaSocio { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public int Vendedor { get; set; }
        public int Persona { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }

        public string BusquedaAvanzada { get; set; }
        public string Estado { get; set; }


        public List<ErrorObj> lstErrores = new List<ErrorObj>();

        public PaginacionGenerico paginacion = new PaginacionGenerico();

       

    }


}