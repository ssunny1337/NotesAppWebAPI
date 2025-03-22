namespace NotesAppWebAPI.Models
{
    public class ReminderTag
    {
        public int ReminderId { get; set; }
        public Reminder Reminder { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
