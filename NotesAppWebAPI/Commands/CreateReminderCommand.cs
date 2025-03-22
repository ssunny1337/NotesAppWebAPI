using MediatR;

namespace NotesAppWebAPI.Commands
{
    public class CreateReminderCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime ReminderTime { get; set; }
        public List<int> TagsIds { get; set; } = new();
    }
}
