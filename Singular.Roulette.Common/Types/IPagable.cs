using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Common.Types
{
    public interface IPagable
    {
         int CurrentPage { get; set; }
         int PageCount { get; set; }
         int PageSize { get; set; }
         int RowCount { get; set; }
    }
}
