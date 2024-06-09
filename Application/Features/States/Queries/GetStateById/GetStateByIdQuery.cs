using Application.Dto.States;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.States;
using MediatR;
using Shared;

namespace Application.Features.States.Queries.GetStateById
{
    public class GetStateByIdQuery : IRequest<Result<GetStateDto>>
    {
        public int Id { get; set; }
        public GetStateByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, Result<GetStateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetStateByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetStateDto>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<State>().GetById(request.Id);
            if (data == null)
            {
                return Result<GetStateDto>.NotFound($"Invalid Id : {request.Id}");
            }
            var mapData = _mapper.Map<GetStateDto>(data);
            return Result<GetStateDto>.Success(mapData, "State data");
        }
    }
}
