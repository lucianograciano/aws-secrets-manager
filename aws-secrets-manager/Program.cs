using Amazon;
using Amazon.Runtime;
using aws_secrets_manager;

var builder = WebApplication.CreateBuilder(args);

string? env = builder.Environment.EnvironmentName;
string? appName = builder.Environment.ApplicationName;

// Add services to the container.
builder.Configuration.AddSecretsManager(
    credentials: new BasicAWSCredentials(
        builder.Configuration.GetSection("AWSSM_keys:Key").Value,
        builder.Configuration.GetSection("AWSSM_keys:Secret").Value),
    region: RegionEndpoint.USEast1,
    configurator: options =>
    {
        options.SecretFilter = entry => entry.Name.StartsWith($"{env}_{appName}_");
        options.KeyGenerator = (_, s) => s
            .Replace($"{env}_{appName}_", string.Empty)
            .Replace("__", ":");
        options.PollingInterval = TimeSpan.FromMinutes(1);
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(DatabaseSettings.SectionName));

builder.Services.Configure<KeySettings>(
    builder.Configuration.GetSection(KeySettings.SectionName));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
