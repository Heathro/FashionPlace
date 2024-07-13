using Newtonsoft.Json;

namespace business.Services;

public class AuthService
{
    public async Task<bool> Login(string username, string password)
    {
        var client = new HttpClient();
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", username },
            { "password", password },
            { "client_id", "business" },
            { "client_secret", "secret-business" },
            { "scope", "fashionPlace openid profile" }
        };
        var content = new FormUrlEncodedContent(parameters);

        var response = await client.PostAsync("http://localhost:5000/connect/token", content);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);
            await SecureStorage.SetAsync("access_token", tokenResponse.AccessToken);
            return true;
        }

        return false;
    }

    public void Logout()
    {
        SecureStorage.Remove("access_token");
    }

    public async Task<bool> IsLoggedIn()
    {
        var token = await SecureStorage.GetAsync("access_token");
        return !string.IsNullOrEmpty(token);
    }
}

public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
    
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
    
    [JsonProperty("scope")]
    public string Scope { get; set; }
}
