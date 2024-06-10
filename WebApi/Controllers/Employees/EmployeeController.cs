using Application.Features.Employees.Command.CreateEmployees;
using Application.Features.Employees.Command.DeleteEmployees;
using Application.Features.Employees.Command.UpdateEmployees;
using Application.Features.Employees.Queries.GetByIdEmployees;
using Application.Features.Employees.Queries.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace WebApi.Controllers.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult>Post(CreateEmployeeCommand input)
        {
            var data=await _mediator.Send(input);
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpGet]
        public async Task<ActionResult>GetAll()
        {
            var data = await _mediator.Send(new GetAllEmployeeQuerie());
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpGet]
        [Route("api/Employee/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var data = await _mediator.Send(new GetByIdEmployeeQuerie(id));
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpPut]
        [Route("api/Employee/{id}")]
        public async Task<ActionResult>Put(int id,CreateEmployeeCommand input)
        {
            var data = await _mediator.Send(new UpdateEmployeeCommand(id, input));
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpDelete]
        [Route("api/Employee/{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var data=await _mediator.Send(new DeleteEmployeeCommand(id));
            return ResponseHelper.GenerateResponse(data);
        }
    }
}
