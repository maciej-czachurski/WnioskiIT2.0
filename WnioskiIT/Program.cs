using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using WnioskiIT.Components;
using WnioskiIT.Data;
using WnioskiIT.Services;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework Core / SQL Server
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Ensure DB is created via migrations and seeded
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    await using var db = await factory.CreateDbContextAsync();
    await db.Database.MigrateAsync();
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
