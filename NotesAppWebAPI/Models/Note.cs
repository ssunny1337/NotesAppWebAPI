namespace NotesAppWebAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public ICollection<NoteTag> NoteTags { get; set; } = new List<NoteTag>();
    }
}
