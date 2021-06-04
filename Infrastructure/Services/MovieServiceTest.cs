using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieServiceTest : IMovieService
    {
        public MovieDetailsResponseModel GetMovieDetailsById(int id)
        {
            throw new NotImplementedException();
        }

        public List<MovieCardResponseModel> GetTopRevenueMovies()
        {
            throw new NotImplementedException();
        }
    }
}
