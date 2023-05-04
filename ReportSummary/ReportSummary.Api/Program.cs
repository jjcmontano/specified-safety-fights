using Microsoft.Azure.Cosmos;
using ReportSummary.Configuration;
using ReportSummary.Services;
using Microsoft.Azure.Cosmos.Fluent;
using Azure.AI.OpenAI;
using static Azure.Core.HttpHeader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CosmosConfiguration>(options => builder.Configuration.GetSection(nameof(CosmosConfiguration)).Bind(options));
builder.Services.Configure<OpenAiConfiguration>(options => builder.Configuration.GetSection(nameof(OpenAiConfiguration)).Bind(options));

builder.Services.AddSingleton(
    serviceProvider =>
    {
        var cosmosConfiguration = builder.Configuration.GetSection(nameof(CosmosConfiguration)).Get<CosmosConfiguration>();

        if (cosmosConfiguration == null)
        {
            throw new Exception("Cosmos DB not configured");
        }

        return new CosmosClientBuilder(
            accountEndpoint: cosmosConfiguration.Endpoint,
            authKeyOrResourceToken: cosmosConfiguration.PrimaryKey)
            .WithSerializerOptions(
                new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                }
            )
            .Build();
    });

builder.Services.AddSingleton(
    serviceProvider =>
    {
        var openAiConfiguration = builder.Configuration.GetSection(nameof(OpenAiConfiguration)).Get<OpenAiConfiguration>();

        if (openAiConfiguration == null)
        {
            throw new Exception("OpenAI not configured");
        }

        return new OpenAIClient(openAiConfiguration.ApiKey);
    });

builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<ISummaryService, SummaryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
