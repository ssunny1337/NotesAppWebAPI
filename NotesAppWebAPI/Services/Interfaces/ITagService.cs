using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task<Tag?> GetByIdAsync(int id);
        Task<List<Tag>> GetAllAsync();
        Task<int> AddAsync(string name);
        Task<bool> UpdateAsync(int id, string name);
        Task<bool> DeleteAsync(int id);
    }
}
