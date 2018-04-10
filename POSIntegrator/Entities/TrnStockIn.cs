using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIntegrator.Entities
{
    class TrnStockIn
    {
        public String BranchCode { get; set; }
        public String INDate { get; set; }
        public String Particulars { get; set; }
        public String ManualINNumber { get; set; }
        public String IsProduced { get; set; }
        public String CreatedBy { get; set; }
    }

    class TrnStockInItem
    {
        public String BranchCode { get; set; }
        public String ManualINNumber { get; set; }
        public String ItemCode { get; set; }
        public String Particulars { get; set; }
        public String Unit { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Cost { get; set; }
        public Decimal Amount { get; set; }
    }
}
