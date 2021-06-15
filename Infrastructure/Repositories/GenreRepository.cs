using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Genre> GetByIdAsync(int Id)
        {
            var genre = await _dbContext.Genres
                .Include(g => g.MovieGenres)
                .ThenInclude(m => m.Movie)
                .FirstOrDefaultAsync(g => g.Id == Id);

            return genre;
        }
    }
}
