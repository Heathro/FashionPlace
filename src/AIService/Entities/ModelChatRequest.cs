namespace AIService.Entities;

public class ModelChatRequest
{
    public string model { get; set; } = "shop-assistant";
    public List<ModelChatMessage> messages { get; set; } = new List<ModelChatMessage>();
    public bool stream { get; set; } = false;
}
