using MediatR;

namespace NotesAppWebAPI.Commands
{
    public class UpdateTagCommand : IRequest<bool>
    {
        public int TagId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
