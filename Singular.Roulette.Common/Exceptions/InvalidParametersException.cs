using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Common.Exceptions
{
    public class InvalidParametersException:Exception
    {
        public InvalidParametersException(string message,string code    )
        {
            Message = message;
            Code = code;
        }
        public string Message { get; }
        public string Code { get; set; }
    }
}
