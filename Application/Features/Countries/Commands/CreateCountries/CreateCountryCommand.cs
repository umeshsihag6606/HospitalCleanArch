using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Counteries;
using MediatR;
using Shared;

namespace Application.Features.Countries.Commands.CreateCountries
{
    public class CreateCountryCommand:IRequest<Result<int>>
    {
        public string Name { get; set; }
    }
    internal class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var mapData=_mapper.Map<Countary>(request);
            await _unitOfWork.Repositary<Countary>().AddAsync(mapData);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("Inserted...");
        }
    }
}
