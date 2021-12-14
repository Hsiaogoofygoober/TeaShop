using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class SalesOrderHeader
    {
        public SalesOrderHeader()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        public int SalesOrderId { get; set; }
        public string Customer { get; set; }
        public decimal SalesTotal { get; set; }
        public DateTime SalesDate { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
