using MediatR;

namespace NotesAppWebAPI.Commands
{
    public class UpdateNoteCommand : IRequest<bool>
    {
        public int NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<int> TagsIds { get; set; } = new();
    }
}
