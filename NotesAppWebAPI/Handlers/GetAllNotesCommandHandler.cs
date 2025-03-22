using MediatR;
using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class GetAllNotesCommandHandler : IRequestHandler<GetAllNotesCommand, List<Note>>
    {
        private readonly INoteService _noteService;

        public GetAllNotesCommandHandler(INoteService noteService) => _noteService = noteService;

        public async Task<List<Note>> Handle(GetAllNotesCommand request, CancellationToken cancellationToken)
        {
            return await _noteService.GetAllAsync();
        }
    }
}
