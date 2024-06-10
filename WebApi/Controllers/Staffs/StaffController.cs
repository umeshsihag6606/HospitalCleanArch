using Application.Features.Countries.Queries.GetById;
using Application.Features.Staffs.Commands.CreateStaff;
using Application.Features.Staffs.Commands.DeleteStaff;
using Application.Features.Staffs.Commands.UpdateStaff;
using Application.Features.Staffs.Queries.GetAll;
using Application.Features.Staffs.Queries.GetById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Staffs;

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IMediator _mediator;

    public StaffController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult>Post(CreateStaffCommand command)
    {
        var data= await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(data);
    }
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var data = await _mediator.Send(new GetAllStaffQuery());
        return ResponseHelper.GenerateResponse(data);
    }
    [HttpGet]
    [Route("api/Staff/{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var data =await _mediator.Send(new GetStaffByIdQuery(id));
        return ResponseHelper.GenerateResponse(data);
    }
    [HttpPut]
    [Route("api/Staff/{id}")]
    public async Task<ActionResult> Put( int id,CreateStaffCommand command) 
    {
        var data = await _mediator.Send(new UpdateStaffCommand(id, command));
        return ResponseHelper.GenerateResponse(data);
    }
    [HttpDelete]
    [Route("api/Staff/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var data = await _mediator.Send(new DeleteStaffCommand (id));
        return ResponseHelper.GenerateResponse(data);
    }


}
