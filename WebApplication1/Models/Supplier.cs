using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    }
}
