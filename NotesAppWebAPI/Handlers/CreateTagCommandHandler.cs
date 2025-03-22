using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly ITagService _tagService;

        public CreateTagCommandHandler(ITagService tagService) => _tagService = tagService;          

        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            return await _tagService.AddAsync(request.Name);
        }
    }
}
