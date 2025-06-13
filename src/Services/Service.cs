using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Services;

public class Service : IService
{
    
    private readonly AppDbContext _context;

    public Service(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<DriverDTO>> Get()
    {
        var query = await _context.Drivers
            .OrderBy(d => d.FirstName)
            .Select(d => new DriverDTO
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthday = d.Birthday
            })
            .ToListAsync();

        if (query.IsNullOrEmpty())
        {
            throw new Exception("No drivers found");
        }
        return query;
    }
}