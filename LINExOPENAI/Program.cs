using LINExOPENAI.ApplicationService.ADProduct;
using LINExOPENAI.ApplicationService.Interfaces;
using LINExOPENAI.Infrastructure.Network;
using LINExOPENAI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<LineBotConfig, LineBotConfig>((s) => new LineBotConfig
{
    channelSecret = builder.Configuration["LineBot:channelSecret"],
    accessToken = builder.Configuration["LineBot:accessToken"]
});
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IADProductService, ADProductService>();
builder.Services.AddScoped<IBotAPIService, BotAPIService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
