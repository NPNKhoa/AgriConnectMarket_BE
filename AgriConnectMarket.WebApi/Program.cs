using AgriConnectMarket.Infrastructure.Data;

using AgriConnectMarket.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
//);

//builder.Services.AddScoped<IAccount, AccountService>();

builder.Services.AddPersistence<AppDbContext>(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddExternalServices(builder.Configuration);


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
