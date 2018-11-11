using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.Models
{
    public class OrderViewModel
    {
        public long OrderId { get; set; }
        public string Date { get; set; }
        public string Distributor { get; set; }
        public string IsDispatched { get; set; }
        public int TotalItems { get; set; }
        public int TotalItemsPending { get; set; }
        public string ProcessOrder { get; set; }
        public string ViewOrder { get; set; }
    }

    public class OrderDetailViewModel
    {
        public long OrderId { get; set; }
        public string Date { get; set; }
        public string Distributor { get; set; }
        public int TotalItems { get; set; }

        public List<OrderDetailModel> OrderDetailList { get; set; }
        public OrderDetailViewModel()
        {
            OrderDetailList = new List<OrderDetailModel>();
        }
    }

    public class OrderDetailModel
    {
        public long OrderDetailId { get; set; }
        public int ItemId { get; set; }
        public string Item { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public bool IsDispatched { get; set; } 
        public string AddedOn { get; set; }
    }

    public class AddItemToOrderModel
    {
        public long OrderId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public string Item { get; set; }

        public string Category { get; set; }
        public long OrderDetailId { get; set; }
        [Required]
        public int DistributorId { get; set; }
        [Required]
        public int Quantity { get; set; }
         
    }

}