using System.Text;
using System.Text.Json;
using MotivAi.Models;

namespace MotivAi.Services
{
    public class RoadmapService
    {
        private readonly HttpClient _httpClient;

        public RoadmapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://mo-medhat-roadmap-generator.hf.space");
        }

        public async Task<RoadmapResponse?> GenerateAsync(RoadmapRequest request)
        {
            var body = new
            {
                goal = request.Goal,
                duration_days = request.DurationDays == 0 ? 30 : request.DurationDays,
                daily_minutes = request.DailyMinutes == 0 ? 15 : request.DailyMinutes,
                level = string.IsNullOrEmpty(request.Level) ? "beginner" : request.Level
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/generate", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<RoadmapResponse>(result,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}