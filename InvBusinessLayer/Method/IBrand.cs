using InvBusinessLayer.Model;
using InvDatabaseLayer.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvBusinessLayer.Method
{
   public interface IBrand
    {
         Task<ResponseViewModel<BrandViewModel>> Get(BrandViewModel BrandObj);
         Task<ResponseViewModel<BrandViewModel>> CreateAndUpdate(BrandViewModel BrandObj);
         Task<ResponseViewModel<BrandViewModel>> Delete(BrandViewModel BrandObj);   
    }
}
