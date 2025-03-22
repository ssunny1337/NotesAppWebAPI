using MediatR;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Commands
{
    public class GetAllTagsCommand : IRequest<List<Tag>> { }
}
