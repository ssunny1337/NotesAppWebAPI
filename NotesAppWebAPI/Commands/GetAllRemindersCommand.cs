using MediatR;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Commands
{
    public class GetAllRemindersCommand : IRequest<List<Reminder>> { }
}
