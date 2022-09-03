using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Repository
{
    internal class SpinRepository : GenericRepository<Spin>, ISpinRepository
    {
        public SpinRepository(SingularDbContext context) : base(context)
        {
        }
    }
}
