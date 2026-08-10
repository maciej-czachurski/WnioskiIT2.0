using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using WnioskiIT.Components;
using WnioskiIT.Data;
using WnioskiIT.Services;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework Core
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var useSqliteFallback = string.IsNullOrWhiteSpace(defaultConnection)
    || (!OperatingSystem.IsWindows() && defaultConnection.Contains("(localdb)", StringComparison.OrdinalIgnoreCase));

var sqliteConnection = builder.Configuration.GetConnectionString("SqliteConnection")
    ?? $"Data Source={Path.Combine(builder.Environment.ContentRootPath, "App_Data", "wnioskiit.db")}";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (useSqliteFallback)
    {
        var sqliteDirectory = Path.GetDirectoryName(sqliteConnection["Data Source=".Length..]);
        if (!string.IsNullOrWhiteSpace(sqliteDirectory))
        {
            Directory.CreateDirectory(sqliteDirectory);
        }

        options.UseSqlite(sqliteConnection);
    }
    else
    {
        options.UseSqlServer(defaultConnection);
    }
});

// Application services
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IApprovalService, ApprovalService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddSingleton<AppToastService>();

// Blazor + Fluent UI
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

var app = builder.Build();

// Ensure DB is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (useSqliteFallback)
    {
        await db.Database.EnsureCreatedAsync();
    }
    else
    {
        await db.Database.MigrateAsync();
    }

    await DbSeeder.SeedAsync(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
