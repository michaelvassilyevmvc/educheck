using Microsoft.EntityFrameworkCore;
using vassilyev.EduCheckV2App.WebAPI.Data;
using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Repository;

public class UserRepository: IRepository<User>
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<ICollection<User>> GetAllAsync()
    {
        return await _db.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User> GetAsync(Guid id) => await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User> GetAsync(string login) => await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());

    public async Task CreateAsync(User user)
    {
        await _db.Users.AddAsync(user);
    }

    public void Update(User user)
    {
        _db.Users.Update(user);
    }

    public void Remove(User user)
    {
        _db.Users.Remove(user);
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}