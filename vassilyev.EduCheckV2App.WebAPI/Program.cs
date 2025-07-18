using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Data;
using vassilyev.EduCheckV2App.WebAPI.Entities;
using vassilyev.EduCheckV2App.WebAPI.Profiles;
using vassilyev.EduCheckV2App.WebAPI.Repository;
using vassilyev.EduCheckV2App.WebAPI.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// добавляем DbContext для БД Postgresql
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddValidatorsFromAssembly(typeof(UserCreateValidator).Assembly);
// Настраиваем службу для подключения всех endpoints
builder.Services.AddCarter();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Question>, QuestionRepository>();
builder.Services.AddScoped<IRepository<Topic>, TopicRepository>();
builder.Services.AddScoped<IRepository<AnswerOption>, AnswerOptionRepository>();
builder.Services.AddScoped<IRepository<SessionStats>, SessionStatsRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCarter();

app.Run();