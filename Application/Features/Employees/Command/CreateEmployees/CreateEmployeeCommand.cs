using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.CreateEmployees
{
    public class CreateEmployeeCommand:IRequest<Result<int>>
    {
        public string FastName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int Age { get; set; }
        [Phone(ErrorMessage ="Phone Number is not Format")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage ="Email is Not Format")]
        public string Email { get; set; }
        public int CityId { get; set; }
    }
    internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var checkExistMobileNumber = await _unitOfWork.Repositary<Employee>().Entities.Where(x => x.PhoneNumber == request.PhoneNumber).FirstOrDefaultAsync();
            if(checkExistMobileNumber != null) 
            {
                return Result<int>.BaRequest("Mobile number is already exist");
            }
            var mapdata = _mapper.Map<Employee>(request);
            var data=await _unitOfWork.Repositary<Employee>().AddAsync(mapdata);
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("Inserted.......");
        }
    }
}
