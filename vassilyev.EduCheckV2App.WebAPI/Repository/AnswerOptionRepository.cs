using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Data;
using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Repository;

public class AnswerOptionRepository: IRepository<AnswerOption>
{
    private readonly ApplicationDbContext _db;

    public AnswerOptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<ICollection<AnswerOption>> GetAllAsync()
    {
        return await _db.AnswerOptions.AsNoTracking().ToListAsync();
    }

    public async Task<AnswerOption> GetAsync(Guid id)
    {
        return await _db.AnswerOptions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AnswerOption> GetAsync(string answerOptionText)
    {
        return await _db.AnswerOptions.AsNoTracking().FirstOrDefaultAsync(x => x.Text.ToLower().StartsWith(answerOptionText.ToLower()));
    }

    public async Task CreateAsync(AnswerOption answerOption)
    {
        await _db.AnswerOptions.AddAsync(answerOption);
    }

    public void Update(AnswerOption answerOption)
    {
        _db.AnswerOptions.Update(answerOption);
    }

    public void Remove(AnswerOption answerOption)
    {
        _db.AnswerOptions.Remove(answerOption);
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}