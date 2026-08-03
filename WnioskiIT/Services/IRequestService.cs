using WnioskiIT.Models;

namespace WnioskiIT.Services;

public interface IRequestService
{
    Task<List<ItRequest>> GetAllAsync();
    Task<ItRequest?> GetByCodeAsync(string code);
    Task<ItRequest?> GetByIdAsync(int id);
    Task<ItRequest> CreateAsync(ItRequest request);
    Task UpdateAsync(ItRequest request);
    Task<string> GetNextCodeAsync();
    Task<DashboardMetrics> GetDashboardMetricsAsync();
}

public record DashboardMetrics(int Total, int InProgress, int NeedsAction, int Completed);
