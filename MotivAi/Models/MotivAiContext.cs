using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotivAi.Models;



public class MotivAiContext : DbContext
{
    public MotivAiContext(DbContextOptions<MotivAiContext> options) : base(options) { }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Task_Status> Task_Status { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<AI_Request> AI_Requests { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Notification_Type> Notification_Types { get; set; }
    public DbSet<Calendar_Event> Calendar_Events { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Habit_Frequency> Habit_Frequencies { get; set; }
    public DbSet<ChatHistory> ChatHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // تكوين العلاقات بشكل صريح لتجنب المشاكل

        // User - Role
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.Role_id);

        // Goal - User
        modelBuilder.Entity<Goal>()
            .HasOne(g => g.User)
            .WithMany(u => u.Goals)
            .HasForeignKey(g => g.User_id);

        // Task - Goal
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Goal)
            .WithMany(g => g.Tasks)
            .HasForeignKey(t => t.Goal_id);

        // Task - Task_Status
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Status)
            .WithMany(ts => ts.Tasks)
            .HasForeignKey(t => t.Status_id);

        // Attachment - Task
        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.Task)
            .WithMany(t => t.Attachments)
            .HasForeignKey(a => a.Task_id);

        // Notification - User
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.User_id);

        // Notification - Notification_Type
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Type)
            .WithMany(nt => nt.Notifications)
            .HasForeignKey(n => n.Type_id);

        // Habit - User
        modelBuilder.Entity<Habit>()
            .HasOne(h => h.User)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.User_id);

        // Habit - Habit_Frequency
        modelBuilder.Entity<Habit>()
            .HasOne(h => h.Frequency)
            .WithMany(hf => hf.Habits)
            .HasForeignKey(h => h.Frequency_id);

        // Calendar_Event - User
        modelBuilder.Entity<Calendar_Event>()
            .HasOne(ce => ce.User)
            .WithMany(u => u.Calendar_Events)
            .HasForeignKey(ce => ce.User_id);

        // AI_Request - User
        modelBuilder.Entity<AI_Request>()
            .HasOne(ai => ai.User)
            .WithMany(u => u.AI_Requests)
            .HasForeignKey(ai => ai.User_id);
    }
}


