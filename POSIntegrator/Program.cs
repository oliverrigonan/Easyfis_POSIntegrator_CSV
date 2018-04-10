using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POSIntegrator
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 i = 0;
            String apiUrlHost = "localhost:2651";
            foreach (var arg in args)
            {
                if (i == 0) { apiUrlHost = arg; }
                i++;
            }

            Console.WriteLine("===========================================");
            Console.WriteLine("Innosoft CSV Uploader - Version: 1.20180321");
            Console.WriteLine("===========================================");

            Console.WriteLine();

            Controllers.TrnStockInController stockIn = new Controllers.TrnStockInController();
            Controllers.TrnSalesInvoiceController salesInvoice = new Controllers.TrnSalesInvoiceController();

            while (true)
            {
                stockIn.SendStockInCSVFile(apiUrlHost);
                salesInvoice.SendSalesInvoiceCSVFile(apiUrlHost);
                Thread.Sleep(5000);
            }
        }
    }
}
