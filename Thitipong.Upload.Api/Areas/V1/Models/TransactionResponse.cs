using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitipong.Upload.Model;

namespace Thitipong.Upload.Api.Areas.V1.Models
{

    public class TransactionResponse
    {
        public string Id { get; set; }

        public string Payment { get; set; }

        public string Status { get; set; }

        public static TransactionResponse FromModel(Transaction item)
        {
            return new TransactionResponse()
            {
                Id = item.TransactionId,
                Payment = $"{String.Format(item.Amount.ToString(), "###,###,###,##0.00")} {item.CurrencyCode}",
                Status = item.TransactionStatus,
            };

        }

    }
}
