//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF_DB_MARKET.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_Product
    {
        public string Pid { get; set; }
        public string Pname { get; set; }
        public int Pleft { get; set; }
        public string Ptype { get; set; }
        public Nullable<double> Pprice { get; set; }
        public Nullable<double> Pdiscount { get; set; }
    }
}
