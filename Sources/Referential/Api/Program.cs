using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Referential.Domain.ProductAggregate.Abstractions;
using MlcAccounting.Referential.Domain.UserAggregate;
using MlcAccounting.Referential.Domain.UserAggregate.Abstractions;
using MlcAccounting.Referential.Infrastructure.Repositories;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure((Action<ApiBehaviorOptions>)(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
        new BadRequestObjectResult(new ValidationProblemDetails(actionContext.ModelState)
        {
            Title = "Bad Request",
            Status = (int)HttpStatusCode.BadRequest
        });
}));

builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(_ => _.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddSingleton<IProductRepository, ProductMongoRepository>();

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
