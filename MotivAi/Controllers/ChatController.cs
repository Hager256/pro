using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotivAi.Models;
using MotivAi.Services;

namespace MotivAi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly RoadmapService _roadmapService;
        private readonly MotivAiContext _context;

        public ChatController(RoadmapService roadmapService, MotivAiContext context)
        {
            _roadmapService = roadmapService;
            _context = context;
        }

        [HttpPost("message")]
        public async Task<IActionResult> Message([FromBody] ChatMessage message)
        {
            var text = message.Text.ToLower();
            string reply;

            if (text.Contains("roadmap") || text.Contains("خطة") || text.Contains("تعلم"))
            {
                var request = new RoadmapRequest
                {
                    Goal = message.Text,
                    DurationDays = 30,
                    DailyMinutes = 15,
                    Level = "beginner"
                };

                var roadmap = await _roadmapService.GenerateAsync(request);

                if (roadmap != null && roadmap.Ok)
                {
                    reply = $"تمام! جهزتلك خطة تعلم:\n\n🎯 الهدف: {roadmap.Data.Goal}\n\n";

                    foreach (var milestone in roadmap.Data.Milestones)
                        reply += $"📌 {milestone.Title} (يوم {milestone.StartDay} - {milestone.EndDay})\n";

                    reply += "\n📅 خطة أول أسبوع:\n";
                    foreach (var day in roadmap.Data.FirstWeekPlan)
                    {
                        reply += $"\nيوم {day.Key}:\n";
                        foreach (var task in day.Value)
                            reply += $"  ✅ {task.Title} ({task.DurationMinutes} دقيقة)\n";
                    }
                }
                else
                {
                    reply = "معلش مقدرتش أجيب الخطة، حاول تاني.";
                }
            }
            else
            {
                reply = "أنا Motiv AI! قولي إيه اللي عايز تتعلمه وهجهزلك خطة 🚀";
            }

            // حفظ المحادثة في قاعدة البيانات
            var chat = new ChatHistory
            {
                UserMessage = message.Text,
                BotReply = reply,
                CreatedAt = DateTime.UtcNow
            };
            _context.ChatHistories.Add(chat);
            await _context.SaveChangesAsync();

            return Ok(new { reply });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _context.ChatHistories
                .OrderByDescending(c => c.CreatedAt)
                .Take(20)
                .ToListAsync();
            return Ok(history);
        }
    }

    public class ChatMessage
    {
        public string Text { get; set; } = "";
    }
}