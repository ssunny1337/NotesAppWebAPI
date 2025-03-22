using MediatR;

namespace NotesAppWebAPI.Commands
{
    public class DeleteReminderCommand : IRequest<bool>
    {
        public int ReminderId { get; set; }
    }
}
