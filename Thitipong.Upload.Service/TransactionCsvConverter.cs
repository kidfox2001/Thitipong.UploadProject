using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Thitipong.Upload.Model;

namespace Thitipong.Upload.Service
{
   public class TransactionCsvConverter: IFileConverter<Transaction> 
    {

        private string _pathFileName;

        public TransactionCsvConverter(string pathFileName)
        {
            _pathFileName = pathFileName;
        }

        public IEnumerable<Transaction> Convert()
        {
            var extension = Path.GetExtension(_pathFileName);
            if (extension.ToLower() != "csv")
            {
                return null;
            }

            List<Transaction> datas = File.ReadAllLines(_pathFileName)
                                          .Select(line => FromCsv(line))
                                          .ToList();

            return datas;
        }

        public static Transaction FromCsv(string csvLine)
        {
            if (csvLine == null) return null;

            Func<string, string> ConvertStatus = (i) =>
                   {
                       if (i == "Approved")
                       {
                           return "A";
                       }
                       else if (i == "Failed")
                       {
                           return "R";
                       }
                       else 
                       {
                           return "D";
                       }
                   };

            string[] values = csvLine.Split(',');
            return new Transaction()
            {
                TransactionId = values[0],
                Amount = decimal.Parse( values[1]),
                CurrencyCode = values[2],
                TransactionDate = DateTime.ParseExact(values[3],
                    "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                TransactionStatus = ConvertStatus(values[4]) ,
            };

        }

     
    }
}
