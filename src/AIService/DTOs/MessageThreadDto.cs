namespace AIService.DTOs;

public class MessageThreadDto
{
    public Guid Id { get; set; }
    public string ConnectionId { get; set; }
    public ICollection<MessageDto> Messages { get; set; }
}
