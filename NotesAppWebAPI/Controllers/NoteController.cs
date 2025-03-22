using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Controllers
{
    [Route("api/v1/function/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateNote), new { id = result }, result);           
        }

        [HttpPost("get/{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var command = new GetNoteCommand { NoteId = id };

            var note = await _mediator.Send(command);

            if (note is null)
                return NotFound("Note not found");

            return Ok(note);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllNotes()
        {
            var command = new GetAllNotesCommand();

            var notes = await _mediator.Send(command);

            return Ok(notes);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateNote([FromBody] UpdateNoteCommand command)
        {            
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Note not found");

            return NoContent();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var command = new DeleteNoteCommand { NoteId = id };
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound("Note not found");

            return NoContent();
        }
    }
}
