using MediatR;
using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class GetNoteCommandHandler : IRequestHandler<GetNoteCommand, Note>
    {
        private readonly INoteService _noteService;

        public GetNoteCommandHandler(INoteService noteService) => _noteService = noteService;

        public async Task<Note> Handle(GetNoteCommand request, CancellationToken cancellationToken)
        {
            return await _noteService.GetByIdAsync(request.NoteId);
        }
    }
}
