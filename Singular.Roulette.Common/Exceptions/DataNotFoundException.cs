using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Common.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message)
        {
            Message = message;
        }
       public string Message { get; }
        public string Code { get; set; }
    }
}
