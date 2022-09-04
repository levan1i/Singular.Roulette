using Singular.Roulette.Common.Extentions;
using Singular.Roulette.Common.Types;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Repository
{
    public class BetRepository : GenericRepository<Bet>, IBetRepository
    {
        public BetRepository(SingularDbContext context) : base(context)
        {
        }

        public Task<PagedResult<Bet>> GetGameHistory(long UserId, int page, int pageSize)
        {
            var query = _context.Bets.Where(x => x.UserId == UserId).OrderByDescending(x=>x.CreateDate);
            var paged = query.GetPagedResultAsync(page, pageSize);
            return paged;
        }
    }
}
