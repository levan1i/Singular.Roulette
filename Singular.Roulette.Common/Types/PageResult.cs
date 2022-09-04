
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singular.Roulette.Common.Types { 
    public class PageResult 
    {
        public int CurrentPage { get;  set; }
        public int TotalPages { get;  set; }
        public int PageSize { get;  set; }
        public long TotalCount { get;  set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
