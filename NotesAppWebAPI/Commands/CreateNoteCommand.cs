using MediatR;

namespace NotesAppWebAPI.Commands
{
    public class CreateNoteCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<int> TagsIds { get; set; } = new();
    }
}
