using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreResponseModel>> GetAllGenres()
        {
            var genres = await _genreRepository.ListAll();

            var genresModel = new List<GenreResponseModel>();
            foreach (var genre in genres)
            {
                genresModel.Add(new GenreResponseModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }

            return genresModel;
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenreId(int Id)
        {
            var genre = await _genreRepository.GetById(Id);

            if (genre == null)
            {
                return null;
            }

            var movieCasts = new List<MovieCardResponseModel>();

            foreach (var moviecast in genre.MovieGenres)
            {
                movieCasts.Add(new MovieCardResponseModel
                {
                    Id = moviecast.Movie.Id,
                    PosterUrl = moviecast.Movie.PosterUrl,
                    ReleaseDate = moviecast.Movie.ReleaseDate.GetValueOrDefault(),
                    Title = moviecast.Movie.Title
                });
            }

            return movieCasts;
        }
    }
}
