using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Models
{
    public class TransactionStatus
    {
        public int TransactionStatusCode { get; set; }    
        public string Description { get; set; }
        public bool isFinished { get; set; }
        public bool isFailed { get; set; }
    }
}
