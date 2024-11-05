public class DummySensor : IPressureSensor
{
    private readonly Random _random = new();
    public float GetPressure()
    {
        return (float)_random.NextDouble() * 100;
    }
}