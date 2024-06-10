using Application.Features.Cities.Command.CreateCity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Cities
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult>Post(CreateCityCommand input)
        {
            var data=await _mediator.Send(input);
            return ResponseHelper.GenerateResponse(data);
        }
    }
}
