using DTOs;
using Models;

namespace Services;

public interface IRecordService
{
    Task<IEnumerable<RecordResponseDto>> GetRecordsAsync(DateTime? fromDate, int? languageId, int? taskId);
    Task<(bool Success, string? ErrorMessage)> AddRecordAsync(AddRecordRequestDto dto);
}