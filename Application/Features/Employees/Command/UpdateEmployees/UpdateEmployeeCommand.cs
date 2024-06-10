using Application.Dto.Employees;
using Application.Features.Employees.Command.CreateEmployees;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Employees;
using MediatR;
using Shared;

namespace Application.Features.Employees.Command.UpdateEmployees
{
    public class UpdateEmployeeCommand : IRequest<Result<EmployeeDto>>
    {
        public int Id { get; set; }
        public CreateEmployeeCommand CreateEmployeeCommand { get; set; }
        public UpdateEmployeeCommand(int id, CreateEmployeeCommand createEmployeeCommand)
        {
            Id = id;
            CreateEmployeeCommand = createEmployeeCommand;
        }
    }
    internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repositary<Employee>().GetById(request.Id);
            if (data == null)
            {
                return Result<EmployeeDto>.BaRequest($"Sorry Update Id={request.Id} Not Found");
            }
            var mapdata = _mapper.Map(request.CreateEmployeeCommand, data);
            await _unitOfWork.Repositary<Employee>().UpdateAsync(mapdata, request.Id);
            await _unitOfWork.Save(cancellationToken);
            return Result<EmployeeDto>.Success("Data Update");
        }
    }
}
