using Application.Features.Countries.Commands.CreateCountries;
using Application.Features.Countries.Commands.DeleteCountries;
using Application.Features.Countries.Commands.UpdateCountries;
using Application.Features.Countries.Queries.GetAllCountries;
using Application.Features.Countries.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Countries
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var data = await _mediator.Send(new GetAllCountriesQuery());
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var data = await _mediator.Send(new GetByIdQuery(id));
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreateCountryCommand command)
        {
            var data = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpPut]
        public async Task<ActionResult> Put(int id, CreateCountryCommand command)
        {
            var data = await _mediator.Send(new UpdateCountryCommand(id, command));
            return ResponseHelper.GenerateResponse(data);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _mediator.Send(new DeleteCountryCommand(id));
            return ResponseHelper.GenerateResponse(data);
        }
    }
}
