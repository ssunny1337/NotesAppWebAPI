using MediatR;

namespace NotesAppWebAPI.Commands
{
    public class DeleteTagCommand : IRequest<bool>
    {
        public int TagId { get; set; }
    }
}
