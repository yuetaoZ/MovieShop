using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<Purchase>> GetAllPurchasesByMovieId(int movieId)
        {
            var purchases = await _dbContext.Purchases.Where(p => p.MovieId == movieId).Include(m => m.Movie)
                .Include(m => m.User)
                .OrderByDescending(p => p.PurchaseDateTime)
                .ToListAsync();

            return purchases;
         }
    }
}
