using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace InvBusinessLayer.Model
{
   public class BrandViewModel
    {
        public int Brand_Id { get; set; }
        public string Name { get; set; }
        public ConnectionStringViewModel ConnectionString { get; set; }
    }
}
