using InvBusinessLayer.Model;
using InvDatabaseLayer.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvBusinessLayer.Method
{
   public interface IQuantity
    {
        Task<ResponseViewModel<QuantityViewModel>> Get(QuantityViewModel BrandObj);
        Task<ResponseViewModel<QuantityViewModel>> CreateAndUpdate(QuantityViewModel BrandObj);
        Task<ResponseViewModel<QuantityViewModel>> Delete(QuantityViewModel BrandObj);
    }
}
