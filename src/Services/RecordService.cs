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


    public Task<IEnumerable<RecordResponseDto>> GetRecordsAsync(DateTime? fromDate, int? languageId, int? taskId)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Success, string? ErrorMessage)> AddRecordAsync(AddRecordRequestDto dto)
    {
        throw new NotImplementedException();
    }
}