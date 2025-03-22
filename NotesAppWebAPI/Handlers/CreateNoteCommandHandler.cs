using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
    {
        private readonly INoteService _noteService; 

        public CreateNoteCommandHandler(INoteService noteService) => _noteService = noteService;

        public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken) 
        {
            return await _noteService.AddAsync(request.Title, request.Text, request.TagsIds);
        }
    }
}
