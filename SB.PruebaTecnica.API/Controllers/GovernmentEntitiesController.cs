using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.PruebaTecnica.Application.Commands.GovernmentEntities;

namespace SB.PruebaTecnica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GovernmentEntitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GovernmentEntitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddGovernmentEntityCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest("Failed to add the government entity.");
            }

            return Ok("Government entity added successfully.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _mediator.Send(new GetAllGovernmentEntitiesCommand());
            return Ok(entities);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _mediator.Send(new GetGovernmentEntityByIdCommand() { Id = id});
            if (entity == null)
            {
                return NotFound("Government entity not found.");
            }

            return Ok(entity);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateGovernmentEntityCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest("Failed to update the government entity.");
            }

            return Ok("Government entity updated successfully.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteGovernmentEntityCommand() { Id = id});
            if (!result)
            {
                return BadRequest("Failed to delete the government entity.");
            }

            return Ok("Government entity deleted successfully.");
        }
    }
}
