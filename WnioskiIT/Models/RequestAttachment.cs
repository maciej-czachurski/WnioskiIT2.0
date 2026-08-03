using System.ComponentModel.DataAnnotations;

namespace WnioskiIT.Models;

public class RequestAttachment
{
    public int Id { get; set; }

    public int ItRequestId { get; set; }
    public ItRequest ItRequest { get; set; } = null!;

    [Required, MaxLength(260)]
    public string FileName { get; set; } = string.Empty;

    public long FileSizeBytes { get; set; }

    [MaxLength(100)]
    public string ContentType { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(150)]
    public string UploadedBy { get; set; } = string.Empty;
}
