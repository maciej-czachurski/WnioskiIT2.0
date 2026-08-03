using System.ComponentModel.DataAnnotations;

namespace WnioskiIT.Models;

public class PendingApproval
{
    public int Id { get; set; }

    public int ItRequestId { get; set; }
    public ItRequest ItRequest { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    public string SubTitle { get; set; } = string.Empty;

    [MaxLength(20)]
    public string IconKey { get; set; } = "purchase";

    public bool IsResolved { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
