//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WsComercialApp.Models.Bd
{
    using System;
    using System.Collections.Generic;
    
    public partial class MA_FormadePagoDetalle
    {
        public string FormadePago { get; set; }
        public int Secuencia { get; set; }
        public Nullable<int> NumeroDias { get; set; }
        public Nullable<int> Porcentaje { get; set; }
        public string CompaniaCodigo { get; set; }
    
        public virtual MA_FormadePago MA_FormadePago { get; set; }
        public virtual MA_FormadePago MA_FormadePago1 { get; set; }
        public virtual MA_FormadePago MA_FormadePago2 { get; set; }
        public virtual MA_FormadePago MA_FormadePago3 { get; set; }
    }
}
