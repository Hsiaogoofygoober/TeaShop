using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SSViewModel
    {
        public SalesOrderHeader SOHVM { get; set; }

        public SalesOrderDetail SODVM { get; set; }

        public Product PVM { get; set; }
    }
}
