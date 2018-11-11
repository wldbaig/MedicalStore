using BaigMedicalStore.Common;
using BaigMedicalStore.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using static BaigMedicalStore.Common.Enumeration;

namespace BaigMedicalStore.BusinessLogic
{
    public class OrderBusinessLogic : BusinessLogicBase
    {
        public DataSourceResult GetOrders(DataSourceRequest request)
        {
            Hashtable fltr = new Hashtable();
            Common.CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

            ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);

            var distribut = fltr.ContainsKey("Distributor") ? Convert.ToInt32(fltr["Distributor"].ToString()) : 0;
            DateTime? orderDate = (fltr.ContainsKey("Date") && fltr["Date"] != null) ? Convert.ToDateTime(fltr["Date"]) : (DateTime?)null;

            var queryResult = db.Order_Get(orderDate, distribut, request.Page, request.PageSize, objparam).ToList()
                .Select(i => new OrderViewModel()
                {
                    OrderId = i.OrderId,
                    Date = i.ModifiedDate.Date.ToString("dd MMM yyy"),
                    Distributor = i.Distributor,
                    IsDispatched = i.IsDispatched,
                    TotalItems = (int)i.TotalItems,
                    TotalItemsPending = (int)i.TotalItemsPending,
                    ProcessOrder = CryptographyUtility.GetEncryptedQueryString(new { orderId = i.OrderId }),
                    ViewOrder = CryptographyUtility.GetEncryptedQueryString(new { orderId = i.OrderId })
                });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = queryResult;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

        public OrderDetailViewModel GetOrderDetail(int orderId, bool forProcessing = false)
        {
            var model = new OrderDetailViewModel();

            var order = db.Orders.Find(orderId);
            if (order != null)
            {
                model.OrderId = order.OrderId;
                model.Date = order.ModifiedDate.Date.ToString("dd MMM yyyy");
                model.Distributor = order.Distributor.Name;
                if (forProcessing)
                {
                    model.TotalItems = order.OrderDetails.Count(c => c.IsDispatched == false);
                    model.OrderDetailList = order.OrderDetails.Where(c => c.IsDispatched == false).Select(c => new OrderDetailModel()
                    {
                        AddedOn = c.AddedOn.Date.ToString("dd MMM yyyy"),
                        IsDispatched = c.IsDispatched,
                        Category = c.Item.Category.Name,
                        Item = c.Item.Name,
                        OrderDetailId = c.OrderDetailId,
                        ItemId = c.ItemId,
                        Quantity = c.Quantity
                    }).ToList();
                }
                else
                {
                    model.TotalItems = order.OrderDetails.Count();
                    model.OrderDetailList = order.OrderDetails.Select(c => new OrderDetailModel()
                    {
                        AddedOn = c.AddedOn.Date.ToString("dd MMM yyyy"),
                        IsDispatched = c.IsDispatched,
                        Category = c.Item.Category.Name,
                        Item = c.Item.Name,
                        OrderDetailId = c.OrderDetailId,
                        ItemId = c.ItemId,
                        Quantity = c.Quantity
                    }).ToList();
                }
            }
            return model;
        }

        public AddItemToOrderModel GetItemFromOrder(int itemId)
        {
            AddItemToOrderModel obj = new AddItemToOrderModel();
            var item = db.Items.Find(itemId);
            obj.ItemId = item.ItemId;
            obj.Item = item.Name;
            obj.Category = item.Category.Name;
            if (db.Orders.Any(c => c.DistributorId == item.DistributorId && c.IsDispatched == false))
            {
                var order = db.Orders.FirstOrDefault(c => c.DistributorId == item.DistributorId && c.IsDispatched == false);

                if (order.OrderDetails.Any(c => c.ItemId == item.ItemId && c.IsDispatched == false))
                {
                    var odrdet = order.OrderDetails.FirstOrDefault(c => c.ItemId == item.ItemId && c.IsDispatched == false);
                    obj.OrderId = odrdet.OrderId;
                    obj.OrderDetailId = odrdet.OrderDetailId;
                    obj.Quantity = odrdet.Quantity;
                    // obj.ItemId = odrdet.ItemId;
                    obj.DistributorId = (int)item.DistributorId;
                }
                else
                {
                    obj.OrderId = order.OrderId;
                    obj.OrderDetailId = 0;
                    obj.Quantity = 1;
                    //   obj.ItemId = itemId;
                    obj.DistributorId = (int)item.DistributorId;
                }
            }
            else
            {
                obj.OrderId = 0;
                obj.OrderDetailId = 0;
                obj.Quantity = 1;
                //obj.ItemId = itemId;
                obj.DistributorId = (int)item.DistributorId;
            }

            return obj;

        }

        public void AddItemToOrder(AddItemToOrderModel model)
        {
            var item = db.Items.Find(model.ItemId);
            if (model.OrderId == 0)
            {
                Order o = new Order();
                o.IsDispatched = false;
                o.ModifiedDate = DateTime.Now.Date;
                o.CreatedBy = (int)GetLoggedInUserId();
                o.CreatedDate = DateTime.Now.Date;
                o.DistributorId = (int)item.DistributorId;
                db.Orders.Add(o);
                db.SaveChanges();

                OrderDetail od = new OrderDetail();
                od.OrderId = o.OrderId;
                od.ItemId = item.ItemId;
                od.Quantity = model.Quantity;
                od.AddedOn = DateTime.Now.Date;
                od.IsDispatched = false;

                db.OrderDetails.Add(od);
                db.SaveChanges();

            }
            else
            {
                var order = db.Orders.Find(model.OrderId);
                order.ModifiedDate = DateTime.Now.Date;
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                if (model.OrderDetailId == 0)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId = order.OrderId;
                    od.ItemId = item.ItemId;
                    od.Quantity = model.Quantity;
                    od.AddedOn = DateTime.Now.Date;
                    od.IsDispatched = false;

                    db.OrderDetails.Add(od);
                    db.SaveChanges();
                }
                else
                {
                    var od = db.OrderDetails.Find(model.OrderDetailId);
                    od.Quantity = model.Quantity;
                    od.AddedOn = DateTime.Now.Date;

                    db.Entry(od).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

        }

        public void DispatchOrder(int orderId)
        {
            var order = db.Orders.Find(orderId);
            if (!order.IsDispatched && !order.OrderDetails.Any(c => c.IsDispatched == false))
            {
                order.IsDispatched = true;
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DispatchItemInOrder(long orderDetailId)
        {
            var orderDetail = db.OrderDetails.Find(orderDetailId);
            if (!orderDetail.IsDispatched)
            {
                orderDetail.IsDispatched = true;
                db.Entry(orderDetail).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                if (db.OrderDetails.Where(c => c.OrderId == orderDetail.OrderId && c.IsDispatched == false).Count() == 0)
                {
                    var order = db.Orders.Find(orderDetail.OrderId);
                    order.IsDispatched = true;
                    order.ModifiedDate = DateTime.Now.Date;
                    db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

    }
}