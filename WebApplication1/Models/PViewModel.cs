using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PViewModel
    {
        public IList<PurchaseOrderDetail> PDVM { get; set; }

        public PurchaseOrderHeader PHVM { get; set; }
    }
}
