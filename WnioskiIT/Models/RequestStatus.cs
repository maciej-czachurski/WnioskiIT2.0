namespace WnioskiIT.Models;

public enum RequestStatus
{
    Nowy,
    WRealizacji,
    DoUzupelnienia,
    OczekujeNaAkceptacje,
    Zakonczony,
    Anulowany
}

public static class RequestStatusExtensions
{
    public static string ToLabel(this RequestStatus status) => status switch
    {
        RequestStatus.Nowy => "Nowy",
        RequestStatus.WRealizacji => "W realizacji",
        RequestStatus.DoUzupelnienia => "Do uzupełnienia",
        RequestStatus.OczekujeNaAkceptacje => "Oczekuje na akceptację",
        RequestStatus.Zakonczony => "Zakończony",
        RequestStatus.Anulowany => "Anulowany",
        _ => status.ToString()
    };

    public static string ToBadgeClass(this RequestStatus status) => status switch
    {
        RequestStatus.Zakonczony => "success",
        RequestStatus.DoUzupelnienia => "danger",
        RequestStatus.WRealizacji => "warning",
        RequestStatus.Anulowany => "neutral",
        _ => ""
    };

    public static string ToGroup(this RequestStatus status) => status switch
    {
        RequestStatus.Zakonczony => "done",
        RequestStatus.DoUzupelnienia => "action",
        _ => "open"
    };
}
