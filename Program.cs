using FireAndForgetHandler.Data;
using FireAndForgetHandler.Handler;
using FireAndForgetHandler.Model;
using FireAndForgetHandler.Repository;
using FireAndForgetHandler.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
                options.JsonSerializerOptions
                .Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseInMemoryDatabase("ApplicationDb"));

builder.Services.AddSingleton<IFireAndForgetTaskHandler, 
    FireAndForgetTaskHandler>();

builder.Services.AddScoped<IRepository<TaskStatusInfo>, 
    Repository<TaskStatusInfo>>();

builder.Services.AddTransient<EmailService>();
builder.Services.AddScoped<StatusService>();

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
