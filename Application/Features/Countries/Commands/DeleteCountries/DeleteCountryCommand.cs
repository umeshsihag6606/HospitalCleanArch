using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Counteries;
using MediatR;
using Shared;

namespace Application.Features.Countries.Commands.DeleteCountries
{
    public class DeleteCountryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteCountryCommand(int id)
        {
            Id = id;
        }
    }
    internal class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<Countary>().GetById(request.Id);
            if (data == null)
            {
                return Result<int>.NotFound($"Invalid Id:{request.Id}");
            }
            var mapData = _mapper.Map<Countary>(data);
            await _unitOfWork.Repositary<Countary>().DeleteAsync(mapData);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("Data deleted...");
        }
    }
}
