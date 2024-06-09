using Application.Features.States.Commands.CreateStates;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.States;
using MediatR;
using Shared;

namespace Application.Features.States.Commands.UpdateStates
{
    public class UpdateStateCommand : IRequest<Result<int>>
    {
        public CreateStateCommand CreateStateCommand { get; set; }
        public int Id { get; set; }
        public UpdateStateCommand(int id, CreateStateCommand createStateCommand)
        {
            Id = id;
            CreateStateCommand = createStateCommand;
        }
    }
    internal class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            var state = await _unitOfWork.Repositary<State>().GetById(request.Id);
            if (state == null)
            {
                return Result<int>.NotFound($"Invalid Id:{request.Id}");
            }
            var mapData = _mapper.Map(request.CreateStateCommand, state);
            await _unitOfWork.Repositary<State>().UpdateAsync(mapData, request.Id);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(mapData.Id, "Updated Data");
        }
    }
}
