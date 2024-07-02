namespace AIService.Entities;

public class MessageThread
{
    public Guid Id { get; set; }
    public string ConnectionId { get; set; }
    public ICollection<Message> Messages { get; set; }
}
