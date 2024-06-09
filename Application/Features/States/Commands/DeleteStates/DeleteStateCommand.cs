using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.States;
using MediatR;
using Shared;

namespace Application.Features.States.Commands.DeleteStates
{
    public class DeleteStateCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteStateCommand(int id)
        {
            Id = id;
        }
    }
    internal class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<State>().GetById(request.Id);
            if (data == null)
            {
                return Result<int>.NotFound($"Invalid Id:{request.Id}");
            }
            var mapData = _mapper.Map<State>(data);
            await _unitOfWork.Repositary<State>().DeleteAsync(mapData);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("Data Deleted...");
        }
    }
}
