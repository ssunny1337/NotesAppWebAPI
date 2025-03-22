using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Repositories.Interfaces
{
    public interface IReminderRepository
    {
        Task<Reminder?> GetByIdAsync(int id);
        Task<List<Reminder>> GetAllAsync();
        Task AddAsync(Reminder reminder);
        Task UpdateAsync(Reminder reminder);
        Task DeleteAsync(Reminder reminder);
    }
}
