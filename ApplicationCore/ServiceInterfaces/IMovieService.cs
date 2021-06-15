using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        // method for getting top 30 highest revenue movies..
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetailsById(int id);
        Task<IEnumerable<MovieReviewResponseModel>> GetReviesByMovieId(int id);
        Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies();
        Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest model);
        Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest model);
    }
}
