using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;
using NotesAppWebAPI.Services.Interfaces;

namespace NotesAppWebAPI.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly ITagRepository _tagRepository;

        public NoteService(INoteRepository noteRepository, ITagRepository tagRepository)
        {
            _noteRepository = noteRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Note?> GetByIdAsync(int id)
        {
            return await _noteRepository.GetByIdAsync(id);
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await _noteRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(string title, string text, List<int> tagsIds)
        {
            var tags = (await _tagRepository.GetAllAsync()).Where(t => tagsIds.Contains(t.Id));

            var note = new Note
            {
                Title = title,
                Text = text,
            };

            foreach (var tag in tags)
                note.NoteTags.Add(new NoteTag { Note = note, Tag = tag });

            await _noteRepository.AddAsync(note);

            return note.Id;
        }

        public async Task<bool> UpdateAsync(int id, string title, string text, List<int> tagsIds)
        {           
            var note = await _noteRepository.GetByIdAsync(id);

            if (note is null)
                return false;

            var tags = (await _tagRepository.GetAllAsync()).Where(t => tagsIds.Contains(t.Id));

            note.Title = title;
            note.Text = text;

            note.NoteTags.Clear();

            foreach (var tag in tags)
                note.NoteTags.Add(new NoteTag { Note = note, Tag = tag });

            await _noteRepository.UpdateAsync(note);
            return true;            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);

            if (note is null)
                return false;

            await _noteRepository.DeleteAsync(note);

            return true;
        }
    }
}
