using System.Globalization;

public class ThresholdService
{
    public float Threshold { get; }

    public ThresholdService()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Models", "threshold.txt");

        if (!File.Exists(path))
        {
            // fallback بدل ما يعمل crash
            Threshold = 0.5f;
            Console.WriteLine($"[WARN] threshold.txt not found at: {path}. Using default Threshold=0.5");
            return;
        }

        var text = File.ReadAllText(path).Trim();
        if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var t))
        {
            Threshold = 0.5f;
            Console.WriteLine($"[WARN] threshold.txt invalid value: '{text}'. Using default Threshold=0.5");
            return;
        }

        Threshold = t;
        Console.WriteLine($"[INFO] Loaded Threshold={Threshold} from {path}");
    }
}