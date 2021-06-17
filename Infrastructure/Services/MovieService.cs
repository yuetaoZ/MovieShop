using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighestRevenueMovies();

            var movieCardList = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                    Title = movie.Title
                });
            }

            return movieCardList;
        }
        
        //public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        //{
        //    var movies = await _movieRepository.GetTopRatedMovies();

        //    var movieCardList = new List<MovieCardResponseModel>();
        //    foreach (var movie in movies)
        //    {
        //        movieCardList.Add(new MovieCardResponseModel
        //        {
        //            Id = movie.Id,
        //            PosterUrl = movie.PosterUrl,
        //            ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
        //            Title = movie.Title
        //        });
        //    }

        //    return movieCardList;
        //}

        public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return null;
            }

            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                RunTime = movie.RunTime,
                Price = movie.Price,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault()
            };

            movieDetails.Genres = new List<GenreResponseModel>();
            movieDetails.Casts = new List<CastCardResponseModel>();

            foreach (var moviecast in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastCardResponseModel { 
                    Id = moviecast.CastId, 
                    Name = moviecast.Cast.Name, 
                    ProfilePath = moviecast.Cast.ProfilePath, 
                    Character = moviecast.Character });
            }

            foreach (var moviegenre in movie.MovieGenres)
            {
                movieDetails.Genres.Add(new GenreResponseModel { 
                    Id = moviegenre.Genre.Id, 
                    Name = moviegenre.Genre.Name
                });
            }

            return movieDetails;
        }

        public async Task<IEnumerable<MovieReviewResponseModel>> GetReviesByMovieId(int id)
        {
            var reviews = await _movieRepository.GetMovieReviews(id);

            var response = new List<MovieReviewResponseModel>();

            foreach (var review in reviews)
            {
                response.Add(new MovieReviewResponseModel
                {
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                }); 
            }

            return response;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTopRatedMovies();

            var response = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                response.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.GetValueOrDefault()
                });
            }

            return response;
        }

        public Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest model)
        {
            throw new System.NotImplementedException();
        }

        public Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest model)
        {
            throw new System.NotImplementedException();
        }
    }
}
