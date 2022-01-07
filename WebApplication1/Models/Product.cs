using Newtonsoft.Json;
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

        [JsonProperty("ProductId")]
        public int ProductId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPicture { get; set; }
        public string ProductDescription { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
