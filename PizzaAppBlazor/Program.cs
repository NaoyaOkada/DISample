using PizzaAppBlazor.Components;
using PizzaAppDBAccessLib.Data;
using PizzaAppDBAccessLib.DataServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();

builder.Services.AddTransient<ISQLDataBase, SQLDataBase>();
builder.Services.AddTransient<ISqliteDataBase, SqliteDataBase>();

string? dbChoice = builder.Configuration["DBChoice"];
if ("mssql" == dbChoice)
{
    builder.Services.AddTransient<IDataService, SqlDataService>();
}
else if ("sqlite" == dbChoice)
{
    builder.Services.AddTransient<IDataService, SqliteDataService>();
}
else
{
    builder.Services.AddTransient<IDataService, SqlDataService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
