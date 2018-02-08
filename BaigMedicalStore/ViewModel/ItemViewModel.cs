using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.Models
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }

       // [Remote("IsItemNameUnique", "Item", HttpMethod ="POST", ErrorMessage = "Item already exists.")]
        [Required]
        public string Name { get; set; }
         
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }

        [Display(Name = "Distributor")]
        public int DistributorId { get; set; }
        public string DistributorName { get; set; }

        [Required]
        [Display(Name = "T-P")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Display(Name = "S-P")]
        public decimal? SalePrice { get; set; }
         
        [Display(Name = "U-P")]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Display(Name = "Pieces / Packing")]
        public int? PiecesInPaking { get; set; }

        public string Code { get; set; }

        public string Formula { get; set; }
        public int TotalStock { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public string EditItemUrl { get; internal set; }
    }
}