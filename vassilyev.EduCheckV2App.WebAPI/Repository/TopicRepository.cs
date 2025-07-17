using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Data;
using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Repository;

public class TopicRepository: IRepository<Topic>
{
    private readonly ApplicationDbContext _db;

    public TopicRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<ICollection<Topic>> GetAllAsync()
    {
        return await _db.Topics.AsNoTracking().ToListAsync();
    }

    public async Task<Topic> GetAsync(Guid id)
    {
        return await _db.Topics.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Topic> GetAsync(string topicText)
    {
        return await _db.Topics.AsNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower().StartsWith(topicText.ToLower()));
    }

    public async Task CreateAsync(Topic topic)
    {
        await _db.Topics.AddAsync(topic);
    }

    public void Update(Topic topic)
    {
        _db.Topics.Update(topic);
    }

    public void Remove(Topic topic)
    {
        _db.Topics.Remove(topic);
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}