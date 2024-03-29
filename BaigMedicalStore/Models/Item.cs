//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaigMedicalStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int ItemId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> LocationId { get; set; }
        public int CategoryId { get; set; }
        public decimal PurchasePrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> PiecesInPacking { get; set; }
        public string Formula { get; set; }
        public Nullable<int> DistributorId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<int> TotalStock { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Distributor Distributor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
