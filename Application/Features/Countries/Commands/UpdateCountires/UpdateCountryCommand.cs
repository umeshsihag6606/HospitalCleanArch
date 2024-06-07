using Application.Features.Countries.Commands.CreateCountries;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Counteries;
using MediatR;
using Shared;

namespace Application.Features.Countries.Commands.UpdateCountries
{
    public class UpdateCountryCommand : IRequest<Result<int>>
    {
        public CreateCountryCommand CreateCountryCommand { get; set; }
        public int Id { get; set; }
        public UpdateCountryCommand(int id, CreateCountryCommand createCountryCommand)
        {
            Id = id;
            CreateCountryCommand = createCountryCommand;
        }
    }
    internal class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCountryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _unitOfWork.Repositary<Countary>().GetById(request.Id);
            if (country == null)
            {
                return Result<int>.NotFound($"Invalid Id : {request.Id}");
            }
            var mapData = _mapper.Map(request.CreateCountryCommand, country);
            await _unitOfWork.Repositary<Countary>().UpdateAsync(mapData,request.Id);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(mapData.Id, "Updated Data");

        }
    }
}
