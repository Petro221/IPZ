//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderedFood
    {
        public int Id { get; set; }
        public int order_id { get; set; }
        public int category_id { get; set; }
        public int number { get; set; }
    
        public virtual CategoryDish CategoryDish { get; set; }
        public virtual Order Order { get; set; }
    }
}
