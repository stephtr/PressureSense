public interface IPressureSensor
{
    Task<float> GetPressureAsync();
}