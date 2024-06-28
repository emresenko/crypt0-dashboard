using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using MongoDB.Driver;
using CryptoDashboard.Api.Services;
using CryptoDashboard.Api.Data;


//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL tanýmlama
builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Redis tanýmlama
var redisConfiguration = builder.Configuration.GetConnectionString("RedisConnection");
var multiplexer = ConnectionMultiplexer.Connect(redisConfiguration);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

// MongoDB tanýmlama
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoConnection");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase("CryptoDashboardDb");
    return database;
});

builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<ICryptoService, CryptoService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
