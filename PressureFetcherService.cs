public class PressureFetcherService : BackgroundService
{
    private readonly IPressureSensor PressureSensor;
    private readonly IPressureHistory PressureHistory;
    private readonly ILogger<PressureFetcherService> Logger;

    public PressureFetcherService(IPressureSensor pressureSensor, IPressureHistory pressureHistory, ILogger<PressureFetcherService> logger)
    {
        PressureSensor = pressureSensor;
        PressureHistory = pressureHistory;
        Logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var pressure = await PressureSensor.GetPressureAsync();
                await PressureHistory.AddPressureAsync(pressure);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error fetching pressure");
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}