using System;
using System.Collections.Generic;

using System.Text;

namespace Singular.Roulette.Domain.Models
{
    public class User
    {  
     
        public long UserId { get; set; }    
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
