using Application.Dto.Employees;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Employees;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetByIdEmployees
{
    public class GetByIdEmployeeQuerie:IRequest<Result<EmployeeDto>>
    {
        public int Id { get; set; }
        public GetByIdEmployeeQuerie(int id)
        {
            Id = id;
        }

    }
    internal class GetByIdEmployeeQuerieHandler : IRequestHandler<GetByIdEmployeeQuerie, Result<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdEmployeeQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeDto>> Handle(GetByIdEmployeeQuerie request, CancellationToken cancellationToken)
        {
            var data= await _unitOfWork.Repositary<Employee>().GetById(request.Id);
            if (data == null)
            {
                return Result<EmployeeDto>.BaRequest($"Sorry Id={request.Id} Not Found");
            }
            var mapadta=_mapper.Map<EmployeeDto>(data);
            return Result<EmployeeDto>.Success(mapadta, "Employee Data");
        }
    }
}
