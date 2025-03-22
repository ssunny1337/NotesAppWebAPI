using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Controllers
{
    [Route("api/v1/function/reminder")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReminderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReminder([FromBody] CreateReminderCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateReminder), new { id = result }, result);
        }

        [HttpPost("get/{id}")]
        public async Task<IActionResult> GetReminder(int id)
        {
            var command = new GetReminderCommand { ReminderId = id };

            var reminder = await _mediator.Send(command);

            if (reminder is null)
                return NotFound("Reminder not found");

            return Ok(reminder);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllReminders()
        {
            var command = new GetAllRemindersCommand();

            var reminders = await _mediator.Send(command);

            return Ok(reminders);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateReminder([FromBody] UpdateReminderCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Reminder not found");

            return NoContent();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var command = new DeleteReminderCommand { ReminderId = id };
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound("Reminder not found");

            return NoContent();
        }
    }
}
