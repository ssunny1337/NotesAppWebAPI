using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Services.Interfaces
{
    public interface INoteService
    {
        Task<Note?> GetByIdAsync(int id);
        Task<List<Note>> GetAllAsync();
        Task<int> AddAsync(string title, string text, List<int> tagsIds);
        Task<bool> UpdateAsync(int id, string title, string text, List<int> tagsIds);
        Task<bool> DeleteAsync(int id);
    }
}
