using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Data;
using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Repository;

public class QuestionRepository: IRepository<Question>
{
    private readonly ApplicationDbContext _db;

    public QuestionRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<ICollection<Question>> GetAllAsync()
    {
        return await _db.Questions.AsNoTracking().ToListAsync();
    }

    public async Task<Question> GetAsync(Guid id)
    {
        return await _db.Questions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Question> GetAsync(string questionText)
    {
        return await _db.Questions.AsNoTracking().FirstOrDefaultAsync(x => x.Text.ToLower().StartsWith(questionText.ToLower()));
    }

    public async Task CreateAsync(Question question)
    {
        await _db.Questions.AddAsync(question);
    }

    public void Update(Question question)
    {
        _db.Questions.Update(question);
    }

    public void Remove(Question question)
    {
        _db.Questions.Remove(question);
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}