using Application.Mediator_Handlers.Cafe.Commands;
using Application.Mediator_Handlers.Commands;
using Application.Mediator_Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CafesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CafesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet] //cafes?location=<location>
        public async Task<IActionResult> GetCafes([FromQuery] string? location)
        {
            var cafes = await _mediator.Send(new GetCafesQuery { Location = location });

            if(cafes == null)
            return NotFound();

            return Ok(cafes);
        }

        [HttpPost] //api/Cafe 
        public async Task<IActionResult> CreateCafe([FromBody] CreateCafeCommand command)
        {
            var cafeId = await _mediator.Send(command);
            if(cafeId.GetType() != typeof(Guid))
                 return BadRequest("Problem creating Cafe");
                 
            return CreatedAtAction(nameof(GetCafes), new { id = cafeId },  new { Id = cafeId });
        }

        [HttpPut("{id}")] //PUT /api/Cafe/{id}
        public async Task<IActionResult> UpdateCafe(Guid id, [FromBody] UpdateCafeCommand command)
        {
             if (id != command.Id)
                return BadRequest("ID in URL and body must match");

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")] //DELETE /api/Cafe/4f7ac345-3c41-4df7-81e7-9b6f33b94310
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
           var success = await _mediator.Send(new DeleteCafeCommand { Id = id });
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}