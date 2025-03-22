using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class GetAllRemindersCommandHandler : IRequestHandler<GetAllRemindersCommand, List<Reminder>>
    {
        private readonly IReminderService _reminderService;

        public GetAllRemindersCommandHandler(IReminderService reminderService) => _reminderService = reminderService;

        public async Task<List<Reminder>> Handle(GetAllRemindersCommand request, CancellationToken cancellationToken)
        {
            return await _reminderService.GetAllAsync();
        }
    }
}
