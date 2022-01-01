using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class SalesOrderDetail
    {
        public int SalesOrderDetailId { get; set; }
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public int SalesQuantity { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual SalesOrderHeader SalesOrder { get; set; }
    }
}
