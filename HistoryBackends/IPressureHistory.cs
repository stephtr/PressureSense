public record PressureReading(DateTime Timestamp, float Pressure);

public interface IPressureHistory
{
    Task AddPressureAsync(float pressure);
    Task<IEnumerable<PressureReading>> GetPressureHistoryAsync(int count);
}