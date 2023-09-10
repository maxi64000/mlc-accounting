using FluentValidation;
using FluentValidation.AspNetCore;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Infrastructure.Repositories;
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
