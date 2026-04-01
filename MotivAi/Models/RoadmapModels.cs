namespace MotivAi.Models
{
    public class RoadmapRequest
    {
        public string Goal { get; set; } = "";
        public int DurationDays { get; set; } = 30;
        public int DailyMinutes { get; set; } = 15;
        public string Level { get; set; } = "beginner";
    }

    public class Milestone
    {
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [System.Text.Json.Serialization.JsonPropertyName("start_day")]
        public int StartDay { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("end_day")]
        public int EndDay { get; set; }
    }

    public class DailyTask
    {
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [System.Text.Json.Serialization.JsonPropertyName("duration_minutes")]
        public int DurationMinutes { get; set; }
    }

    public class RoadmapData
    {
        [System.Text.Json.Serialization.JsonPropertyName("goal")]
        public string Goal { get; set; } = "";

        [System.Text.Json.Serialization.JsonPropertyName("duration_days")]
        public int DurationDays { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("milestones")]
        public List<Milestone> Milestones { get; set; } = new();

        [System.Text.Json.Serialization.JsonPropertyName("first_week_plan")]
        public Dictionary<string, List<DailyTask>> FirstWeekPlan { get; set; } = new();
    }
    public class RoadmapResponse
    {
        public bool Ok { get; set; }
        public RoadmapData Data { get; set; } = new();
    }
}