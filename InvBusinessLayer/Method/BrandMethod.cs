using InvBusinessLayer.Model;
using InvDatabaseLayer.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
namespace InvBusinessLayer.Method
{
   public class BrandMethod:IBrand
    {
        IInventoryDbConnection connMethod;
        public BrandMethod(IInventoryDbConnection _connection) {
            connMethod = _connection;
        }
        /// <summary>
        /// Get the list of brand by passing brand_id=0 and a single brand by passing brand_id 
        /// </summary>
        /// <param name="BrandObj"></param>
        /// <returns>response object</returns>
        public async Task<ResponseViewModel<BrandViewModel>> Get(BrandViewModel BrandObj) {

            ResponseViewModel<BrandViewModel> ResObj = new ResponseViewModel<BrandViewModel>();
            try
            {
                if (BrandObj != null)
                {
                    InventoryDBContext dbContext = new InventoryDBContext(await connMethod.GetConnectionString(BrandObj.ConnectionString));
                    var brand = dbContext.Brand.Where(x => x.Brand_Id == BrandObj.Brand_Id || BrandObj.Brand_Id == 0).ToList();
                    if (brand.Count() > 0)
                    {
                        ResObj.Data = JsonConvert.DeserializeObject<List<BrandViewModel>>(JsonConvert.SerializeObject(brand.ToList()));
                        ResObj.IsSuccess = true;
                    }
                    else
                    {
                        ResObj.IsSuccess = false;
                        ResObj.ErrorCode = 404;
                        ResObj.ErrorDetails = "No record found.";
                    }
                }
                else {
                    ResObj.IsSuccess = false;
                    ResObj.ErrorCode = 400;
                    ResObj.ErrorDetails = "Parameter not provided.";
                }
            }
            catch (Exception ex) {
                ResObj.IsSuccess = false;
                ResObj.ErrorCode = 500;
                ResObj.ErrorDetails = ex.ToString();
            }
            return await Task.Run(()=> { return ResObj; });
        }
        /// <summary>
        /// Method used for create and update brand, Pass Brand_Id=0 for create and Brand_Id>0 for update
        /// </summary>
        /// <param name="BrandObj"></param>
        /// <returns>Response Object</returns>
        public async Task<ResponseViewModel<BrandViewModel>> CreateAndUpdate(BrandViewModel BrandObj)
        {
            ResponseViewModel<BrandViewModel> ResObj = new ResponseViewModel<BrandViewModel>();
            try
            {
                if (BrandObj != null)
                {
                    bool IsNew = false;
                    InventoryDBContext dbContext = new InventoryDBContext(await connMethod.GetConnectionString(BrandObj.ConnectionString));
                    /*Check if already exists*/
                    Brand BrandDBObj = dbContext.Brand.Where(x => x.Brand_Id == BrandObj.Brand_Id).FirstOrDefault();
                    bool IsDuplicate = dbContext.Brand.Where(x => x.Name == BrandObj.Name && x.Brand_Id != BrandObj.Brand_Id).FirstOrDefault() != null?true:false;
                    if (!IsDuplicate)
                    {
                        if (BrandDBObj == null)
                        {
                            BrandDBObj = new Brand();
                            IsNew = true;
                        }
                        BrandDBObj.Name = BrandObj.Name;
                        if (IsNew)
                        {
                            await dbContext.Brand.AddAsync(BrandDBObj);
                        }
                        await dbContext.SaveChangesAsync();
                        BrandObj.Brand_Id = BrandDBObj.Brand_Id;
                        ResObj.IsSuccess = true;
                        List<BrandViewModel> BrandList = new List<BrandViewModel>();
                        BrandList.Add(BrandObj);
                        ResObj.Data = BrandList;
                    }
                    else {
                        ResObj.IsSuccess = false;
                        ResObj.ErrorCode = 500;
                        ResObj.ErrorDetails = "Duplicate brand name.";
                    }
                }
                else {
                    ResObj.IsSuccess = false;
                    ResObj.ErrorCode = 400;
                    ResObj.ErrorDetails = "Parameter not provided.";
                }
            }
            catch (Exception ex) {
                ResObj.IsSuccess = false;
                ResObj.ErrorCode = 500;
                ResObj.ErrorDetails = ex.ToString();
            }
            return  ResObj;
        }
        /// <summary>
        /// Delete Brand
        /// </summary>
        /// <param name="BrandObj"></param>
        /// <returns></returns>
        public async Task<ResponseViewModel<BrandViewModel>> Delete(BrandViewModel BrandObj)
        {
            ResponseViewModel<BrandViewModel> ResObj = new ResponseViewModel<BrandViewModel>();
            try
            {
                if (BrandObj != null)
                {
                    InventoryDBContext dbContext = new InventoryDBContext(await connMethod.GetConnectionString(BrandObj.ConnectionString));
                    /*Check if already exists*/
                    Brand BrandDBObj = dbContext.Brand.Where(x => x.Brand_Id == BrandObj.Brand_Id).FirstOrDefault();
                    if (BrandDBObj != null)
                    {
                        dbContext.Brand.Remove(BrandDBObj);
                        await dbContext.SaveChangesAsync();
                        ResObj.IsSuccess = true;
                    }
                    else
                    {
                        ResObj.IsSuccess = false;
                        ResObj.ErrorCode = 404;
                        ResObj.ErrorDetails = "Record not found.";
                    }
                }
                else
                {
                    ResObj.IsSuccess = false;
                    ResObj.ErrorCode = 400;
                    ResObj.ErrorDetails = "Parameter not provided.";
                }
            }
            catch (Exception ex)
            {
                ResObj.IsSuccess = false;
                ResObj.ErrorCode = 500;
                ResObj.ErrorDetails = ex.ToString();
            }
            return ResObj;
        }
    }
}
