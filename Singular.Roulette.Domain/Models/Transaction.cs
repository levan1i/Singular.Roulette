using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Domain.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; } 
        public int TransactionTypeId { get; set; }    
        public long FromAccountId { get; set; } 
        public long ToAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public long? ParentTransactionId { get; set; }
        public int TransactionStatusCode { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

    }
}
