using Application.Mediator_Handlers.Commands;
using Application.Mediator_Handlers.Employee.Commands;
using Application.Mediator_Handlers.Employee.Queries.QueryValidator;
using Application.Mediator_Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]  ///employees?cafe=<cafe>
        public async Task<IActionResult> GetEmployeeById([FromQuery] Guid id)
        {
            var employees = await _mediator.Send(new GetEmployeeByIdQuery  { Id = id});
           if (employees == null)
            return NotFound();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var id = await _mediator.Send(command);

             if( id == null)
                 return BadRequest("Problem creating Cafe");

            return CreatedAtAction(nameof(GetEmployeeById), new { id }, new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL and body must match");

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var success = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}