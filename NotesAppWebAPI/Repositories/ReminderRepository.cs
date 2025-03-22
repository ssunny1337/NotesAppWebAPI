using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Data;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;

namespace NotesAppWebAPI.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReminderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Reminder reminder)
        {
            _dbContext.Reminders.Add(reminder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reminder reminder)
        {
            _dbContext.ReminderTags.RemoveRange(reminder.ReminderTags);

            _dbContext.Reminders.Remove(reminder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Reminder>> GetAllAsync()
        {
            return await _dbContext.Reminders
                .Include(r => r.ReminderTags)
                .ThenInclude(rt => rt.Tag)
                .ToListAsync();
        }

        public async Task<Reminder?> GetByIdAsync(int id)
        {
            return await _dbContext.Reminders
                .Include(r => r.ReminderTags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            _dbContext.Reminders.Update(reminder);
            await _dbContext.SaveChangesAsync();
        }
    }
}
