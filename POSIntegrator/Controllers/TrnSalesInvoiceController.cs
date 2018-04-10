using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

namespace POSIntegrator.Controllers
{
    class TrnSalesInvoiceController
    {
        // ==================================
        // Send Sales Invoice Header CSV File
        // ==================================
        public void SendSalesInvoiceCSVFile(String apiUrlHost)
        {
            try
            {
                // ==========
                // File Paths
                // ==========
                String headerFilesPath = "d:/innosoft/csv/SI/headers";
                List<String> headerFiles = new List<String>(Directory.EnumerateFiles(headerFilesPath));

                if (headerFiles.Any())
                {
                    foreach (var headerFile in headerFiles)
                    {
                        // =====================================
                        // Read File Streams and Process Objects
                        // =====================================
                        List<Entities.TrnSalesInvoice> listSalesInvoices = new List<Entities.TrnSalesInvoice>();
                        using (StreamReader sr = new StreamReader(headerFile))
                        {
                            sr.ReadLine();
                            while (sr.Peek() != -1)
                            {
                                List<String> lineValues = sr.ReadLine().Split(',').ToList();
                                var branchCode = lineValues[0];
                                var SIDate = lineValues[1];
                                var documentReference = lineValues[2];
                                var customerCode = lineValues[3];
                                var term = lineValues[4];
                                var remarks = lineValues[5];
                                var manualSINumber = lineValues[6];

                                listSalesInvoices.Add(new Entities.TrnSalesInvoice
                                {
                                    BranchCode = branchCode,
                                    SIDate = SIDate,
                                    DocumentReference = documentReference,
                                    CustomerCode = customerCode,
                                    Term = term,
                                    Remarks = remarks,
                                    ManualSINumber = manualSINumber
                                });
                            }
                        }

                        // ==================================
                        // HTTP Web Request (Process Request)
                        // ==================================
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + apiUrlHost + "/api/add/CSVIntegrator/salesInvoice");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        // ===================================
                        // Process Json Objects (Process Data)
                        // ===================================
                        var jsonSerialiser = new JavaScriptSerializer();
                        var serializedJson = jsonSerialiser.Serialize(listSalesInvoices);
                        List<Entities.TrnSalesInvoice> deserializedJson = jsonSerialiser.Deserialize<List<Entities.TrnSalesInvoice>>(serializedJson);
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            Console.WriteLine("Sending SI Header File [ " + headerFile + " ]...");
                            streamWriter.Write(new JavaScriptSerializer().Serialize(deserializedJson));
                        }

                        // ====================================
                        // HTTP Web Response (Process Response)
                        // ====================================
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            if (result != null)
                            {
                                Console.WriteLine(result.Replace("\"", ""));
                                Console.WriteLine();

                                File.Delete(headerFile);
                            }
                        }
                    }
                }
                else
                {
                    String itemFilesPath = "d:/innosoft/csv/SI/lines";
                    List<String> itemFiles = new List<String>(Directory.EnumerateFiles(itemFilesPath));

                    if (itemFiles.Any())
                    {
                        foreach (var itemFile in itemFiles)
                        {
                            // =====================================
                            // Read File Streams and Process Objects
                            // =====================================
                            List<Entities.TrnSalesInvoiceItem> listSalesInvoiceItems = new List<Entities.TrnSalesInvoiceItem>();
                            using (StreamReader sr = new StreamReader(itemFile))
                            {
                                sr.ReadLine();
                                while (sr.Peek() != -1)
                                {
                                    List<String> lineValues = sr.ReadLine().Split(',').ToList();
                                    var branchCode = lineValues[0];
                                    var manualSINumber = lineValues[1];
                                    var itemCode = lineValues[2];
                                    var particulars = lineValues[3];
                                    var unit = lineValues[4];
                                    var quantity = lineValues[5];
                                    var price = lineValues[6];
                                    var discount = lineValues[7];
                                    var discountAmount = lineValues[8];
                                    var netPrice = lineValues[9];
                                    var amount = lineValues[10];
                                    var VAT = lineValues[11];
                                    var VATAmount = lineValues[12];
                                    var SalesTimeStamp = lineValues[13];

                                    listSalesInvoiceItems.Add(new Entities.TrnSalesInvoiceItem
                                    {
                                        BranchCode = branchCode,
                                        ManualSINumber = manualSINumber,
                                        ItemCode = itemCode,
                                        Particulars = particulars,
                                        Unit = unit,
                                        Quantity = Convert.ToDecimal(quantity),
                                        Price = Convert.ToDecimal(price),
                                        Discount = discount,
                                        DiscountAmount = Convert.ToDecimal(discountAmount),
                                        NetPrice = Convert.ToDecimal(netPrice),
                                        Amount = Convert.ToDecimal(amount),
                                        VAT = VAT,
                                        VATAmount = Convert.ToDecimal(VATAmount),
                                        SalesItemTimeStamp = SalesTimeStamp
                                    });
                                }
                            }

                            // ==================================
                            // HTTP Web Request (Process Request)
                            // ==================================
                            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + apiUrlHost + "/api/add/CSVIntegrator/salesInvoiceItem");
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";

                            // ===================================
                            // Process Json Objects (Process Data)
                            // ===================================
                            var jsonSerialiser = new JavaScriptSerializer();
                            var serializedJson = jsonSerialiser.Serialize(listSalesInvoiceItems);
                            List<Entities.TrnSalesInvoiceItem> deserializedJson = jsonSerialiser.Deserialize<List<Entities.TrnSalesInvoiceItem>>(serializedJson);
                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                Console.WriteLine("Sending SI Line File [ " + itemFile + " ]...");
                                streamWriter.Write(new JavaScriptSerializer().Serialize(deserializedJson));
                            }

                            // ====================================
                            // HTTP Web Response (Process Response)
                            // ====================================
                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                if (result != null)
                                {
                                    Console.WriteLine(result.Replace("\"", ""));
                                    Console.WriteLine();

                                    File.Delete(itemFile);
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException we)
            {
                var resp = new StreamReader(we.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Easyfis says: " + resp.Replace("\"", ""));
                Console.WriteLine();
            }
        }
    }
}
