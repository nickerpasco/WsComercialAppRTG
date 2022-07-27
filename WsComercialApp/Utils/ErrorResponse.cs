using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsComercialApp.Utils
{
    public class ErrorResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Datas> data { get; set; }

        public class Datas
        {
            public string pro1 { get; set; }
            public string pro2 { get; set; }

            public string Token { get; set; }



        }
    }
}