using System;
using System.Collections.Generic;
using System.Text;
using InvBusinessLayer.Model;
using InvDatabaseLayer.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace InvBusinessLayer.Method
{
   public class InventoryDbConnection : IInventoryDbConnection
    {
        public async Task<ResponseViewModel<ConnectionStringViewModel>> IsServerExists(ConnectionStringViewModel Conn) {
            ResponseViewModel<ConnectionStringViewModel> ResObj = new ResponseViewModel<ConnectionStringViewModel>();
            try
            {
                ResObj.IsSuccess = false;
                string ConnectionString = await GetConnectionString(Conn);
                InventoryDBContext dbContext = new InventoryDBContext(ConnectionString);
                if (dbContext.Database.GetService<IRelationalDatabaseCreator>().Exists())
                {
                    ResObj.IsSuccess = true;
                }
                else {
                    dbContext.Database.EnsureCreated();
                    ResObj.IsSuccess = true;
                }
            }
            catch (Exception ex) {
                ResObj.IsSuccess = false;
                ResObj.ErrorDetails = ex.ToString();
                ResObj.ErrorCode = 500;
            }
            return await Task.Run(() =>
            {
                return ResObj;
            });
        }
        public async Task<string> GetConnectionString(ConnectionStringViewModel Conn)
        {
           return await Task.Run(() =>
            {
                return "Data Source=" + Conn.DataSource + ";Initial Catalog=InventoryNew;User ID=" + Conn.Username + ";Password=" + Conn.Password; 
            });
        }
    }
}
