using MediatR;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Handlers
{
    public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, int>
    {
        private readonly IReminderService _reminderService;

        public CreateReminderCommandHandler(IReminderService reminderService) => _reminderService = reminderService;

        public async Task<int> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
        {
            return await _reminderService.AddAsync(request.Title, request.Text, request.ReminderTime, request.TagsIds);
        }
    }
}
