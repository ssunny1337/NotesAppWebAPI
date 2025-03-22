using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Controllers
{
    [Route("api/v1/function/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command)
        {
            var tagId = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateTag), new { id = tagId }, tagId);
        }

        [HttpPost("get/{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var command = new GetTagCommand { TagId = id };

            var tag = await _mediator.Send(command);

            if (tag is null)
                return NotFound("Tag not found");

            return Ok(tag);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllTags()
        {            
            var command = new GetAllTagsCommand();  

            var tags = await _mediator.Send(command);

            return Ok(tags);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagCommand command)
        {            
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Tag not found");

            return Ok("Tag updated");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var command = new DeleteTagCommand { TagId = id };
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound("Tag not found");

            return NoContent();
        }
    }
}
