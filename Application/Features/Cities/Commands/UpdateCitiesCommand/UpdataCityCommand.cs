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

namespace Application.Features.Cities.Commands.UpdateCitiesCommand
{
    public class UpdateCityCommand : IRequest<Result<int>>, IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }
    internal class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.Repositary<City>().GetById(request.Id);
            if (city == null)
            {
                return Result<int>.NotFound("City not found.");
            }

            city.Name = request.Name;
            city.StateId = request.StateId;

            await _unitOfWork.Repositary<City>().UpdateAsync(city,city.Id);
            await _unitOfWork.Save(cancellationToken);

            return Result<int>.Success(city.Id, "City updated successfully!");
        }
    }
}
