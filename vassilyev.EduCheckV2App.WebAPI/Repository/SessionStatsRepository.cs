using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Data;
using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Repository;

public class SessionStatsRepository: IRepository<SessionStats>
{
    private readonly ApplicationDbContext _db;

    public SessionStatsRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<ICollection<SessionStats>> GetAllAsync()
    {
        return await _db.SessionStats.AsNoTracking().ToListAsync();
    }

    public async Task<SessionStats> GetAsync(Guid id)
    {
        return await _db.SessionStats.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(SessionStats sessionStats)
    {
        await _db.SessionStats.AddAsync(sessionStats);
    }

    public void Update(SessionStats sessionStats)
    {
        _db.SessionStats.Update(sessionStats);
    }

    public void Remove(SessionStats sessionStats)
    {
        _db.SessionStats.Remove(sessionStats);
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}