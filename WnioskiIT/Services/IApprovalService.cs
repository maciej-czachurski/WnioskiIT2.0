using WnioskiIT.Models;

namespace WnioskiIT.Services;

public interface IApprovalService
{
    Task<List<PendingApproval>> GetPendingAsync();
    Task ApproveAsync(int approvalId);
    Task<int> GetPendingCountAsync();
}
