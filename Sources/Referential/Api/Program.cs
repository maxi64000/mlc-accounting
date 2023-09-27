using FluentValidation;
using FluentValidation.AspNetCore;
using MlcAccounting.Referential.Domain.UserAggregate;
using MlcAccounting.Referential.Domain.UserAggregate.Abstractions;
using MlcAccounting.Referential.Infrastructure.Repositories;
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
    .Configure<UserMongoRepositoryOptions>(builder.Configuration.GetSection("UserMongoRepository"))
    .AddSingleton<IUserRepository, UserMongoRepository>();

builder.Services
    .AddSingleton<UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
