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
  public  class QuantityMethod:IQuantity
    {
        IInventoryDbConnection connMethod;
        public QuantityMethod(IInventoryDbConnection _connection)
        {
            connMethod = _connection;
        }
        public async Task<ResponseViewModel<QuantityViewModel>> Get(QuantityViewModel QauntityObj)
        {

            ResponseViewModel<QuantityViewModel> ResObj = new ResponseViewModel<QuantityViewModel>();
            try
            {
                if (QauntityObj != null)
                {
                    InventoryDBContext dbContext = new InventoryDBContext(await connMethod.GetConnectionString(QauntityObj.ConnectionString));
                    var Qauntity = (from Quantity in dbContext.Brand_Quantity_Time_Received.Where(x => x.Inventory_Id == QauntityObj.Inventory_Id || QauntityObj.Inventory_Id == 0)
                                    join brand in dbContext.Brand on Quantity.Brand_Id equals brand.Brand_Id
                                    select new QuantityViewModel {
                                    Inventory_Id=Quantity.Inventory_Id,
                                    Brand_Id=Quantity.Brand_Id,
                                    Name=brand.Name,
                                    Time_Received=Quantity.Time_Received,
                                    Quantity=Quantity.Quantity
                                    }
                                    ).ToList();
                    if (Qauntity.Count() > 0)
                    {
                        Qauntity[0].TotalQuantity = Qauntity.Sum(x => x.Quantity);
                        ResObj.Data = JsonConvert.DeserializeObject<List<QuantityViewModel>>(JsonConvert.SerializeObject(Qauntity.ToList()));
                        ResObj.IsSuccess = true;
                    }
                    else
                    {
                        ResObj.IsSuccess = false;
                        ResObj.ErrorCode = 404;
                        ResObj.ErrorDetails = "No record found.";
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
            return await Task.Run(() => { return ResObj; });
        }
        public async Task<ResponseViewModel<QuantityViewModel>> CreateAndUpdate(QuantityViewModel QauntityObj)
        {
            ResponseViewModel<QuantityViewModel> ResObj = new ResponseViewModel<QuantityViewModel>();
            try
            {
                if (QauntityObj != null)
                {
                    bool IsNew = false;
                    InventoryDBContext dbContext = new InventoryDBContext(await connMethod.GetConnectionString(QauntityObj.ConnectionString));
                    /*Check if already exists*/
                    Brand_Quantity_Time_Received QuantityDBObj = dbContext.Brand_Quantity_Time_Received.Where(x => x.Inventory_Id == QauntityObj.Brand_Id).FirstOrDefault();
                    if (QuantityDBObj == null)
                    {
                        QuantityDBObj = new Brand_Quantity_Time_Received();
                        IsNew = true;
                    }
                    QuantityDBObj.Brand_Id = QauntityObj.Brand_Id;
                    QuantityDBObj.Quantity = QauntityObj.Quantity;
                    QuantityDBObj.Time_Received = QauntityObj.Time_Received;
                    if (IsNew)
                    {
                        await dbContext.Brand_Quantity_Time_Received.AddAsync(QuantityDBObj);
                    }
                    await dbContext.SaveChangesAsync();
                    QauntityObj.Inventory_Id = QuantityDBObj.Inventory_Id;
                    ResObj.IsSuccess = true;
                    List<QuantityViewModel> QuantityList = new List<QuantityViewModel>();
                    QuantityList.Add(QauntityObj);
                    ResObj.Data = QuantityList;
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
        public async Task<ResponseViewModel<QuantityViewModel>> Delete(QuantityViewModel QuantityObj)
        {
            ResponseViewModel<QuantityViewModel> ResObj = new ResponseViewModel<QuantityViewModel>();
            try
            {
                if (QuantityObj != null)
                {
                    InventoryDBContext dbContext = new InventoryDBContext(await connMethod.GetConnectionString(QuantityObj.ConnectionString));
                    /*Check if already exists*/
                    Brand_Quantity_Time_Received QuantityDBObj = dbContext.Brand_Quantity_Time_Received.Where(x => x.Inventory_Id == QuantityObj.Inventory_Id).FirstOrDefault();
                    if (QuantityDBObj != null)
                    {
                        dbContext.Brand_Quantity_Time_Received.Remove(QuantityDBObj);
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
