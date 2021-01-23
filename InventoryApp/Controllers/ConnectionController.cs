using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvBusinessLayer.Model;
using InvBusinessLayer.Method;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Controllers
{
    public class ConnectionController : ControllerBase
    {
        IInventoryDbConnection dbConnection;
        public ConnectionController(IInventoryDbConnection _dbConnection)
        {
            dbConnection = _dbConnection;
        }
        [HttpGet]
        [Route("api/Connection/CheckConnection")]
        public async Task<ResponseViewModel<ConnectionStringViewModel>> CheckConnection(string DataSource,string Username,string Password)
        {
            ConnectionStringViewModel Connection = new ConnectionStringViewModel();
            Connection.DataSource = DataSource;
            Connection.Username = Username;
            Connection.Password = Password;
            return await dbConnection.IsServerExists(Connection);
        }
    }
}
