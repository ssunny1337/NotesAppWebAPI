using MediatR;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Commands
{
    public class GetNoteCommand : IRequest<Note>
    {
        public int NoteId { get; set; }
    }
}
