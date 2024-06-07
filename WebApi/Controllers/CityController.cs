using Application.Features.Cities.Commands.CreateCitiesCommand;
using Application.Features.Cities.Commands.DeleteCitiesCommand;
using Application.Features.Cities.Queries.GetAllCities;
using Application.Features.Cities.Queries.GetCityById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/city")]
        public async Task<IActionResult> Get()
        {
            var data = await _mediator.Send(new GetAllCityQuery());
            return Ok(data);

        }

        [HttpGet]
        [Route("api/city/{id}")]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            var data = await _mediator.Send(new GetCityByIdQuery(id));
            return Ok(data);
        }


        
        [HttpPost]
        [Route("api/city")]
        public async Task<ActionResult> Create(CreateCityCommand createCityCommand)
        {
            if (createCityCommand == null)
            {
                return BadRequest(new { Message = "please enter a city name!" });

            }

            var data = await _mediator.Send(createCityCommand);

            if (data.Successed)
            {
                return Ok(data);
            }

            return BadRequest(new { Message = data.Messages });
        }

        [HttpPut]
        [Route("api/city/{id}")]

        public async Task<IActionResult> Update(int id, CreateCityCommand command)
        {
            if (id == null)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("api/city/{id}")]

        public async Task<IActionResult> Delete(int id)
        {

            if (id == null)
            {
                return BadRequest("Id Is null, Please enter  a valid Id and try again !");
            }
            var city = await _mediator.Send(new DeleteCityCommand(id));
            return Ok(city);
        }
    }
}
