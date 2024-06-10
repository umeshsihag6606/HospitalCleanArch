using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Staffs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Staffs.Commands.CreateStaff;

public class CreateStaffCommand : IRequest<Result<int>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public int Age { get; set; }
    [Phone(ErrorMessage = "Moblie no is not format")]
    public string MobileNumber { get; set; }
    [EmailAddress(ErrorMessage = "Email is not format")]
    public string Email { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }

}
internal class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateStaffCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var mobile = await _unitOfWork.Repositary<Staff>().Entities.Where(s => s.Email == request.Email || s.MobileNumber == request.MobileNumber).FirstOrDefaultAsync();
        if(mobile != null) 
        {
            return Result<int>.BadRequest("Email or mobile number is already exist");
        }
        var mapData = _mapper.Map<Staff>(request);
        var data = await _unitOfWork.Repositary<Staff>().AddAsync(mapData);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success("Inserted....");
    }
}
