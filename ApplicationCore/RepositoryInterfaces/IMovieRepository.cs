using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        IEnumerable<Movie> GetTopRatedMovies();
        IEnumerable<Movie> GetHighestRevenueMovies();
    }
}
