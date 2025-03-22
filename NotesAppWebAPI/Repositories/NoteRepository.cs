using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Data;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;

namespace NotesAppWebAPI.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NoteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Note note)
        {
            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Note note)
        {            
            _dbContext.NoteTags.RemoveRange(note.NoteTags);

            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await _dbContext.Notes
                .Include(n => n.NoteTags)
                .ThenInclude(nt => nt.Tag)
                .ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(int id)
        {
            return await _dbContext.Notes
                .Include(n => n.NoteTags)
                .ThenInclude(nt => nt.Tag)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateAsync(Note note)
        {
            _dbContext.Notes.Update(note);
            await _dbContext.SaveChangesAsync();
        }
    }
}
