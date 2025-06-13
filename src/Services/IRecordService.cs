using DTOs;
using Models;

namespace Services;

public interface IRecordService
{
    Task<IEnumerable<RecordResponseDto>> GetRecordsAsync(DateTime? fromDate, int? languageId, int? taskId);
    Task AddRecordAsync(AddRecordRequestDto dto);
}