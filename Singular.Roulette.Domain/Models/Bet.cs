using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Domain.Models
{
    public class Bet
    {
        public long BetId { get; set; }
        public long SpinId { get; set; }  
        public decimal WonAmount { get; set; }  
        public decimal BetAmount { get; set; }  
        public string BetStringJson { get; set;}    
  
        public long UserId { get; set; }    

        public virtual Spin Spin { get; set; }


    }
}
