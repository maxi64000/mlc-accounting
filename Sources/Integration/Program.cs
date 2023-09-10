using FluentValidation;
using FluentValidation.AspNetCore;
using MlcAccounting.Domain.UserIntegrationAggregate;
using MlcAccounting.Domain.UserIntegrationAggregate.Abstractions;
using MlcAccounting.Infrastructure.Clients;
using MlcAccounting.Infrastructure.Consumers;
using MlcAccounting.Infrastructure.Producers;
using MlcAccounting.Infrastructure.Repositories;
using MlcAccounting.Integration.UserIntegrationFeatures;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddMediatR(_ => _.RegisterServicesFromAssemblyContaining<Program>());

builder.Services
    .Configure<UserIntegrationMongoRepositoryOptions>(builder.Configuration.GetSection("UserIntegrationMongoRepository"))
    .AddSingleton<IUserIntegrationRepository, UserIntegrationMongoRepository>();

builder.Services
    .Configure<UserReferentialClientOptions>(builder.Configuration.GetSection("UserReferentialClient"))
    .AddSingleton<IUserReferentialClient, UserReferentialClient>();

builder.Services
    .Configure<UserIntegrationKafkaConsumerOptions>(builder.Configuration.GetSection("UserIntegrationKafkaConsumer"))
    .AddSingleton<IUserIntegrationConsumer, UserIntegrationKafkaConsumer>();

builder.Services
    .Configure<UserIntegrationKafkaProducerOptions>(builder.Configuration.GetSection("UserIntegrationKafkaProducer"))
    .AddSingleton<IUserIntegrationProducer, UserIntegrationKafkaProducer>();

builder.Services.AddSingleton<UserIntegrationService>();

builder.Services.AddHostedService<UserIntegrationWorker>();

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
