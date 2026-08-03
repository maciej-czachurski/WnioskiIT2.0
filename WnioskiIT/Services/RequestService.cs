using Microsoft.EntityFrameworkCore;
using WnioskiIT.Data;
using WnioskiIT.Models;

namespace WnioskiIT.Services;

public class RequestService(AppDbContext db) : IRequestService
{
    public Task<List<ItRequest>> GetAllAsync() =>
        db.Requests
          .Include(r => r.Attachments)
          .Include(r => r.History)
          .OrderByDescending(r => r.CreatedAt)
          .ToListAsync();

    public Task<ItRequest?> GetByCodeAsync(string code) =>
        db.Requests
          .Include(r => r.Attachments)
          .Include(r => r.History)
          .FirstOrDefaultAsync(r => r.Code == code);

    public Task<ItRequest?> GetByIdAsync(int id) =>
        db.Requests
          .Include(r => r.Attachments)
          .Include(r => r.History)
          .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<ItRequest> CreateAsync(ItRequest request)
    {
        request.Code = await GetNextCodeAsync();
        request.CreatedAt = DateTime.UtcNow;

        request.History.Add(new RequestHistoryEntry
        {
            Action = "Wniosek został utworzony",
            ActorName = request.CreatedByName,
            OccurredAt = request.CreatedAt
        });

        request.History.Add(new RequestHistoryEntry
        {
            Action = "Przekazano do właściwej ścieżki akceptacji",
            ActorName = "Automatyczny obieg wniosków",
            OccurredAt = request.CreatedAt.AddSeconds(1)
        });

        db.Requests.Add(request);
        await db.SaveChangesAsync();
        return request;
    }

    public async Task UpdateAsync(ItRequest request)
    {
        request.UpdatedAt = DateTime.UtcNow;
        db.Requests.Update(request);
        await db.SaveChangesAsync();
    }

    public async Task<string> GetNextCodeAsync()
    {
        var highest = await db.Requests
            .Select(r => r.Code)
            .ToListAsync();

        int max = highest
            .Select(code => int.TryParse(code.Split('-').LastOrDefault(), out int n) ? n : 0)
            .DefaultIfEmpty(131)
            .Max();

        return $"WIT-{DateTime.UtcNow.Year}-{(max + 1):D4}";
    }

    public async Task<DashboardMetrics> GetDashboardMetricsAsync()
    {
        var total = await db.Requests.CountAsync();
        var inProgress = await db.Requests.CountAsync(r =>
            r.Status == RequestStatus.WRealizacji || r.Status == RequestStatus.OczekujeNaAkceptacje || r.Status == RequestStatus.Nowy);
        var needsAction = await db.Requests.CountAsync(r => r.Status == RequestStatus.DoUzupelnienia);
        var completed = await db.Requests.CountAsync(r => r.Status == RequestStatus.Zakonczony);

        return new DashboardMetrics(total, inProgress, needsAction, completed);
    }
}
