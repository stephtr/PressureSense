var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPressureHistory, MemoryPressureHistory>();
builder.Services.AddSingleton<IPressureSensor, DummySensor>();
builder.Services.AddHostedService<PressureFetcherService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapGet("/pressure", async (IPressureHistory history) =>
{
    var pressureHistory = await history.GetPressureHistoryAsync(6 * 60 * 8);
    return new
    {
        currentPressure = pressureHistory.LastOrDefault()?.Pressure ?? float.NaN,
        pressureHistory,
    };
});

app.Run();
