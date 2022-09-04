using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions.Dtos
{
    public class BetResult
    {
        public BetResult(int winingNumber, decimal wonAmount)
        {
            WiningNumber = winingNumber;
            WonAmount = wonAmount;
            Currency = "USD";
            Dimention = "¢";


        }

        public int WiningNumber { get; set; }   
        public decimal WonAmount { get; set; }
        public string Currency { get; set; }    
        public string Dimention { get; set; }
        
    }
}
