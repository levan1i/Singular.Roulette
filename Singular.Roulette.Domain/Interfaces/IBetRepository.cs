using Singular.Roulette.Common.Types;
using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Interfaces
{
    public interface IBetRepository: IGenericRepository<Bet>
    {
        Task<PagedResult<Bet>> GetGameHistory(long UserId,int page, int pageSize);
    }
}
