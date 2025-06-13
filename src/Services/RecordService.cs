using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Services;

public class RecordService : IRecordService
{
    
    private readonly AppDbContext _context;

    public RecordService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<RecordResponseDto>> GetRecordsAsync(DateTime? fromDate, int? languageId, int? taskId)
    {
        var query = _context.Records
            .Include(r => r.Language)
            .Include(r => r.Tasks)
            .Include(r => r.Student)
            .AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(r => r.CreatedAt >= fromDate.Value);

        if (languageId.HasValue)
            query = query.Where(r => r.LanguageId == languageId);

        if (taskId.HasValue)
            query = query.Where(r => r.TaskId == taskId);

        var results = await query
            .OrderByDescending(r => r.CreatedAt)
            .ThenBy(r => r.Student.LastName)
            .ToListAsync();

        return results.Select(r => new RecordResponseDto
        {
            Id = r.Id,
            Language = new LanguageDto
            {
                Id = r.Language.Id,
                Name = r.Language.Name
            },
            Task = new TaskDto
            {
                Id = r.Tasks.Id,
                Name = r.Tasks.Name,
                Description = r.Tasks.Description
            },
            Student = new StudentDto
            {
                Id = r.Student.Id,
                FirstName = r.Student.FirstName,
                LastName = r.Student.LastName,
                Email = r.Student.Email
            },
            ExecutionTime = r.ExecutionTime,
            Created = r.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss")
        });
    }

    public async Task AddRecordAsync(AddRecordRequestDto dto)
    {
        throw new NotImplementedException();
    }
}