using Microsoft.EntityFrameworkCore;
using WnioskiIT.Data;
using WnioskiIT.Models;

namespace WnioskiIT.Services;

public class NotificationService(AppDbContext db) : INotificationService
{
    public Task<List<AppNotification>> GetAllAsync() =>
        db.Notifications.OrderByDescending(n => n.CreatedAt).ToListAsync();

    public async Task MarkAllReadAsync()
    {
        await db.Notifications.Where(n => !n.IsRead).ExecuteUpdateAsync(s => s.SetProperty(n => n.IsRead, true));
    }

    public Task<int> GetUnreadCountAsync() =>
        db.Notifications.CountAsync(n => !n.IsRead);
}
