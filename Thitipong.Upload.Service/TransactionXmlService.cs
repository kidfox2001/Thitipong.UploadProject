using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Thitipong.Upload.Model;

namespace Thitipong.Upload.Service
{
    public class TransactionXmlService : TransactionService
    {

        public TransactionXmlService(App app) : base(app)
        {
        }

        public override void ImportFile(string filePath)
        {
            var datas = ConvertXml(filePath);
            if (datas != null)
            {

                foreach (var item in datas)
                {
                    app.Transactions.AddAsync(item);
                }

                app.SaveChangesAsync();
            }
        }

        private List<Transaction> ConvertXml(string filePath)
        {
            Func<string, string> ConvertStatus = (i) =>
            {
                if (i == "Approved")
                {
                    return "A";
                }
                else if (i == "Rejected")
                {
                    return "R";
                }
                else if (i == "Done")
                {
                    return "D";
                }
                else
                {
                    throw new Exception("ConvertStatus fail");
                }
            };

            XDocument xDocument = XDocument.Load(filePath);
            List<Transaction> datas = xDocument.Descendants("Transactions").Elements("Transaction").Select
                (p => new Transaction()
                {
                    TransactionId = p.Attribute("id").Value,
                    TransactionDate = DateTime.ParseExact(p.Element("TransactionDate").Value,
                         "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    TransactionStatus = ConvertStatus(p.Element("Status").Value),
                    CurrencyCode = p.Element("PaymentDetails").Element("CurrencyCode").Value,
                    Amount = Decimal.Parse(p.Element("PaymentDetails").Element("Amount").Value),
                }).ToList();

            //foreach (var item in xDocument.Descendants("Transactions").Elements("Transaction"))
            //{
            //    var x = item.Attribute("id").Value;
            //}

            return datas;
        }

    }
}
