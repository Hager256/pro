using Microsoft.AspNetCore.Mvc;
using MotivaAI.Services;
using System.Net.Http.Json;
using MotivAi.Models;
using MotivAi.Dto;

namespace MotivAi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly OnnxService _onnxService;
        private readonly ThresholdService _threshold;

        public PredictionController(OnnxService onnxService, ThresholdService threshold)
        {
            _onnxService = onnxService;
            _threshold = threshold;
        }

        [HttpPost("predict")]
        public IActionResult Predict([FromBody] float[] input)
        {
            if (input == null || input.Length == 0)
                return BadRequest("Input is empty");

            var result = _onnxService.Predict(input);
            var t = _threshold.Threshold;

            return Ok(new
            {
                probability = result,
                threshold = t,
                prediction = result > t ? "Positive" : "Negative"
            });
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            // إنشاء vector بطول 768
            float[] input = new float[768];

            // نحط قيم عشوائية للتجربة
            var random = new Random();
            for (int i = 0; i < 768; i++)
            {
                input[i] = (float)random.NextDouble();
            }

            var result = _onnxService.Predict(input);

            return Ok(new
            {
                inputSize = input.Length,
                probability = result,
                prediction = result > 0.5 ? "Positive" : "Negative"
            });
        }

        [HttpGet("test768")]
        public IActionResult Test768()
        {
            var input = new float[768];
            input[0] = 1f;

            var result = _onnxService.Predict(input);
            var t = _threshold.Threshold;

            return Ok(new
            {
                inputLength = input.Length,
                probability = result,
                threshold = t,
                prediction = result > t ? "Positive" : "Negative"
            });
        }


        [HttpPost("predict-text")]
        public async Task<IActionResult> PredictText([FromBody] TextRequest req)
        {
            if (string.IsNullOrWhiteSpace(req?.Text))
                return BadRequest("Text is empty");

            // 1) اطلب embedding من Python
            using var http = new HttpClient();
            var pyResp = await http.PostAsJsonAsync("http://127.0.0.1:5005/embed", new { text = req.Text });

            if (!pyResp.IsSuccessStatusCode)
                return BadRequest($"Embedding service error: {await pyResp.Content.ReadAsStringAsync()}");

            var embed = await pyResp.Content.ReadFromJsonAsync<EmbedResponse>();
            if (embed?.vector == null || embed.vector.Length != 768)
                return BadRequest($"Embedding dim invalid. Got {(embed?.vector?.Length ?? 0)} expected 768.");

            // 2) شغّل ONNX classifier
            var result = _onnxService.Predict(embed.vector);

            // 3) threshold
            var t = _threshold.Threshold;

            return Ok(new
            {
                text = req.Text,
                embeddingDim = embed.vector.Length,
                probability = result,
                threshold = t,
                prediction = result > t ? "LOW_MOTIVATION" : "MOTIVATED"
            });
        }
    }
}
