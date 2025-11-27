using DataAccessLayer.Context;
using DataAccessLayer.Model;

namespace DataAccessLayer.Repositories;

public class UserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }
}