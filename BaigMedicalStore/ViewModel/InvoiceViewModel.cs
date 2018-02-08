using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.Models
{

    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public DateTime AddedOn { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AmountRecieved { get; set; }
        public decimal Discount { get; set; }
        public int NoOfItems { get; set; }

        public string ViewInvoiceUrl { get; set; }
        public List<InvoiceDetailViewModel> InvDetList { get; set; }
        public int InvoiceNo { get; internal set; }

        public InvoiceViewModel()
        {
            InvDetList = new List<InvoiceDetailViewModel>();
        }
    }

    public class InvoiceDetailViewModel
    {
        public int InvoiceDetailId { get; set; }
        public int Item { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
    }
}