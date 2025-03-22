using MediatR;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Commands
{
    public class GetReminderCommand : IRequest<Reminder>
    {
        public int ReminderId { get; set; }
    }
}
