using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvBusinessLayer.Model;
using InvBusinessLayer.Method;
using System.IO;

namespace Inventory.Controllers
{
    public class BrandController : ControllerBase
    {
        IBrand brandMethod;
        public BrandController(IBrand _brandMethod) {
            brandMethod = _brandMethod;
        }
        [HttpGet]
        [Route("api/Brand/Get")]
        public async Task<ResponseViewModel<BrandViewModel>> Get(BrandViewModel BrandObj) {
            return await brandMethod.Get(BrandObj); 
        }
        [HttpPost]
        [Route("api/Brand/Save")]
        public async Task<ResponseViewModel<BrandViewModel>> Save([FromBody]BrandViewModel BrandObj)
        {
            return await brandMethod.CreateAndUpdate(BrandObj);
        }
        [HttpDelete]
        [Route("api/Brand/Delete")]
        public async Task<ResponseViewModel<BrandViewModel>> Delete([FromBody]BrandViewModel BrandObj)
        {
            return await brandMethod.Delete(BrandObj);
        }
        [HttpPost, DisableRequestSizeLimit]
        [Route("api/Brand/Upload")]
        public async Task<ResponseViewModel<BrandViewModel>> Upload()
        {
            ResponseViewModel<BrandViewModel> ResObj = new ResponseViewModel<BrandViewModel>();
            var Files = Request.Form.Files[0];
            var SavePath =Path.Combine(Directory.GetCurrentDirectory(),Path.Combine("Resources", "TSVFiles"));
            if (Files.Length > 0) {
                var FileName = Guid.NewGuid() + ".tsv";
                using (var stream = new FileStream(SavePath, FileMode.Create)) {
                  await Files.CopyToAsync(stream);
                }
                ResObj.IsSuccess = true;
            }
            return ResObj;
        }
    }
}
