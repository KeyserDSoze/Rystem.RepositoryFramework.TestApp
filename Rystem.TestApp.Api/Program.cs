using RepositoryFramework;
using RepositoryFramework.InMemory;
using Rystem.TestApp.Business;
using Rystem.TestApp.Domain;
using Rystem.TestApp.Infrastructure.BlobStorageIntegration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiFromRepositoryFramework()
    .WithDescriptiveName("Repository Api")
    .WithDocumentation()
    .WithSwagger()
    .WithDefaultCorsWithAllOrigins()
    .ConfigureAzureActiveDirectory(builder.Configuration);
//builder.Services.AddStorageForApp();
//builder.Services.AddStorageWithBlobStorageForApp();
builder.Services.AddRepository<SimpleModel, string>(settings =>
{
    settings
        .WithInMemory()
        .PopulateWithRandomData(2000, 20)
        .WithAutoIncrement(x => x.Value.AutoincrementedValue, 2)
        .WithPattern(x => x.Value.Name, "[A-Z]{5,7}");
    //The in memory repository will be add with 2000 elements, and each element will have a value starting from 2 auto incremented
    // the name will be a random value that will respect the regular expression.

});
builder.Services.AddBusinessForApp();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseApiFromRepositoryFramework()
    .WithDefaultAuthorization();

app.Run();
