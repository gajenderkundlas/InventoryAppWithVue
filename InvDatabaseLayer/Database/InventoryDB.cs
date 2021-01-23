using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace InvDatabaseLayer.Database
{
    public class InventoryDBContext : DbContext
    {
        string ConnectionString = "";
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Brand_Quantity_Time_Received> Brand_Quantity_Time_Received { get; set; }
        public InventoryDBContext(string _connectionString) {
            this.ConnectionString = _connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(this.ConnectionString);
        }
    }
    public class Brand { 
       [Key]
       public int Brand_Id { get; set; }
       public string Name { get; set; }
    }
    public class Brand_Quantity_Time_Received
    {
        [Key]
        public int Inventory_Id { get; set; }
        public int Brand_Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Time_Received { get; set; }
    }
}
