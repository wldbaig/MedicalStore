using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaigMedicalStore.BusinessLogic;
using BaigMedicalStore.Common;
using BaigMedicalStore.Filters;
using BaigMedicalStore.Models;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace BaigMedicalStore.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrders([DataSourceRequest] DataSourceRequest request)
        {
            OrderBusinessLogic obj = new OrderBusinessLogic();
            DataSourceResult lstItems = obj.GetOrders(request);
            return Json(lstItems, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [DecryptParams]
        public ActionResult Detail(int orderId)
        {
            OrderBusinessLogic objOrderBusinessLogic = new OrderBusinessLogic();
            var model = objOrderBusinessLogic.GetOrderDetail(orderId);
            return View(model);
        }

        [HttpGet]
        [DecryptParams]
        public ActionResult Process(int orderId)
        {
            OrderBusinessLogic objOrderBusinessLogic = new OrderBusinessLogic();
            var model = objOrderBusinessLogic.GetOrderDetail(orderId, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddItemToOrder(AddItemToOrderModel obj)
        {
            MessageModel model = new MessageModel();

            OrderBusinessLogic objOrderBusinessLogic = new OrderBusinessLogic();

            try
            {
                objOrderBusinessLogic.AddItemToOrder(obj);
                model.Message = "Item added in Order successfully";
            }
            catch (System.Exception ex)
            {
                model.Message = "An error has occured while adding Item to Order";
                model.Type = Enumeration.MessageType.Error;
                logger.Error(model.Message, ex);
            }

            var response = new
            {
                //Status = updatedStatus,
                MessageModel = model
            };

            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddToOrderPartial(int itemId = 0)
        {
            OrderBusinessLogic bl = new OrderBusinessLogic();
            var view = bl.GetItemFromOrder(itemId);

            return PartialView("_AddToOrder", view);
        }

        [HttpPut]
        public ActionResult DispatchItemInOrder(long orderDetailId)
        {
            MessageModel model = new MessageModel();

            OrderBusinessLogic objOrderBusinessLogic = new OrderBusinessLogic();

            try
            {
                objOrderBusinessLogic.DispatchItemInOrder(orderDetailId);
                model.Message = "Item dispatch successfully";
            }
            catch (System.Exception ex)
            {
                model.Message = "An error has occured while adding Item to Order";
                model.Type = Enumeration.MessageType.Error;
                logger.Error(model.Message, ex);
            }

            var response = new
            {
                OrderDetailId = orderDetailId,
                MessageModel = model
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}