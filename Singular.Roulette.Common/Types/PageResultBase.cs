﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Common.Types
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
}
