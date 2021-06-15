using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Exceptions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        private readonly IMapper _mapper;

        public CastService(ICastRepository castRepository, IMapper mapper)
        {
            _castRepository = castRepository;
            _mapper = mapper;
        }

        public async Task<CastDetailsResponseModel> GetCastDetailsById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            if (cast == null)
            {
                throw new NotFoundException("Cast", id);
            }

            var response = _mapper.Map<CastDetailsResponseModel>(cast);
            return response;
        }
    }
}
