//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageNew
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public int NewID { get; set; }
        public string ImageURL { get; set; }
        public string Note { get; set; }
    
        public virtual News News { get; set; }
    }
}
