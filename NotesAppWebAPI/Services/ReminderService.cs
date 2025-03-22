using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly ITagRepository _tagRepository;

        public ReminderService(IReminderRepository reminderRepository, ITagRepository tagRepository)
        {
            _reminderRepository = reminderRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Reminder?> GetByIdAsync(int id)
        {
            return await _reminderRepository.GetByIdAsync(id);
        }

        public async Task<List<Reminder>> GetAllAsync()
        {
            return await _reminderRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(string title, string text, DateTime reminderTime, List<int> tagsIds)
        {
            var tags = (await _tagRepository.GetAllAsync()).Where(t => tagsIds.Contains(t.Id));

            var reminder = new Reminder
            {
                Title = title,
                Text = text,
                ReminderTime = reminderTime
            };

            foreach (var tag in tags)
                reminder.ReminderTags.Add(new ReminderTag { Reminder = reminder, Tag = tag });

            await _reminderRepository.AddAsync(reminder);

            return reminder.Id;
        }

        public async Task<bool> UpdateAsync(int id, string title, string text, DateTime reminderTime, List<int> tagsIds)
        {
            var reminder = await _reminderRepository.GetByIdAsync(id);

            if (reminder is null)
                return false;

            var tags = (await _tagRepository.GetAllAsync()).Where(t => tagsIds.Contains(t.Id));

            reminder.Title = title;
            reminder.Text = text;
            reminder.ReminderTime = reminderTime;

            reminder.ReminderTags.Clear();

            foreach (var tag in tags)
                reminder.ReminderTags.Add(new ReminderTag { Reminder = reminder, Tag = tag });

            await _reminderRepository.UpdateAsync(reminder);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reminder = await _reminderRepository.GetByIdAsync(id);

            if (reminder is null)
                return false;

            await _reminderRepository.DeleteAsync(reminder);

            return true;
        }
    }
}
