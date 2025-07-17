using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }
    public DbSet<SessionStats> SessionStats { get; set; }
}