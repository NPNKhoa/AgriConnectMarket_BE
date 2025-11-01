using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Extensions;
using AgriConnectMarket.Infrastructure.Middlewares;
using AgriConnectMarket.WebApi.Middlewares;
using AgriConnectMarket.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseTokenParsing();

app.UseAuthorization();

app.MapControllers();

app.Run();
