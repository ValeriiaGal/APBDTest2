namespace DTOs;

public class RecordResponseDto
{
    public int Id { get; set; }
    public LanguageDto Language { get; set; }
    public TaskDto Task { get; set; }
    public StudentDto Student { get; set; }
    public long ExecutionTime { get; set; }
    public DateTime Created { get; set; }
}