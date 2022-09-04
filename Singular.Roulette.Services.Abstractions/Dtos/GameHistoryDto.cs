using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions.Dtos
{
    public class GameHistoryDto
    {
        public long SpinId { get; set; }
        public decimal BetAmount { get; set; }  
        public decimal WonAmount { get; set; }  
        public DateTime CreateDate { get; set; }
    }
}
