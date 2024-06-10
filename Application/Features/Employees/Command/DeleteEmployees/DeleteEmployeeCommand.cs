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

namespace Application.Features.Employees.Command.DeleteEmployees
{
    public class DeleteEmployeeCommand:IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteEmployeeCommand(int id)
        {
            Id = id;
        }
    }
    internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
           var data=await _unitOfWork.Repositary<Employee>().GetById(request.Id);
            if(data == null)
            {
                return Result<int>.BaRequest($"Sorry Delete ID={request.Id} Not Found");
            }
            var mapdata=_mapper.Map<Employee>(data);
            await _unitOfWork.Repositary<Employee>().DeleteAsync(mapdata);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("Delete Data");
        }
    }
}
