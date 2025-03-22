using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class GetReminderCommandHandler : IRequestHandler<GetReminderCommand, Reminder>
    {
        private readonly IReminderService _reminderService;

        public GetReminderCommandHandler(IReminderService reminderService) => _reminderService = reminderService;

        public async Task<Reminder> Handle(GetReminderCommand request, CancellationToken cancellationToken)
        {
            return await _reminderService.GetByIdAsync(request.ReminderId);
        }
    }
}
