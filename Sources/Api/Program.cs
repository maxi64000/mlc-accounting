using FluentValidation;
using FluentValidation.AspNetCore;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Infrastructure.UserRepository;
using MlcAccounting.Infrastructure.UserRepository.Dtos;
using MongoDB.Driver;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddMediatR(_ => _.RegisterServicesFromAssemblyContaining<Program>());

builder.Services
    .AddSingleton(new MongoClient(builder.Configuration.GetSection("UserMongoRepository:ConnectionString").Value)
        .GetDatabase(builder.Configuration.GetSection("UserMongoRepository:Database").Value)
        .GetCollection<UserDto>(builder.Configuration.GetSection("UserMongoRepository:Collection").Value))
    .AddSingleton<IUserRepository, UserMongoRepository>();

builder.Services
    .AddSingleton<IUserService, UserService>();

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
