using Application.Comman.Mappings;
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

namespace Application.Features.Cities.Commands.CreateCitiesCommand
{
    public  class CreateCityCommand : IRequest<Result<int>>, IMapFrom<City>
    {
        public string Name { get; set; }
        public int StateId { get; set; }

    }
    internal class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Result<int>>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City city = new City()
            {
                Name= request.Name,
                StateId= request.StateId,
            };

            await _unitOfWork.Repositary<City>().AddAsync(city);
            await _unitOfWork.Save(cancellationToken);

            return  Result<int>.Success(city.Id,"City created successfully!");

        }
    }


}
