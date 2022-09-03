using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message)
        {
            Message = message;
        }
        string Message { get; }
    }
}
