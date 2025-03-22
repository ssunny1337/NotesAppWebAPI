using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _tagRepository.GetByIdAsync(id);
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(string name)
        {
            var tag = new Tag { Name = name };
            await _tagRepository.AddAsync(tag);
            return tag.Id;
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag is null)
                return false;

            tag.Name = name;

            await _tagRepository.UpdateAsync(tag);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag is null)
                return false;

            await _tagRepository.DeleteAsync(tag);

            return true;
        }
    }
}
