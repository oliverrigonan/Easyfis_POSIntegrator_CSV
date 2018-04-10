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
        public String DocumentReference { get; set; }
        public String CustomerCode { get; set; }
        public String Term { get; set; }
        public String Remarks { get; set; }
        public String ManualSINumber { get; set; }
    }

    class TrnSalesInvoiceItem
    {
        public String BranchCode { get; set; }
        public String ManualSINumber { get; set; }
        public String ItemCode { get; set; }
        public String Particulars { get; set; }
        public String Unit { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Price { get; set; }
        public String Discount { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Decimal NetPrice { get; set; }
        public Decimal Amount { get; set; }
        public String VAT { get; set; }
        public Decimal VATAmount { get; set; }
        public String SalesItemTimeStamp { get; set; }
    }
}
