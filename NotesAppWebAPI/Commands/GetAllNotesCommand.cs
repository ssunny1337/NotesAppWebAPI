using MediatR;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Commands
{
    public class GetAllNotesCommand : IRequest<List<Note>> { }
}
