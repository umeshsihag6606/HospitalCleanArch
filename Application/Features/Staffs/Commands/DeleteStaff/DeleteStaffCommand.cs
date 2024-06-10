using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Staffs;
using MediatR;
using Shared;

namespace Application.Features.Staffs.Commands.DeleteStaff;

public class DeleteStaffCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public DeleteStaffCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteStaffCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
       var data = await _unitOfWork.Repositary<Staff>().GetById(request.Id);
        if(data == null)
        {
            return Result<int>.BadRequest($"Sorry Id ={request.Id} is not found");
        }
        var mapData= _mapper.Map<Staff>(data);
        await _unitOfWork.Repositary<Staff>().DeleteAsync(mapData);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success("Data Deleted");
    }
}
