using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class GetAllTagsCommandHandler : IRequestHandler<GetAllTagsCommand, List<Tag>>
    {
        private readonly ITagService _tagService;

        public GetAllTagsCommandHandler(ITagService tagService) => _tagService = tagService;

        public async Task<List<Tag>> Handle(GetAllTagsCommand request, CancellationToken cancellationToken)
        {
            return await _tagService.GetAllAsync();
        }
    }
}
