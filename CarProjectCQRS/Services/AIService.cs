using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarProjectCQRS.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "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";

        public AIService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetCarRecommendationAsync(string cars, string requirements)
        {
            try
            {
                var requestData = new
                {
                    model = "llama-3.1-8b-instant",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir araç öneri asistanısın. Elindeki araç listesini analiz edip, fiyat, yakıt türü, vites tipi ve diğer özellikleri dikkate alarak öneri yap." },
                        new { role = "user", content = $"Araçlar: {cars.Substring(0, Math.Min(cars.Length, 1000))}. İhtiyaçlar: {requirements}. En uygun 3 araç öner." }
                    }
                };

                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"API Error: {response.StatusCode} - {response.ReasonPhrase}\nError Details: {errorContent}";
                }

                var result = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(result);
                var choices = doc.RootElement.GetProperty("choices");
                
                if (choices.GetArrayLength() > 0)
                {
                    return choices[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();
                }
                else
                {
                    return "No recommendations available from AI service.";
                }
            }
            catch (JsonException ex)
            {
                return $"JSON Parse Error: {ex.Message}";
            }
            catch (HttpRequestException ex)
            {
                return $"Network Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Unexpected Error: {ex.Message}";
            }
        }
    }
}
