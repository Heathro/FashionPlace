namespace AIService.Entities;

public class Message
{
    public Guid Id { get; set; }
    public bool IsUser { get; set; }
    public string Content { get; set; }
    public Guid MessageThreadId { get; set; }
    public MessageThread MessageThread { get; set; }
}
