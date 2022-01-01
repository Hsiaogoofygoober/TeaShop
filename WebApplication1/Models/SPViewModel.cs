using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SPViewModel
    {
        public PurchaseOrderHeader PohVM { get; set; }

        public PurchaseOrderDetail PodVM { get; set; }

        public Supplier SVM { get; set; }

        public Product PVM { get; set; }

        
    }
}
