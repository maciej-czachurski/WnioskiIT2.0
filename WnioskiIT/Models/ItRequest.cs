using System.ComponentModel.DataAnnotations;

namespace WnioskiIT.Models;

public class ItRequest
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string TypeKey { get; set; } = "purchase";

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(5000)]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [Required, MaxLength(150)]
    public string CreatedByName { get; set; } = "Jan Kowalski";

    [MaxLength(150)]
    public string? AssigneeName { get; set; }

    [MaxLength(100)]
    public string Department { get; set; } = "Wydział W2";

    [MaxLength(50)]
    public string CostCenter { get; set; } = string.Empty;

    public DateTime? NeededByDate { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.Nowy;

    public Priority Priority { get; set; } = Priority.Standardowy;

    public int SlaProgressPercent { get; set; } = 5;

    public string SlaDeadlineLabel { get; set; } = string.Empty;

    /// <summary>JSON-serialized dictionary of dynamic field values (label → value)</summary>
    public string DynamicFieldsJson { get; set; } = "{}";

    public ICollection<RequestAttachment> Attachments { get; set; } = new List<RequestAttachment>();
    public ICollection<RequestHistoryEntry> History { get; set; } = new List<RequestHistoryEntry>();
}
