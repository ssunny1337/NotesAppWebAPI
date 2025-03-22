using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Services.Interfaces
{
    public interface IReminderService
    {
        Task<Reminder?> GetByIdAsync(int id);
        Task<List<Reminder>> GetAllAsync();
        Task<int> AddAsync(string title, string text, DateTime reminderTime, List<int> tagsIds);
        Task<bool> UpdateAsync(int id, string title, string text, DateTime reminderTime, List<int> tagsIds);
        Task<bool> DeleteAsync(int id);
    }
}
