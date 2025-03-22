using MediatR;

namespace NotesAppWebAPI.Commands
{
    public class DeleteNoteCommand : IRequest<bool> 
    {
        public int NoteId { get; set; }
    }
}
