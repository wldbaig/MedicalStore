using BaigMedicalStore.Common;
using BaigMedicalStore.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.BusinessLogic
{
    public class InvoiceBusinessLogic : BusinessLogicBase
    {

        public DataSourceResult GetInvoices(DataSourceRequest request)
        {
            Hashtable fltr = new Hashtable();
            Common.CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

            string sortBy = string.Empty;
            if (request.Sorts.Any())
                sortBy = request.Sorts[0].Member + " " + request.Sorts[0].SortDirection;

            ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);

            int invoice = 0;
            DateTime date = new DateTime();
            if (fltr.ContainsKey("Date"))
            {
                DateTime.TryParse(fltr["Date"].ToString(), out date);
            }

            if (fltr.ContainsKey("InvoiceNo"))
            {
                int.TryParse(fltr["InvoiceNo"].ToString(), out invoice);
            }

            var queryResult = db.Invoice_Get(date, invoice, request.Page, request.PageSize, sortBy, objparam).ToList()
                .Select(m => new InvoiceViewModel()
                {
                    InvoiceId = m.InvoiceId,
                    AddedOn = m.AddedOn,
                    AmountRecieved = m.AmountRecieved,
                    Discount = m.DiscountAmount,
                    NoOfItems = m.NoOfItems,
                    TotalPrice = m.TotalPrice,
                    ViewInvoiceUrl = CryptographyUtility.GetEncryptedQueryString(new { InvoiceId = m.InvoiceId })
                });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = queryResult;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

        public void SaveInvoice(InvoiceViewModel model)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var inv = new Invoice();
                if (model.InvoiceId > 0)
                {
                    inv = db.Invoices.FirstOrDefault(c => c.InvoiceId == model.InvoiceId);
                }
                 
                inv.AddedOn = DateTime.UtcNow;
                inv.AmountRecieved = model.AmountRecieved;
                inv.CreatedBy = (int)GetLoggedInUserId();
                inv.DiscountAmount = model.Discount;
                inv.TotalPrice = model.TotalPrice;

                db.Invoices.Add(inv);
                db.SaveChanges();

                foreach (var item in model.InvDetList)
                {
                    var invDet = new InvoiceDetail();
                    invDet.InvoiceId = inv.InvoiceId;
                    invDet.ItemId = item.Item;
                    invDet.Quantity = item.Quantity;
                    invDet.TotalPrice = item.TotalPrice;
                    invDet.UnitPrice = item.UnitPrice;
                    invDet.Discount = item.Discount;

                    db.InvoiceDetails.Add(invDet);
                    db.SaveChanges();
                }

                inv.NoOfItems = inv.InvoiceDetails.Count();
                db.Entry(inv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                dbContextTransaction.Commit();
            }
        }

    }
}