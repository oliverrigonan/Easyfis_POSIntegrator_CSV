using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIntegrator.Entities
{
    class TrnSalesInvoice
    {
        public String BranchCode { get; set; }
        public String SIDate { get; set; }
        public String CustomerManualArticleCode { get; set; }
        public String Term { get; set; }
        public String Remarks { get; set; }
        public String ManualSINumber { get; set; }
        public String PreparedBy { get; set; }
        public String CreatedBy { get; set; }
    }
}
