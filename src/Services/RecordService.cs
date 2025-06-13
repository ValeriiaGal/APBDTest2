using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;

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
            Created = r.CreatedAt
        });
    }

    public async Task AddRecordAsync(AddRecordRequestDto dto)
    {
        var language = await _context.Languages.FindAsync(dto.LanguageId);
        if (language == null)
            throw new Exception("Language not found");
        
        var student = await _context.Students.FindAsync(dto.StudentId);
        if (student == null)
            throw new Exception("Student not found");
        
        var task = await _context.Tasks.FindAsync(dto.Task.Id);
        if (task == null)
        {
            if (string.IsNullOrWhiteSpace(dto.Task.Name) || string.IsNullOrWhiteSpace(dto.Task.Description))
                throw new Exception("Task not found");

            task = new Tasks
            {
                Id = dto.Task.Id,
                Name = dto.Task.Name,
                Description = dto.Task.Description
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        var record = new Record
        {
            LanguageId = dto.LanguageId,
            StudentId = dto.StudentId,
            TaskId = task.Id,
            ExecutionTime = dto.ExecutionTime,
            CreatedAt = dto.Created
        };

        _context.Records.Add(record);
        await _context.SaveChangesAsync();
    }
}