using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Id = genre.Id, Name = genre.Name
                });
            }

            return genresModel;
        }
    }