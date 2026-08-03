namespace WnioskiIT.Models;

public enum Priority
{
    Standardowy,
    Wysoki,
    Krytyczny
}

public static class PriorityExtensions
{
    public static string ToLabel(this Priority priority) => priority switch
    {
        Priority.Standardowy => "Standardowy",
        Priority.Wysoki => "Wysoki",
        Priority.Krytyczny => "Krytyczny",
        _ => priority.ToString()
    };

    public static string ToBadgeClass(this Priority priority) => priority switch
    {
        Priority.Krytyczny => "danger",
        Priority.Wysoki => "warning",
        _ => "neutral"
    };
}
