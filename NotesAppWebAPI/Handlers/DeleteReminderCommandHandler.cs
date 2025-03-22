using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, bool>
    {
        private readonly IReminderService _reminderService;

        public DeleteReminderCommandHandler(IReminderService reminderService) => _reminderService = reminderService;

        public async Task<bool> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
        {
            return await _reminderService.DeleteAsync(request.ReminderId);
        }
    }
}
