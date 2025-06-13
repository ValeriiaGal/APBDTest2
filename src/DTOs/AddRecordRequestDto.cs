namespace DTOs;

public class AddRecordRequestDto
{
    public int LanguageId { get; set; }
    public int StudentId { get; set; }
    public TaskDto Task { get; set; }
    public long ExecutionTime { get; set; }
    public DateTime Created { get; set; }
}