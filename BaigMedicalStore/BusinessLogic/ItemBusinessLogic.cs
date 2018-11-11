﻿using BaigMedicalStore.Common;
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
    public class ItemBusinessLogic : BusinessLogicBase
    {
        public ItemViewModel GetItemViewModel(int itemId, ItemViewModel viewModel = null)
        {
            viewModel = viewModel ?? new ItemViewModel();

            var item = db.Items.FirstOrDefault(c => c.ItemId == itemId);
            if (item != null)
            {
                Transform.FromObjectToObject(item, viewModel);

                viewModel.LocationName = (item.LocationId == null || item.LocationId == 0) ? "" : db.Locations.FirstOrDefault(c => c.LocationId == item.LocationId).Name;
                viewModel.CategoryName = item.CategoryId == 0 ? "" : db.Categories.FirstOrDefault(c => c.CategoryId == item.CategoryId).Name;
                viewModel.ManufacturerName = item.Manufacturer?.Name;
                viewModel.DistributorName = item.Distributor?.Name;
                viewModel.PiecesInPaking = item.PiecesInPacking;
            }

            return viewModel;
        }

        public DataSourceResult GetItems(DataSourceRequest request)
        {
            Hashtable fltr = new Hashtable();
            Common.CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

            string sortBy = string.Empty;
            if (request.Sorts.Any())
                sortBy = request.Sorts[0].Member + " " + request.Sorts[0].SortDirection;

            ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);

            var name = fltr.ContainsKey("Name") ? fltr["Name"].ToString() : null;
            var distribut = fltr.ContainsKey("Distributor") ? fltr["Distributor"].ToString() : null;
            var manufact = fltr.ContainsKey("Manufacturer") ? fltr["Manufacturer"].ToString() : null;
            var locat = fltr.ContainsKey("Location") ? fltr["Location"].ToString() : null;

            var categ = fltr.ContainsKey("Category") ? Convert.ToInt32(fltr["Category"]) : 0;
             
            var queryResult = db.Item_Get(name, distribut, manufact, locat, categ, request.Page, request.PageSize, sortBy, objparam).ToList()
                .Select(i => new ItemViewModel()
                {
                    ManufacturerId = i.ManufacturerId ?? 0,
                    ManufacturerName = i.Manufacturer ?? "",
                    Name = i.Name,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category,
                    DistributorId = i.DistributorId ?? 0,
                    DistributorName = i.Distributor ?? "",
                    Code = i.Code ?? "",
                    Formula = i.Formula ?? "",
                    ItemId = i.ItemId,
                    LocationId = i.LocationId ?? 0,
                    Status = (i.IsActive) ? "Active" : "Inactive",
                    LocationName = i.Location ?? "",
                    PiecesInPaking = i.PiecesInPacking ?? 0,
                    PurchasePrice = i.PurchasePrice,
                    SalePrice = i.SalePrice ?? 0,
                    UnitPrice = i.UnitPrice ?? 0,
                    TotalStock = (int)i.TotalStock,
                    IsActive = i.IsActive,
                    EditItemUrl = CryptographyUtility.GetEncryptedQueryString(new { itemId = i.ItemId })
                });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = queryResult;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

        public void SaveItem(ItemViewModel model)
        {
            var item = new Item();
            if (model.ItemId > 0)
            {
                item = db.Items.FirstOrDefault(c => c.ItemId == model.ItemId);
            }

            item.Name = model.Name;
            item.CategoryId = model.CategoryId;
            item.LocationId = model.LocationId;
            item.ManufacturerId = model.ManufacturerId;
            item.DistributorId = model.DistributorId;
            item.Formula = model.Formula;
            item.Code = model.Code;
            item.IsActive = true;
            item.PiecesInPacking = model.PiecesInPaking;
            item.PurchasePrice = model.PurchasePrice;
            item.SalePrice = model.SalePrice;
            item.UnitPrice = model.SalePrice / model.PiecesInPaking;
            item.TotalStock = model.TotalStock;
            if (item.ItemId == 0)
            {
                item.CreatedBy = (int)GetLoggedInUserId();
                item.CreatedOn = DateTime.UtcNow;

                db.Items.Add(item);
            }
            else
            {
                item.UpdatedBy = (int)GetLoggedInUserId();
                item.UpdatedOn = DateTime.UtcNow;

                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }

        public void ChangeItemStatus(int itemId)
        {
            ToggleActiveStatus<Item>(sub => sub.ItemId == itemId);
        }

        public bool IsItemnameUnique(string name)
        {
            var IsUnique = !db.Items.Any(c => c.Name.Trim().ToLower() == name.Trim().ToLower());
            return IsUnique;
        }
    }
}