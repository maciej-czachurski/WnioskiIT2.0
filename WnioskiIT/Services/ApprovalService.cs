using Microsoft.EntityFrameworkCore;
using WnioskiIT.Data;
using WnioskiIT.Models;

namespace WnioskiIT.Services;

public class ApprovalService(AppDbContext db) : IApprovalService
{
    public Task<List<PendingApproval>> GetPendingAsync() =>
        db.PendingApprovals
          .Where(a => !a.IsResolved)
          .OrderBy(a => a.CreatedAt)
          .ToListAsync();

    public async Task ApproveAsync(int approvalId)
    {
        var approval = await db.PendingApprovals.FindAsync(approvalId);
        if (approval is not null)
        {
            approval.IsResolved = true;
            await db.SaveChangesAsync();
        }
    }

    public Task<int> GetPendingCountAsync() =>
        db.PendingApprovals.CountAsync(a => !a.IsResolved);
}
