using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API;

[ApiController]
public class Controller(IRecordService recordService) : ControllerBase
{
    [HttpGet("/api/records")]
    public async Task<ActionResult<IEnumerable<Record>>> GetRecords(
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

    [HttpPost("/api/records")]
    public async Task<ActionResult> AddRecordAsync(
        [FromQuery] AddRecordRequestDto dto)
    {
        try
        {
            await recordService.AddRecordAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}