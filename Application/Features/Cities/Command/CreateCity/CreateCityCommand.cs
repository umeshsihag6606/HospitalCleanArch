using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Cities;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Command.CreateCity
{
    public class CreateCityCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public int StateId { get; set; }
    }
    internal class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var mapdata = _mapper.Map<City>(request);
            var data = _unitOfWork.Repositary<City>().AddAsync(mapdata);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("Inserted........");

        }
    }
}
