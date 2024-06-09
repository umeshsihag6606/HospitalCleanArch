using Application.Features.Countries.Queries.GetById;
using Application.Features.States.Commands.CreateStates;
using Application.Features.States.Commands.DeleteStates;
using Application.Features.States.Commands.UpdateStates;
using Application.Features.States.Queries.GetAllStates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.States
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var data = await _mediator.Send(new GetAllStateQuery());
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var data = await _mediator.Send(new GetByIdQuery(id));
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreateStateCommand command)
        {
            var data = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpPut]
        public async Task<ActionResult> Put(int id, CreateStateCommand command)
        {
            var data = await _mediator.Send(new UpdateStateCommand(id, command));
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _mediator.Send(new DeleteStateCommand(id));
            return ResponseHelper.GenerateResponse(data);
        }
    }
}
