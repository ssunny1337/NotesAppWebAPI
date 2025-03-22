using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, bool>
    {
        private readonly ITagService _tagService;

        public UpdateTagCommandHandler(ITagService tagService) => _tagService = tagService;

        public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            return await _tagService.UpdateAsync(request.TagId, request.Name);
        }
    }
}
