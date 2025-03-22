using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class GetTagCommandHandler : IRequestHandler<GetTagCommand, Tag>
    {
        private readonly ITagService _tagService;

        public GetTagCommandHandler(ITagService tagService) => _tagService = tagService;

        public async Task<Tag> Handle(GetTagCommand request, CancellationToken cancellationToken)
        {
            return await _tagService.GetByIdAsync(request.TagId);
        }
    }
}
