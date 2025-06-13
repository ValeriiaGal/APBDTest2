using Microsoft.AspNetCore.Mvc;
using Services;

namespace API;

[ApiController]
public class Controller(IRecordService recordService) : ControllerBase
{
    [HttpGet]
    [Route("/api/records")]
    public async Task<IActionResult> GetRecords(
        [FromQuery] DateTime? fromDate,
        [FromQuery] int? languageId,
        [FromQuery] int? taskId)
    {
        try
        {
            var records = await recordService.GetRecordsAsync(fromDate, languageId, taskId);
            return Ok(records);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}