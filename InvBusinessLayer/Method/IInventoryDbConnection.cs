using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InvBusinessLayer.Model;

namespace InvBusinessLayer.Method
{
   public interface IInventoryDbConnection
    {
        Task<ResponseViewModel<ConnectionStringViewModel>> IsServerExists(ConnectionStringViewModel Conn);
        Task<string> GetConnectionString(ConnectionStringViewModel Conn);
    }
}
