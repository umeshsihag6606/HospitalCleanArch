using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Counteries;
using Domain.Entities.States;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared;

namespace Application.Features.States.Commands.CreateStates
{
    public class CreateStateCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public int CounatryId { get; set; }
    }
    internal class CreateStateCommandHandler : IRequestHandler<CreateStateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            var checkcountary = await _unitOfWork.Repositary<Countary>().GetById(request.CounatryId);
            if(checkcountary == null)
            {
                return Result<int>.BadRequest("Invalid Countary Id...");
            }
            var mapData = _mapper.Map<State>(request);
            await _unitOfWork.Repositary<State>().AddAsync(mapData);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("Inserted...");
        }
    }
}
