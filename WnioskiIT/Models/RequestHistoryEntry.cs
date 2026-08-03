using System.ComponentModel.DataAnnotations;

namespace WnioskiIT.Models;

public class RequestHistoryEntry
{
    public int Id { get; set; }

    public int ItRequestId { get; set; }
    public ItRequest ItRequest { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Action { get; set; } = string.Empty;

    [MaxLength(150)]
    public string ActorName { get; set; } = string.Empty;

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
