using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, bool>
    {
        private readonly IReminderService _reminderService;

        public UpdateReminderCommandHandler(IReminderService reminderService) => _reminderService = reminderService;

        public async Task<bool> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            return await _reminderService.UpdateAsync(request.ReminderId, request.Title, request.Text, request.ReminderTime, request.TagsIds);
        }
    }
}
