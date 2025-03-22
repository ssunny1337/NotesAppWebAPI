using MediatR;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Commands
{
    public class CreateTagCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
