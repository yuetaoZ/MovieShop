using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }
        public IEnumerable<Movie> GetHighestRevenueMovies()
        {
            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;
        }

        public IEnumerable<Movie> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public override Movie GetById(int id)
        {
            // get movie info from Movie Table
            // get genres by joining moviegenre, genre
            // movie, moviecast and cast
            // rating, Avg of movieid Review Table
            return base.GetById(id);
        }
    }
}
