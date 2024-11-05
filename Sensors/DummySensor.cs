public class DummySensor : IPressureSensor
{
    private readonly Random _random = new();
    public Task<float> GetPressureAsync()
    {
        return Task.FromResult((float)_random.NextDouble() * 100);
    }
}