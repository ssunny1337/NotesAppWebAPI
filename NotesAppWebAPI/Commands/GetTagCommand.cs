using MediatR;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Commands
{
    public class GetTagCommand : IRequest<Tag>
    {
        public int TagId { get; set; }
    }
}
