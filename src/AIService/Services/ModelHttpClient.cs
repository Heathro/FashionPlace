using AIService.Entities;

namespace AIService.Services;

public class ModelHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ModelHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<ModelChatResponse> GetAIResponseAsync(ModelChatRequest modelChatRequest)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync
            (
                _config["OllamaUrl"] + "/api/chat",
                modelChatRequest
            );
            return await response.Content.ReadFromJsonAsync<ModelChatResponse>();
        }
        catch
        {
            return new ModelChatResponse
            {
                message = new ModelChatMessage
                {
                    content = "Sorry, something went wrong. Please try again later."
                }
            };
        }        
    }
}
