using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Domain.Models
{
    public class Spin
    {
        public long SpinId { get; set; }
        public DateTime CreateDate { get; set; }
        public int WiningNumber { get; set; }
    }
}
