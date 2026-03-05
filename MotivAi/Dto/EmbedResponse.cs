namespace MotivAi.Dto
{
    public class EmbedResponse
    {
        public int dim { get; set; }
        public float[] vector { get; set; } = Array.Empty<float>();
    }
}
