using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Product
    {
        public Product()
        {
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
            Stocks = new HashSet<Stock>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPicture { get; set; }
        public string ProductDescription { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
