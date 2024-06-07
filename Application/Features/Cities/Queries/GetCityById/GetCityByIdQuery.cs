using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Cities;
using MediatR;
using Shared;

namespace Application.Features.Cities.Queries.GetCityById
{
    public class GetCityByIdQuery : IRequest<Result<GetCityByIdDto>>
    {
        public int Id { get; set; }

        public GetCityByIdQuery() { }
        public GetCityByIdQuery(int id)
        {
            Id = id;
        }

    }


    internal class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, Result<GetCityByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetCityByIdDto>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.Repositary<City>().GetById(request.Id);
            if (city == null)
            {
                return Result<GetCityByIdDto>.NotFound("City not found.");
            }

            var cityDto = _mapper.Map<GetCityByIdDto>(city);
            return Result<GetCityByIdDto>.Success(cityDto, "City retrieved successfully!");
        }
    }

}
