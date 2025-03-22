using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoteTag> NoteTags { get; set; }
        public DbSet<ReminderTag> ReminderTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteTag>()
                .HasKey(nt => new { nt.NoteId, nt.TagId });

            modelBuilder.Entity<NoteTag>()
                .HasOne(nt => nt.Note)
                .WithMany(n => n.NoteTags)
                .HasForeignKey(nt => nt.NoteId);

            modelBuilder.Entity<NoteTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NoteTags)
                .HasForeignKey(nt => nt.TagId);

            modelBuilder.Entity<ReminderTag>()
                .HasKey(rt => new { rt.ReminderId, rt.TagId });

            modelBuilder.Entity<ReminderTag>()
                .HasOne(t => t.Tag)
                .WithMany(rt => rt.ReminderTags)
                .HasForeignKey(t => t.TagId);

            modelBuilder.Entity<ReminderTag>()
                .HasOne(r => r.Reminder)
                .WithMany(rt => rt.ReminderTags)
                .HasForeignKey(r => r.ReminderId);

        }
    }
}