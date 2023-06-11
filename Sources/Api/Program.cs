using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Infrastructure.UserRepositories;
using MongoDB.Driver;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var t = builder.Configuration.GetSection("UserMongoRepository:ConnectionString").Value;

builder.Services
    .AddSingleton(new MongoClient(builder.Configuration.GetSection("UserMongoRepository:ConnectionString").Value)
        .GetDatabase(builder.Configuration.GetSection("UserMongoRepository:Database").Value)
        .GetCollection<User>(builder.Configuration.GetSection("UserMongoRepository:Collection").Value))
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
