using Application.Dto.States;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.States;
using MediatR;
using Shared;

namespace Application.Features.States.Queries.GetAllStates
{
    public class GetAllStateQuery : IRequest<Result<List<GetStateDto>>>
    {

    }
    internal class GetAllStateQueryHandler : IRequestHandler<GetAllStateQuery, Result<List<GetStateDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllStateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetStateDto>>> Handle(GetAllStateQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<State>().GetAllAsync();
            if (data == null && data.Count == 0)
            {
                return Result<List<GetStateDto>>.NotFound("Invalid Command");
            }
            var mapData = _mapper.Map<List<GetStateDto>>(data);
            return Result<List<GetStateDto>>.Success(mapData, "State List");
        }
    }
}
