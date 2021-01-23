using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvBusinessLayer.Model;
using InvBusinessLayer.Method;

namespace Inventory.Controllers
{
    public class QuantityController : ControllerBase
    {
        IQuantity quantityMethod;
        public QuantityController(IQuantity _quantityMethod) {
            quantityMethod = _quantityMethod;
        }
        [HttpGet]
        [Route("api/Quantity/Get")]
        public async Task<ResponseViewModel<QuantityViewModel>> Get(QuantityViewModel QuantityObj) {
            return await quantityMethod.Get(QuantityObj); 
        }
        [HttpPost]
        [Route("api/Quantity/Save")]
        public async Task<ResponseViewModel<QuantityViewModel>> Save([FromBody]QuantityViewModel QuantityObj)
        {
            return await quantityMethod.CreateAndUpdate(QuantityObj);
        }
        [HttpDelete]
        [Route("api/Quantity/Delete")]
        public async Task<ResponseViewModel<QuantityViewModel>> Delete([FromBody] QuantityViewModel QuantityObj)
        {
            return await quantityMethod.Delete(QuantityObj);
        }
    }
}
