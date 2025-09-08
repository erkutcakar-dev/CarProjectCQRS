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
            var requestData = new
            {
                model = "mixtral-8x7b-32768",
                messages = new[]
                {
                    new { role = "system", content = "Sen bir araç öneri asistanısın. Elindeki araç listesini analiz edip, fiyat, yakıt türü, vites tipi ve diğer özellikleri dikkate alarak öneri yap." },
                    new { role = "user", content = $"Elimdeki araçlar: {cars}. Kullanıcı ihtiyaçları: {requirements}. Bu ihtiyaçlara göre en uygun 3 araç öner." }
                }
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }
    }
}
