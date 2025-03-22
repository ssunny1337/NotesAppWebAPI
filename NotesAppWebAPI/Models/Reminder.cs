namespace NotesAppWebAPI.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime ReminderTime { get; set; }

        public ICollection<ReminderTag> ReminderTags { get; set; } = new List<ReminderTag>();
    }
}
