using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Domain.Models
{
    public class UserAccount
    {
        public long AccountId1 { get; set; } 
        public long UserId1 { get; set; }
        public virtual User User { get; set; } 
        public virtual Account Account { get; set; }

    }
}
