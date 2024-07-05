namespace AIService.Entities;

public class ModelChatResponse
{
    public string model { get; set; }
    public string created_at { get; set; }
    public ModelChatMessage message { get; set; }
    public string done_reason { get; set; }
    public bool done { get; set; }
    public long total_duration { get; set; }
    public long load_duration { get; set; }
    public long prompt_eval_count { get; set; }
    public long prompt_eval_duration { get; set; }
    public long eval_count { get; set; }
    public long eval_duration { get; set; }
}
