using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
    {
        private readonly ITagService _tagService;

        public DeleteTagCommandHandler(ITagService tagService) => _tagService = tagService;

        public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            return await _tagService.DeleteAsync(request.TagId);
        }
    }
}
