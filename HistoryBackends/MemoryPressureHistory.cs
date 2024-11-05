public class MemoryPressureHistory : IPressureHistory
{
    private readonly Queue<PressureReading> _pressures = new();
    private readonly int _maxHistorySize;
    public MemoryPressureHistory(int maxHistorySize = 160_000)
    {
        _maxHistorySize = maxHistorySize;
    }
    public Task AddPressureAsync(float pressure)
    {
        _pressures.Enqueue(new PressureReading(DateTime.Now, pressure));
        if (_pressures.Count > _maxHistorySize)
        {
            _pressures.Dequeue();
        }
        return Task.CompletedTask;
    }
    public Task<float> GetCurrentPressureAsync()
    {
        return Task.FromResult(_pressures.LastOrDefault()?.Pressure ?? float.NaN);
    }
    public Task<IEnumerable<PressureReading>> GetPressureHistoryAsync(int count)
    {
        return Task.FromResult(_pressures.TakeLast(count).AsEnumerable());
    }
}