using Application.Dto.Employees;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Employees;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetEmployees
{
    public class GetAllEmployeeQuerie : IRequest<Result <List<EmployeeDto>>>
    {

    }
    internal class GetAllEmployeeCommandHandler : IRequestHandler<GetAllEmployeeQuerie, Result<List<EmployeeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeeDto>>> Handle(GetAllEmployeeQuerie request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<Employee>().GetAllAsync();
            if (data == null)
            {
                return Result<List<EmployeeDto>>.NotFound("Sorry data not Found");
            }
            var mapdata=_mapper.Map<List<EmployeeDto>>(data);
            return Result<List<EmployeeDto>>.Success(mapdata,"Employee List");
        }
    }
}
