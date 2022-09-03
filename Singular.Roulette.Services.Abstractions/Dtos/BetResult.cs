using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions.Dtos
{
    public class BetResult
    {
        public int WiningNumber { get; set; }   
        public decimal WonAmount { get; set; }
    }
}
