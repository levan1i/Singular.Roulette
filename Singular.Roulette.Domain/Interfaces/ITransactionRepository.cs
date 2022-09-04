﻿using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Interfaces
{
    public interface ITransactionRepository :IGenericRepository<Transaction>
    {
        Task<bool> MakeBetTransaction(long AccountId,decimal Amount);
        Task<bool> MakeBetWinTransaction(long AccountId, decimal Amount);
    }
}
