using Carter;
using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/hello", () => "Hello World!");
app.MapCarter();
app.Run();
