using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Domain.Models
{
    public class Account
    {
        public long Id { get; set; }
        public int TypeId { get; set; }    
        public string Currency { get; set; }

       public virtual AccountType Type { get; set; }
    }
}
