using Rystem.TestApp.Business;
using Rystem.TestApp.Data;
using Rystem.TestApp.Domain;
using Rystem.TestApp.Infrastructure.BlobStorageIntegration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddStorageForApp();
//builder.Services.AddStorageWithBlobStorageForApp();
builder.Services.AddRepository<SimpleModel, string>(settings =>
{
    settings
        .WithApiClient()
        .WithHttpClient("localhost:3450") //the api domain, for example I put the localhost:3450 because my Rystem.TestApp.Api starts with that value.
        .WithDefaultRetryPolicy(); //Now I can use the same interface IRepository that I already injected, and automatically the pattern call the api to retrieve data or set data.
});
builder.Services.AddBusinessForApp();
builder.Services.AddCacheForApp();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
