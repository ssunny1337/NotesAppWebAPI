using System.Text.Json.Serialization;

namespace NotesAppWebAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<NoteTag> NoteTags { get; set; } = new List<NoteTag>();
        [JsonIgnore]
        public ICollection<ReminderTag> ReminderTags { get; set; } = new List<ReminderTag>();
    }
}
