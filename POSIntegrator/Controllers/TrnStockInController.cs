using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

namespace POSIntegrator.Controllers
{
    class TrnStockInController
    {
        // ==============================
        // Send Stock In Header CSV File
        // =============================
        public void SendStockInCSVFile(String apiUrlHost)
        {
            try
            {
                // ==========
                // File Paths
                // ==========
                String headerFilesPath = "d:/innosoft/csv/IN/headers";
                List<String> headerFiles = new List<String>(Directory.EnumerateFiles(headerFilesPath));

                if (headerFiles.Any())
                {
                    foreach (var headerFile in headerFiles)
                    {
                        // =====================================
                        // Read File Streams and Process Objects
                        // =====================================
                        List<Entities.TrnStockIn> listStockIns = new List<Entities.TrnStockIn>();
                        using (StreamReader sr = new StreamReader(headerFile))
                        {
                            sr.ReadLine();
                            while (sr.Peek() != -1)
                            {
                                List<String> lineValues = sr.ReadLine().Split(',').ToList();
                                var branchCode = lineValues[0];
                                var INDate = lineValues[1];
                                var particulars = lineValues[2];
                                var manualINNumber = lineValues[3];
                                var isProduced = lineValues[4];

                                listStockIns.Add(new Entities.TrnStockIn
                                {
                                    BranchCode = branchCode,
                                    INDate = INDate,
                                    Particulars = particulars,
                                    ManualINNumber = manualINNumber,
                                    IsProduced = isProduced
                                });
                            }
                        }

                        // ==================================
                        // HTTP Web Request (Process Request)
                        // ==================================
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + apiUrlHost + "/api/add/CSVIntegrator/stockIn");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        // ===================================
                        // Process Json Objects (Process Data)
                        // ===================================
                        var jsonSerialiser = new JavaScriptSerializer();
                        var serializedJson = jsonSerialiser.Serialize(listStockIns);
                        List<Entities.TrnStockIn> deserializedJson = jsonSerialiser.Deserialize<List<Entities.TrnStockIn>>(serializedJson);
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            Console.WriteLine("Sending IN Header File [ " + headerFile + " ]...");
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
                    String itemFilesPath = "d:/innosoft/csv/IN/items";
                    List<String> itemFiles = new List<String>(Directory.EnumerateFiles(itemFilesPath));

                    if (itemFiles.Any())
                    {
                        foreach (var itemFile in itemFiles)
                        {
                            // =====================================
                            // Read File Streams and Process Objects
                            // =====================================
                            List<Entities.TrnStockInItem> listStockInItems = new List<Entities.TrnStockInItem>();
                            using (StreamReader sr = new StreamReader(itemFile))
                            {
                                sr.ReadLine();
                                while (sr.Peek() != -1)
                                {
                                    List<String> lineValues = sr.ReadLine().Split(',').ToList();
                                    var branchCode = lineValues[0];
                                    var manualINNumber = lineValues[1];
                                    var itemCode = lineValues[2];
                                    var Particulars = lineValues[3];
                                    var Quantity = lineValues[4];
                                    var Unit = lineValues[5];
                                    var Cost = lineValues[6];
                                    var Amount = lineValues[7];

                                    listStockInItems.Add(new Entities.TrnStockInItem
                                    {
                                        BranchCode = branchCode,
                                        ManualINNumber = manualINNumber,
                                        ItemCode = itemCode,
                                        Particulars = Particulars,
                                        Unit = Unit,
                                        Quantity = Convert.ToDecimal(Cost),
                                        Cost = Convert.ToDecimal(Cost),
                                        Amount = Convert.ToDecimal(Amount),
                                    });
                                }
                            }

                            // ==================================
                            // HTTP Web Request (Process Request)
                            // ==================================
                            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + apiUrlHost + "/api/add/CSVIntegrator/stockInItem");
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";

                            // ===================================
                            // Process Json Objects (Process Data)
                            // ===================================
                            var jsonSerialiser = new JavaScriptSerializer();
                            var serializedJson = jsonSerialiser.Serialize(listStockInItems);
                            List<Entities.TrnStockInItem> deserializedJson = jsonSerialiser.Deserialize<List<Entities.TrnStockInItem>>(serializedJson);
                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                Console.WriteLine("Sending IN Item File [ " + itemFile + " ]...");
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
