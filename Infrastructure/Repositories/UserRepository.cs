using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository: EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {

            var user = await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public override async Task<User> GetByIdAsync(int Id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Purchases).ThenInclude(p => p.Movie)
                .Include(u => u.Favorites).ThenInclude(f => f.Movie)
                .FirstOrDefaultAsync(u => u.Id == Id);
            return user;
        }
    }
}
