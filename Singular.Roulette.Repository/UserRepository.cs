using Microsoft.EntityFrameworkCore;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Domain.Models;

using System.Threading.Tasks;

namespace Singular.Roulette.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SingularDbContext context) : base(context)
        {
        }

        public Task<User> FindByUserName(string username) => _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
    }
}
