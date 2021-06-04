using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public MovieDetailsResponseModel GetMovieDetailsById(int id)
        {
            var movieDetails = new MovieDetailsResponseModel
            {
                Id = 1,
                Title = "Avengers: Infinity War",
                PosterUrl = "",
                Budget = 1200000,
                Overview = "Test String"
            };

            return movieDetails;
        }

        public List<MovieCardResponseModel> GetTopRevenueMovies()
        {
            // we need to call our repository and get the data from database
            var movies = new List<MovieCardResponseModel>
            {
                new MovieCardResponseModel {Id = 1, Title = "Avengers: Infinity War"},
                new MovieCardResponseModel {Id = 2, Title = "Avatar"},
                new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens"},
                new MovieCardResponseModel {Id = 4, Title = "Titanic"},
                new MovieCardResponseModel {Id = 5, Title = "Inception"},
                new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron"},
                new MovieCardResponseModel {Id = 7, Title = "Interstellar"},
                new MovieCardResponseModel {Id = 8, Title = "Fight Club"},
                new MovieCardResponseModel
                {
                    Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring"
                },
                new MovieCardResponseModel {Id = 10, Title = "The Dark Knight"},
                new MovieCardResponseModel {Id = 11, Title = "The Hunger Games"},
                new MovieCardResponseModel {Id = 12, Title = "Django Unchained"},
                new MovieCardResponseModel
                {
                    Id = 13, Title = "The Lord of the Rings: The Return of the King"
                },
                new MovieCardResponseModel
                    {Id = 14, Title = "Harry Potter and the Philosopher's Stone"},
                new MovieCardResponseModel {Id = 15, Title = "Iron Man"},
                new MovieCardResponseModel {Id = 16, Title = "Furious 7"}
            };

            return movies;
        }
    }
}
