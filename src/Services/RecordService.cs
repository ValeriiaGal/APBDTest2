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
        throw new NotImplementedException();
    }

    public async Task AddRecordAsync(AddRecordRequestDto dto)
    {
        throw new NotImplementedException();
    }
}