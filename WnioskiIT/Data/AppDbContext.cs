using Microsoft.EntityFrameworkCore;
using WnioskiIT.Models;

namespace WnioskiIT.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ItRequest> Requests => Set<ItRequest>();
    public DbSet<RequestAttachment> Attachments => Set<RequestAttachment>();
    public DbSet<RequestHistoryEntry> History => Set<RequestHistoryEntry>();
    public DbSet<PendingApproval> PendingApprovals => Set<PendingApproval>();
    public DbSet<AppNotification> Notifications => Set<AppNotification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ItRequest>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasIndex(r => r.Code).IsUnique();
            e.Property(r => r.Code).IsRequired().HasMaxLength(20);
            e.Property(r => r.Title).IsRequired().HasMaxLength(200);
            e.Property(r => r.Description).IsRequired().HasMaxLength(5000);
            e.Property(r => r.DynamicFieldsJson).IsRequired().HasDefaultValue("{}");
            e.HasMany(r => r.Attachments).WithOne(a => a.ItRequest).HasForeignKey(a => a.ItRequestId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(r => r.History).WithOne(h => h.ItRequest).HasForeignKey(h => h.ItRequestId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PendingApproval>(e =>
        {
            e.HasKey(a => a.Id);
            e.HasOne(a => a.ItRequest).WithMany().HasForeignKey(a => a.ItRequestId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
