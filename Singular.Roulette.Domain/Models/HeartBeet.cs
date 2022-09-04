using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Models
{
    public class HeartBeet
    {
        public string SessionId { get; set; } 
        public DateTime LastUpdate { get; set; }
        public long UserId { get; set; }
    }
}
