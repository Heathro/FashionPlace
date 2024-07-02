namespace AIService.DTOs;

public class MessageDto
{
    public Guid Id { get; set; }
    public bool IsUser { get; set; }
    public string Content { get; set; }
}
