using AIService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIService.Data;

public class AIDbContext : DbContext
{
    public AIDbContext(DbContextOptions<AIDbContext> options) : base(options) { }

    public DbSet<MessageThread> MessageThreads { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.MessageThread)
            .WithMany(mt => mt.Messages)
            .HasForeignKey(m => m.MessageThreadId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
