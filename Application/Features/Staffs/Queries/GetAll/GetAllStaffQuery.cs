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

namespace Application.Features.Staffs.Queries.GetAll;

public class GetAllStaffQuery:IRequest<Result<List<GetStaffDto>>>
{
}
internal class GetAllStaffQueryHandler : IRequestHandler<GetAllStaffQuery, Result<List<GetStaffDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllStaffQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetStaffDto>>> Handle(GetAllStaffQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Repositary<Staff>().GetAllAsync();
        if(data==null&& data.Count()==0) 
        {
            return Result<List<GetStaffDto>>.NotFound("Data not found");

        }
        var mapData= _mapper.Map<List<GetStaffDto>>(data);
        return Result<List<GetStaffDto>>.Success(mapData, "Data is......");
    }
}
