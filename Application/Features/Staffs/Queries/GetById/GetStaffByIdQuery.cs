using Application.Dto.Staffs;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Staffs;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Staffs.Queries.GetById;

public class GetStaffByIdQuery:IRequest<Result<GetStaffDto>>
{ 
    public int Id { get; set; }
    public GetStaffByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, Result<GetStaffDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetStaffDto>> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Repositary<Staff>().GetById(request.Id);
        if(data == null)
        {
            return Result<GetStaffDto>.NotFound($"Sorry Id = {request.Id} is not found");
        }
        var mapData= _mapper.Map<GetStaffDto>(data);
        return Result<GetStaffDto>.Success(mapData, "Data is..");
    }
}
