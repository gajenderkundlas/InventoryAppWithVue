using System;
using System.Collections.Generic;
using System.Text;

namespace InvBusinessLayer.Model
{
   public class QuantityViewModel
    {
       public int Inventory_Id   {get;set;}
       public int Brand_Id       {get;set;}
        public string Name { get; set; }
        public int Quantity       {get;set;}
        public int TotalQuantity { get; set; }
        public DateTime Time_Received { get; set; }
        public ConnectionStringViewModel ConnectionString { get; set; }
    }
}
