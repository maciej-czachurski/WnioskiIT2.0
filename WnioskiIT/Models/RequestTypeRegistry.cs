namespace WnioskiIT.Models;

public record ApprovalStepConfig(string Name, string Description);

public record DynamicFieldConfig(
    string Id,
    string Label,
    string Type,          // text | number | date | datetime-local | email | select | textarea | checkbox
    bool Required = false,
    bool Full = false,
    string? Placeholder = null,
    string? Hint = null,
    string? DefaultValue = null,
    string? Min = null,
    string? Max = null,
    string? Step = null,
    string[]? Options = null);

public record RequestTypeConfig(
    string Key,
    string Label,
    string Category,
    string IconKey,
    string Sla,
    string DefaultAssignee,
    string Meta,
    string DynamicTitle,
    ApprovalStepConfig[] Approvals,
    DynamicFieldConfig[] Fields);

public static class RequestTypeRegistry
{
    public static readonly Dictionary<string, RequestTypeConfig> All = new()
    {
        ["purchase"] = new RequestTypeConfig(
            Key: "purchase",
            Label: "Wniosek o zakup",
            Category: "Zakupy IT",
            IconKey: "cart",
            Sla: "5 dni roboczych",
            DefaultAssignee: "Zespół Zakupów IT",
            Meta: "Przewidywany czas realizacji: 5 dni roboczych. Wniosek wymaga akceptacji przełożonego i osoby odpowiedzialnej za budżet.",
            DynamicTitle: "Informacje o planowanym zakupie",
            Approvals:
            [
                new("Przełożony wnioskodawcy", "Potwierdzenie zasadności biznesowej zakupu"),
                new("Właściciel budżetu", "Potwierdzenie dostępności środków"),
                new("Dział IT", "Weryfikacja zgodności ze standardami technicznymi")
            ],
            Fields:
            [
                new("purchaseCategory", "Kategoria zakupu", "select", Required: true,
                    Options: ["Sprzęt komputerowy", "Oprogramowanie", "Licencja", "Usługa IT", "Akcesoria", "Inne"]),
                new("purchaseItem", "Przedmiot zakupu", "text", Required: true,
                    Placeholder: "Np. laptop, monitor, licencja Microsoft Visio"),
                new("purchaseQuantity", "Liczba sztuk lub licencji", "number", Required: true, Min: "1", DefaultValue: "1"),
                new("estimatedCost", "Szacowany koszt brutto [PLN]", "number", Required: true, Min: "0", Step: "0.01", Placeholder: "0,00"),
                new("preferredSupplier", "Preferowany dostawca", "text", Placeholder: "Opcjonalnie"),
                new("purchaseSpecification", "Wymagania lub specyfikacja", "textarea", Required: true, Full: true,
                    Placeholder: "Podaj wymagane parametry, model, wersję albo najważniejsze cechy produktu")
            ]),

        ["permissions"] = new RequestTypeConfig(
            Key: "permissions",
            Label: "Wniosek o nadanie uprawnień",
            Category: "Uprawnienia",
            IconKey: "key",
            Sla: "2 dni robocze",
            DefaultAssignee: "Zespół Zarządzania Dostępem",
            Meta: "Przewidywany czas realizacji: 2 dni robocze. Wymagana jest akceptacja przełożonego oraz właściciela wskazanego systemu.",
            DynamicTitle: "Zakres wymaganych uprawnień",
            Approvals:
            [
                new("Przełożony wnioskodawcy", "Potwierdzenie potrzeby dostępu"),
                new("Właściciel systemu", "Akceptacja zakresu uprawnień"),
                new("Administrator systemu", "Realizacja zatwierdzonego dostępu")
            ],
            Fields:
            [
                new("permissionAction", "Rodzaj operacji", "select", Required: true,
                    Options: ["Nadanie uprawnień", "Zmiana uprawnień", "Odebranie uprawnień"]),
                new("systemName", "System lub aplikacja", "text", Required: true,
                    Placeholder: "Np. IFS, SAP, SharePoint"),
                new("permissionUser", "Użytkownik, którego dotyczy wniosek", "text", Required: true,
                    DefaultValue: "Jan Kowalski"),
                new("permissionRole", "Rola lub zakres dostępu", "text", Required: true,
                    Placeholder: "Np. raportowanie, odczyt, administrator"),
                new("accessStart", "Dostęp od", "date", Required: true),
                new("accessEnd", "Dostęp do", "date",
                    Hint: "Pozostaw puste, jeżeli dostęp ma być bezterminowy"),
                new("privilegedAccess", "Dostęp podwyższony lub administracyjny", "checkbox", Full: true)
            ]),

        ["remote-work"] = new RequestTypeConfig(
            Key: "remote-work",
            Label: "Wniosek o pracę zdalną",
            Category: "Praca zdalna",
            IconKey: "house",
            Sla: "2 dni robocze",
            DefaultAssignee: "Service Desk",
            Meta: "Przewidywany czas realizacji: 2 dni robocze. Wniosek wymaga akceptacji przełożonego, a dostęp VPN zostanie zweryfikowany przez IT.",
            DynamicTitle: "Organizacja pracy zdalnej",
            Approvals:
            [
                new("Przełożony wnioskodawcy", "Akceptacja terminu i trybu pracy zdalnej"),
                new("Dział IT", "Weryfikacja sprzętu, VPN i zabezpieczeń")
            ],
            Fields:
            [
                new("remoteMode", "Tryb pracy zdalnej", "select", Required: true,
                    Options: ["Jednorazowa", "Okresowa", "Hybrydowa", "Stała"]),
                new("remoteLocation", "Miejsce wykonywania pracy", "text", Required: true,
                    Placeholder: "Miejscowość lub kraj"),
                new("remoteStart", "Data rozpoczęcia", "date", Required: true),
                new("remoteEnd", "Data zakończenia", "date", Required: true),
                new("remoteEquipment", "Wymagane wyposażenie", "select", Required: true,
                    Options: ["Bez dodatkowego wyposażenia", "Laptop służbowy", "Monitor", "Telefon służbowy", "Laptop i monitor", "Inne"]),
                new("vpnRequired", "Wymagany dostęp VPN", "checkbox"),
                new("remoteSystems", "Systemy używane podczas pracy zdalnej", "textarea", Full: true,
                    Placeholder: "Wymień systemy lub zasoby, do których potrzebny będzie dostęp")
            ]),

        ["backup"] = new RequestTypeConfig(
            Key: "backup",
            Label: "Wniosek o backup",
            Category: "Backup i odtwarzanie",
            IconKey: "database",
            Sla: "3 dni robocze",
            DefaultAssignee: "Zespół Infrastruktury IT",
            Meta: "Przewidywany czas realizacji: 3 dni robocze. Zakres kopii musi zostać potwierdzony przez właściciela danych.",
            DynamicTitle: "Parametry kopii zapasowej",
            Approvals:
            [
                new("Właściciel danych", "Potwierdzenie zakresu i klasyfikacji danych"),
                new("Administrator backupu", "Ocena możliwości technicznych i retencji")
            ],
            Fields:
            [
                new("backupOperation", "Rodzaj operacji", "select", Required: true,
                    Options: ["Utworzenie nowego backupu", "Zmiana konfiguracji backupu", "Odtworzenie danych", "Wyłączenie backupu"]),
                new("backupResource", "Zasób objęty wnioskiem", "text", Required: true,
                    Placeholder: "Serwer, baza danych, katalog lub system"),
                new("dataOwner", "Właściciel danych", "text", Required: true,
                    Placeholder: "Imię, nazwisko lub jednostka"),
                new("backupFrequency", "Częstotliwość", "select", Required: true,
                    Options: ["Jednorazowo", "Co godzinę", "Codziennie", "Co tydzień", "Co miesiąc"]),
                new("backupRetention", "Wymagany okres retencji", "select", Required: true,
                    Options: ["7 dni", "14 dni", "30 dni", "90 dni", "1 rok", "Inny"]),
                new("restoreDate", "Punkt odtworzenia", "datetime-local",
                    Hint: "Wypełnij w przypadku odtwarzania danych"),
                new("backupScope", "Zakres danych lub ścieżka", "textarea", Required: true, Full: true,
                    Placeholder: "Podaj katalogi, bazy, tabele lub inne zasoby")
            ]),

        ["virtual-machine"] = new RequestTypeConfig(
            Key: "virtual-machine",
            Label: "Wniosek o maszynę wirtualną",
            Category: "Infrastruktura wirtualna",
            IconKey: "server",
            Sla: "5 dni roboczych",
            DefaultAssignee: "Zespół Infrastruktury IT",
            Meta: "Przewidywany czas realizacji: 5 dni roboczych. Wymagana jest akceptacja przełożonego oraz administratora infrastruktury.",
            DynamicTitle: "Parametry maszyny wirtualnej",
            Approvals:
            [
                new("Przełożony wnioskodawcy", "Potwierdzenie potrzeby biznesowej"),
                new("Architekt lub administrator IT", "Weryfikacja architektury i dostępnych zasobów"),
                new("Właściciel budżetu", "Akceptacja kosztów infrastruktury, jeżeli wystąpią")
            ],
            Fields:
            [
                new("vmAction", "Rodzaj operacji", "select", Required: true,
                    Options: ["Utworzenie nowej maszyny", "Zmiana parametrów", "Klonowanie maszyny", "Likwidacja maszyny"]),
                new("vmEnvironment", "Środowisko", "select", Required: true,
                    Options: ["Deweloperskie", "Testowe", "Akceptacyjne", "Produkcyjne"]),
                new("vmSystem", "System operacyjny", "select", Required: true,
                    Options: ["Windows Server 2025", "Windows Server 2022", "Ubuntu Server 24.04 LTS", "Red Hat Enterprise Linux", "Inny"]),
                new("vmCpu", "Liczba vCPU", "number", Required: true, Min: "1", Max: "64", DefaultValue: "2"),
                new("vmRam", "Pamięć RAM [GB]", "number", Required: true, Min: "1", Max: "512", DefaultValue: "8"),
                new("vmDisk", "Przestrzeń dyskowa [GB]", "number", Required: true, Min: "20", DefaultValue: "100"),
                new("vmNetwork", "Strefa sieciowa", "select", Required: true,
                    Options: ["Sieć wewnętrzna", "Sieć serwerowa", "DMZ", "Izolowana sieć testowa"]),
                new("vmEndDate", "Planowana data likwidacji", "date",
                    Hint: "Opcjonalnie dla środowisk czasowych"),
                new("vmPurpose", "Przeznaczenie maszyny", "textarea", Required: true, Full: true,
                    Placeholder: "Opisz aplikację, usługę i przewidywane obciążenie")
            ]),

        ["external-sharing"] = new RequestTypeConfig(
            Key: "external-sharing",
            Label: "Wniosek o możliwość udostępniania plików poza spółkę",
            Category: "Bezpieczeństwo informacji",
            IconKey: "share",
            Sla: "2 dni robocze",
            DefaultAssignee: "Zespół Bezpieczeństwa IT",
            Meta: "Przewidywany czas realizacji: 2 dni robocze. Wniosek wymaga akceptacji przełożonego, właściciela danych i zespołu bezpieczeństwa informacji.",
            DynamicTitle: "Zakres udostępnienia zewnętrznego",
            Approvals:
            [
                new("Przełożony wnioskodawcy", "Potwierdzenie celu biznesowego"),
                new("Właściciel danych", "Zgoda na przekazanie wskazanego zakresu informacji"),
                new("Bezpieczeństwo informacji", "Ocena ryzyka i sposobu zabezpieczenia")
            ],
            Fields:
            [
                new("recipientCompany", "Nazwa podmiotu zewnętrznego", "text", Required: true,
                    Placeholder: "Nazwa firmy lub instytucji"),
                new("recipientPerson", "Osoba odbierająca", "text", Required: true,
                    Placeholder: "Imię i nazwisko"),
                new("recipientEmail", "Adres e-mail odbiorcy", "email", Required: true,
                    Placeholder: "odbiorca@partner.pl"),
                new("dataClassification", "Klasyfikacja informacji", "select", Required: true,
                    Options: ["Publiczne", "Wewnętrzne", "Poufne", "Ściśle poufne"]),
                new("sharingMethod", "Sposób udostępnienia", "select", Required: true,
                    Options: ["SharePoint / OneDrive", "Bezpieczny transfer plików", "Szyfrowana wiadomość e-mail", "SFTP", "Inny"]),
                new("sharingStart", "Dostęp od", "date", Required: true),
                new("sharingEnd", "Dostęp do", "date", Required: true),
                new("personalData", "Pliki zawierają dane osobowe", "checkbox"),
                new("sharingScope", "Zakres i cel udostępnienia", "textarea", Required: true, Full: true,
                    Placeholder: "Opisz przekazywane pliki, cel biznesowy i sposób wykorzystania danych")
            ])
    };

    public static RequestTypeConfig Get(string key) =>
        All.TryGetValue(key, out var cfg) ? cfg : All["purchase"];
}
