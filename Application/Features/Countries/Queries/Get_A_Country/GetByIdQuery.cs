using Application.Dto.Countries;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Counteries;
using MediatR;
using Shared;

namespace Application.Features.Countries.Queries.GetById
{
    public class GetByIdQuery : IRequest<Result<GetCountryDto>>
    {
        public int Id { get; set; }
        public GetByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<GetCountryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetCountryDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<Countary>().GetById(request.Id);
            if (data == null)
            {
                return Result<GetCountryDto>.NotFound($"Invalid Id : {request.Id}");
            }
            var mapData = _mapper.Map<GetCountryDto>(data);
            return Result<GetCountryDto>.Success(mapData, "Country Data");
        }
    }
}
