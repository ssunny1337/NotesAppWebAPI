using MediatR;
using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, bool>
    {
        private readonly INoteService _noteService;

        public UpdateNoteCommandHandler(INoteService noteService) => _noteService = noteService;

        public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            return await _noteService.UpdateAsync(request.NoteId, request.Title, request.Text, request.TagsIds);
        }
    }
}
