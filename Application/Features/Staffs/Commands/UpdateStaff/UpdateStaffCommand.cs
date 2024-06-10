using Application.Dto.Staffs;
using Application.Features.Staffs.Commands.CreateStaff;
using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Staffs;
using MediatR;
using Shared;

namespace Application.Features.Staffs.Commands.UpdateStaff;

public class UpdateStaffCommand : IRequest<Result<GetStaffDto>>
{
    public int Id { get; set; }
    public CreateStaffCommand CreateStaffCommand { get; set; }

    public UpdateStaffCommand(int id, CreateStaffCommand createStaffCommand)
    {
        Id = id;
        CreateStaffCommand = createStaffCommand;
    }
}
internal class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, Result<GetStaffDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateStaffCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetStaffDto>> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Repositary<Staff>().GetById(request.Id);
        if (data == null)
        {
            return Result<GetStaffDto>.NotFound($"Sorry Id = {request.Id} is not found");
        }
        var mapData= _mapper.Map(request.CreateStaffCommand,data);
        await _unitOfWork.Repositary<Staff>().UpdateAsync(mapData, request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<GetStaffDto>.Success("Data Updated....");
    }
}

