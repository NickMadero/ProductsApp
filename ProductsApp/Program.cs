using dataLibrary;
using ProductsApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllersWithViews();
// Register IDataAccess with its implementation DataAccess
builder.Services.AddSingleton<IDataAccess,DataAccess>(provider =>
{
    var connectionString = "Server=127.0.0.1;port=3001;database=product;user id=root;password=root"; // Replace with your actual connection string
    return new DataAccess(connectionString);
});


builder.Services.AddHttpClient<DataAccess>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5171");
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseRouting();

app.MapGet("/hello", () => "Hello World!");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
