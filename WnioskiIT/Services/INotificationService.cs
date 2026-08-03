using WnioskiIT.Models;

namespace WnioskiIT.Services;

public interface INotificationService
{
    Task<List<AppNotification>> GetAllAsync();
    Task MarkAllReadAsync();
    Task<int> GetUnreadCountAsync();
}
