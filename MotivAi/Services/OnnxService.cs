using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MotivaAI.Services
{
    public class OnnxService
    {
        private readonly InferenceSession _session;

        public OnnxService()
        {
            // حطي هنا مسار الموديل عندك
            var modelPath = "Models/model.onnx";
            _session = new InferenceSession(modelPath);

            // 👇 نطبع أسماء الـ inputs الموجودة في الموديل
            Console.WriteLine("=== Model Inputs ===");
            foreach (var input in _session.InputMetadata)
            {
                Console.WriteLine($"Input Name: {input.Key}");
                Console.WriteLine($"Dimensions: {string.Join(",", input.Value.Dimensions)}");
            }

            Console.WriteLine("=== Model Outputs ===");
            foreach (var output in _session.OutputMetadata)
            {
                Console.WriteLine($"Output Name: {output.Key}");
            }
        }

        public float Predict(float[] inputData)
        {
            // ⚠️ عدلي الشكل حسب الموديل عندك
            // مثال لو الموديل بيستقبل 4 features
            var tensor = new DenseTensor<float>(inputData, new[] { 1, inputData.Length });

            // ⚠️ هنا لازم تغيري اسم الـ input حسب اللي هيظهرلك في الـ Console
            var inputName = _session.InputMetadata.Keys.First();

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor(inputName, tensor)
            };

            using var results = _session.Run(inputs);

            // ناخد أول output
            var output = results.First().AsEnumerable<float>().ToArray();

            return output[0]; // لو الموديل بيرجع قيمة واحدة
        }
    }
}


