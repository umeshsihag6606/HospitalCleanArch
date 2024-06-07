using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Cities;
using MediatR;
using Shared;

namespace Application.Features.Cities.Commands.DeleteCitiesCommand
{
    public class DeleteCityCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteCityCommand(int id)
        {
            Id= id;
        }
    }
    internal class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.Repositary<City>().GetById(request.Id);
            if (city == null)
            {
                return Result<int>.NotFound("City not found.");
            }

            await _unitOfWork.Repositary<City>().DeleteAsync(city);
            await _unitOfWork.Save(cancellationToken);

            return Result<int>.Success(city.Id, "City deleted successfully!");
        }
    }


}
