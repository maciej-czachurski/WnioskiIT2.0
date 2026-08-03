using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WnioskiIT.Models;

namespace WnioskiIT.Data;

/// <summary>Seeds demo data when running the application for the first time.</summary>
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Requests.AnyAsync()) return;

        var requests = new List<ItRequest>
        {
            new()
            {
                Code = "WIT-2026-0131",
                TypeKey = "external-sharing",
                Title = "Udostępnienie dokumentacji partnerowi",
                Category = "Bezpieczeństwo informacji",
                CreatedAt = new DateTime(2026, 7, 29, 8, 42, 0, DateTimeKind.Utc),
                CreatedByName = "Jan Kowalski",
                AssigneeName = "Zespół Bezpieczeństwa IT",
                Department = "Wydział W2",
                CostCenter = "W2-PROD-100",
                Status = RequestStatus.DoUzupelnienia,
                Priority = Priority.Wysoki,
                SlaProgressPercent = 84,
                SlaDeadlineLabel = "Dzisiaj, 16:30",
                Description = "Proszę o umożliwienie przekazania dokumentacji technicznej partnerowi realizującemu część prac w projekcie 39WE.",
                DynamicFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    ["Podmiot zewnętrzny"] = "Partner Projekt Sp. z o.o.",
                    ["Osoba odbierająca"] = "Adam Nowak",
                    ["Adres e-mail"] = "adam.nowak@partner.pl",
                    ["Klasyfikacja"] = "Wewnętrzne",
                    ["Sposób udostępnienia"] = "SharePoint / OneDrive",
                    ["Okres dostępu"] = "29.07.2026 – 30.09.2026",
                    ["Dane osobowe"] = "Nie"
                }),
                Attachments = [new() { FileName = "zakres_dokumentacji.pdf", FileSizeBytes = 102400, ContentType = "application/pdf", UploadedBy = "Jan Kowalski" }],
                History =
                [
                    new() { Action = "Wniosek został utworzony", ActorName = "Jan Kowalski", OccurredAt = new DateTime(2026, 7, 29, 8, 42, 0, DateTimeKind.Utc) },
                    new() { Action = "Przekazano do właściwej ścieżki akceptacji", ActorName = "Automatyczny obieg wniosków", OccurredAt = new DateTime(2026, 7, 29, 8, 43, 0, DateTimeKind.Utc) },
                    new() { Action = "Przydzielono osobę realizującą", ActorName = "Zespół Bezpieczeństwa IT", OccurredAt = new DateTime(2026, 7, 29, 9, 0, 0, DateTimeKind.Utc) },
                    new() { Action = "Wymagane uzupełnienie danych", ActorName = "Zespół Bezpieczeństwa IT", OccurredAt = new DateTime(2026, 7, 29, 14, 0, 0, DateTimeKind.Utc) }
                ]
            },
            new()
            {
                Code = "WIT-2026-0130",
                TypeKey = "virtual-machine",
                Title = "Maszyna testowa dla projektu 39WE",
                Category = "Infrastruktura wirtualna",
                CreatedAt = new DateTime(2026, 7, 28, 10, 15, 0, DateTimeKind.Utc),
                CreatedByName = "Jan Kowalski",
                AssigneeName = "Anna Wójcik",
                Department = "Wydział W2",
                CostCenter = "W2-PROD-100",
                Status = RequestStatus.WRealizacji,
                Priority = Priority.Standardowy,
                SlaProgressPercent = 62,
                SlaDeadlineLabel = "01.08.2026, 15:00",
                Description = "Proszę o utworzenie maszyny testowej dla aplikacji wspierającej obsługę dokumentacji technicznej projektu 39WE.",
                DynamicFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    ["Operacja"] = "Utworzenie nowej maszyny",
                    ["Środowisko"] = "Testowe",
                    ["System"] = "Windows Server 2022",
                    ["vCPU"] = "4",
                    ["RAM"] = "16 GB",
                    ["Dysk"] = "200 GB",
                    ["Sieć"] = "Izolowana sieć testowa"
                }),
                Attachments = [new() { FileName = "architektura_39WE.pdf", FileSizeBytes = 204800, ContentType = "application/pdf", UploadedBy = "Jan Kowalski" }],
                History =
                [
                    new() { Action = "Wniosek został utworzony", ActorName = "Jan Kowalski", OccurredAt = new DateTime(2026, 7, 28, 10, 15, 0, DateTimeKind.Utc) },
                    new() { Action = "Przekazano do właściwej ścieżki akceptacji", ActorName = "Automatyczny obieg wniosków", OccurredAt = new DateTime(2026, 7, 28, 10, 16, 0, DateTimeKind.Utc) },
                    new() { Action = "Przydzielono osobę realizującą", ActorName = "Anna Wójcik", OccurredAt = new DateTime(2026, 7, 28, 11, 0, 0, DateTimeKind.Utc) }
                ]
            },
            new()
            {
                Code = "WIT-2026-0128",
                TypeKey = "permissions",
                Title = "Nadanie dostępu do systemu IFS",
                Category = "Uprawnienia",
                CreatedAt = new DateTime(2026, 7, 24, 12, 30, 0, DateTimeKind.Utc),
                CreatedByName = "Jan Kowalski",
                AssigneeName = "Piotr Zieliński",
                Department = "Wydział W2",
                CostCenter = "W2-PROD-100",
                Status = RequestStatus.Zakonczony,
                Priority = Priority.Standardowy,
                SlaProgressPercent = 100,
                SlaDeadlineLabel = "Zrealizowano 25.07.2026",
                Description = "Nadanie dostępu do modułu raportowania operacji produkcyjnych w systemie IFS.",
                DynamicFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    ["Operacja"] = "Nadanie uprawnień",
                    ["System"] = "IFS",
                    ["Użytkownik"] = "Jan Kowalski",
                    ["Rola"] = "Raportowanie operacji produkcyjnych",
                    ["Okres"] = "Bezterminowo",
                    ["Dostęp podwyższony"] = "Nie"
                }),
                History =
                [
                    new() { Action = "Wniosek został utworzony", ActorName = "Jan Kowalski", OccurredAt = new DateTime(2026, 7, 24, 12, 30, 0, DateTimeKind.Utc) },
                    new() { Action = "Przekazano do właściwej ścieżki akceptacji", ActorName = "Automatyczny obieg wniosków", OccurredAt = new DateTime(2026, 7, 24, 12, 31, 0, DateTimeKind.Utc) },
                    new() { Action = "Przydzielono osobę realizującą", ActorName = "Piotr Zieliński", OccurredAt = new DateTime(2026, 7, 24, 13, 0, 0, DateTimeKind.Utc) },
                    new() { Action = "Wniosek zakończony — uprawnienia zostały nadane", ActorName = "Piotr Zieliński", OccurredAt = new DateTime(2026, 7, 25, 10, 0, 0, DateTimeKind.Utc) }
                ]
            },
            new()
            {
                Code = "WIT-2026-0125",
                TypeKey = "purchase",
                Title = "Zakup monitora dla stanowiska W2-14",
                Category = "Zakupy IT",
                CreatedAt = new DateTime(2026, 7, 22, 9, 5, 0, DateTimeKind.Utc),
                CreatedByName = "Jan Kowalski",
                AssigneeName = "Zespół Zakupów IT",
                Department = "Wydział W2",
                CostCenter = "W2-PROD-100",
                Status = RequestStatus.OczekujeNaAkceptacje,
                Priority = Priority.Wysoki,
                SlaProgressPercent = 25,
                SlaDeadlineLabel = "Po akceptacji właściciela budżetu",
                Description = "Zakup monitora 27 cali dla stanowiska konstrukcyjnego W2-14.",
                DynamicFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    ["Kategoria"] = "Sprzęt komputerowy",
                    ["Przedmiot"] = "Monitor 27 cali",
                    ["Liczba sztuk"] = "1",
                    ["Szacowany koszt"] = "1 600 PLN"
                }),
                Attachments = [new() { FileName = "specyfikacja_monitora.pdf", FileSizeBytes = 51200, ContentType = "application/pdf", UploadedBy = "Jan Kowalski" }],
                History =
                [
                    new() { Action = "Wniosek został utworzony", ActorName = "Jan Kowalski", OccurredAt = new DateTime(2026, 7, 22, 9, 5, 0, DateTimeKind.Utc) },
                    new() { Action = "Przekazano do właściwej ścieżki akceptacji", ActorName = "Automatyczny obieg wniosków", OccurredAt = new DateTime(2026, 7, 22, 9, 6, 0, DateTimeKind.Utc) }
                ]
            },
            new()
            {
                Code = "WIT-2026-0122",
                TypeKey = "backup",
                Title = "Odtworzenie katalogu projektu 38WE",
                Category = "Backup i odtwarzanie",
                CreatedAt = new DateTime(2026, 7, 19, 6, 40, 0, DateTimeKind.Utc),
                CreatedByName = "Jan Kowalski",
                AssigneeName = "Zespół Infrastruktury IT",
                Department = "Wydział W2",
                CostCenter = "W2-PROD-100",
                Status = RequestStatus.Zakonczony,
                Priority = Priority.Krytyczny,
                SlaProgressPercent = 100,
                SlaDeadlineLabel = "Zrealizowano 19.07.2026",
                Description = "Odtworzenie usuniętego katalogu dokumentacji projektu 38WE z kopii zapasowej.",
                DynamicFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    ["Operacja"] = "Odtworzenie danych",
                    ["Zasób"] = @"\\fileserver\projekty\38WE",
                    ["Punkt odtworzenia"] = "18.07.2026, 22:00",
                    ["Właściciel danych"] = "Wydział W2"
                }),
                History =
                [
                    new() { Action = "Wniosek został utworzony", ActorName = "Jan Kowalski", OccurredAt = new DateTime(2026, 7, 19, 6, 40, 0, DateTimeKind.Utc) },
                    new() { Action = "Przekazano do właściwej ścieżki akceptacji", ActorName = "Automatyczny obieg wniosków", OccurredAt = new DateTime(2026, 7, 19, 6, 41, 0, DateTimeKind.Utc) },
                    new() { Action = "Przydzielono osobę realizującą", ActorName = "Zespół Infrastruktury IT", OccurredAt = new DateTime(2026, 7, 19, 7, 0, 0, DateTimeKind.Utc) },
                    new() { Action = "Dane zostały odtworzone pomyślnie", ActorName = "Zespół Infrastruktury IT", OccurredAt = new DateTime(2026, 7, 19, 11, 30, 0, DateTimeKind.Utc) }
                ]
            }
        };

        db.Requests.AddRange(requests);

        db.PendingApprovals.AddRange(
            new PendingApproval { ItRequestId = 0, Title = "Zakup laptopa dla konstruktora", SubTitle = "Anna Nowak · szacowany koszt 6 500 zł", IconKey = "purchase" },
            new PendingApproval { ItRequestId = 0, Title = "Dostęp administratora do IFS", SubTitle = "Piotr Kowal · uprawnienie podwyższone", IconKey = "permissions" }
        );

        db.Notifications.AddRange(
            new AppNotification { Title = "Uzupełnij dane WIT-2026-0131", Text = "Brakuje informacji o odbiorcy zewnętrznym i okresie udostępnienia.", IsWarning = true, CreatedAt = DateTime.UtcNow.AddHours(-2) },
            new AppNotification { Title = "WIT-2026-0128 został zakończony", Text = "Uprawnienia do systemu IFS zostały nadane.", CreatedAt = DateTime.UtcNow.AddMinutes(-18) }
        );

        await db.SaveChangesAsync();

        // Fix pending approvals to point to a real request
        var req131 = await db.Requests.FirstAsync(r => r.Code == "WIT-2026-0131");
        var req128 = await db.Requests.FirstAsync(r => r.Code == "WIT-2026-0128");
        var approvals = await db.PendingApprovals.ToListAsync();
        approvals[0].ItRequestId = req128.Id;
        approvals[1].ItRequestId = req128.Id;
        await db.SaveChangesAsync();
    }
}
