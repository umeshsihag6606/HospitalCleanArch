using Application.Dto.Countries;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Counteries;
using MediatR;
using Shared;


namespace Application.Features.Countries.Queries.GetAllCountries
{
    public class GetAllCountriesQuery : IRequest<Result<List<GetCountryDto>>>
    {

    }
    internal class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, Result<List<GetCountryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetCountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<Countary>().GetAllAsync();
            if (data == null && data.Count == 0)
            {
                return Result<List<GetCountryDto>>.NotFound("Invalid Command");
            }
            var mapData = _mapper.Map<List<GetCountryDto>>(data);
            return Result<List<GetCountryDto>>.Success(mapData, "Country List");
        }
    }
}
