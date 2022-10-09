using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Thitipong.Upload.Model;

namespace Thitipong.Upload.Service
{
    public class TransactionService : ServiceBase<Transaction>
    {
        public TransactionService(App app) : base(app)
        {
        }

        public virtual void ImportFile(string filePath)
        {
            if (Path.GetExtension(filePath).ToLower() == ".csv")
            {
                TransactionCsvService csv  = new TransactionCsvService(app);
                csv.ImportFile(filePath);
            }

            if (Path.GetExtension(filePath).ToLower() == ".xml")
            {
                TransactionXmlService xml = new TransactionXmlService(app);
                xml.ImportFile(filePath); 
            }

            //throw new NotImplementedException();
        }

        //public void ImportCsvFile(string filePath)
        //{
        //    try
        //    {
        //        List<Transaction> datas = ConvertCsv(filePath);
        //        if (datas != null)
        //        {
        //            // app.db.BulkInsert<Transaction>(datas);

        //            foreach (var item in datas)
        //            {
        //                app.Transactions.AddAsync(item);
        //            }

        //            app.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }


        //}

        //private List<Transaction> ConvertCsv(string filePath)
        //{
        //    var extension = Path.GetExtension(filePath);
        //    if (extension.ToLower() != ".csv")
        //    {
        //        return null;
        //    }

        //    List<Transaction> datas = File.ReadAllLines(filePath)
        //                                  .Select(line => FromCsvToModel(line))
        //                                  .ToList();

        //    return datas;
        //}

        //private Transaction FromCsvToModel(string csvLine)
        //{
        //    if (csvLine == null) return null;

        //    Func<string, string> ConvertStatus = (i) =>
        //    {
        //        if (i == "Approved")
        //        {
        //            return "A";
        //        }
        //        else if (i == "Failed")
        //        {
        //            return "R";
        //        }
        //        else if (i == "Finished")
        //        {
        //            return "D";
        //        }
        //        else
        //        {
        //            throw new Exception("ConvertStatus fail");
        //        }
        //    };
        //    Func<string, string> CleanString = (i) =>
        //    {
        //        return i.Replace('"', ' ').TrimStart().TrimEnd();
        //    };


        //    Regex rgx1 = new Regex("\"(.*?)\"");
        //    return new Transaction()
        //    {
        //        TransactionId = CleanString(rgx1.Matches(csvLine)[0].Value),
        //        Amount = decimal.Parse(CleanString(rgx1.Matches(csvLine)[1].Value)),
        //        CurrencyCode = CleanString(rgx1.Matches(csvLine)[2].Value),
        //        TransactionDate = DateTime.ParseExact(CleanString(rgx1.Matches(csvLine)[3].Value),
        //          "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
        //        TransactionStatus = ConvertStatus(CleanString(rgx1.Matches(csvLine)[4].Value)),
        //    };

        //}



        //public void ImportXmlFile(string filePath)
        //{
        //    var datas = ConvertXml(filePath);
        //    if (datas != null)
        //    {

        //        foreach (var item in datas)
        //        {
        //            app.Transactions.AddAsync(item);
        //        }

        //        app.SaveChangesAsync();
        //    }

        //}

        //private List<Transaction> ConvertXml(string filePath)
        //{
        //    Func<string, string> ConvertStatus = (i) =>
        //    {
        //        if (i == "Approved")
        //        {
        //            return "A";
        //        }
        //        else if (i == "Rejected")
        //        {
        //            return "R";
        //        }
        //        else if (i == "Done")
        //        {
        //            return "D";
        //        }
        //        else
        //        {
        //            throw new Exception("ConvertStatus fail");
        //        }
        //    };

        //    XDocument xDocument = XDocument.Load(filePath);
        //    List<Transaction> datas = xDocument.Descendants("Transactions").Elements("Transaction").Select
        //        (p => new Transaction()
        //        {
        //            TransactionId = p.Attribute("id").Value,
        //            TransactionDate = DateTime.ParseExact(p.Element("TransactionDate").Value,
        //                 "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
        //            TransactionStatus = ConvertStatus(p.Element("Status").Value),
        //            CurrencyCode = p.Element("PaymentDetails").Element("CurrencyCode").Value,
        //            Amount = Decimal.Parse(p.Element("PaymentDetails").Element("Amount").Value),
        //        }).ToList();

        //    //foreach (var item in xDocument.Descendants("Transactions").Elements("Transaction"))
        //    //{
        //    //    var x = item.Attribute("id").Value;
        //    //}

        //    return datas;
        //}

    }
}
