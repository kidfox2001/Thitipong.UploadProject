using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Thitipong.Upload.Model
{
  public  class Transaction
    {

        [Key]
        [StringLength(50)]
        public string TransactionId { get; set; }

        [Required]
        public decimal Amount { get; set; }


        [Required]
        public string CurrencyCode { get; set; }
        
        
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(1)]
        public string TransactionStatus { get; set; }

       

    }

    public enum TransactionEnum
    {
        Approved,
            yyy

    }

}
