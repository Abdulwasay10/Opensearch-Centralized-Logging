using Serilog;
using Serilog.Sinks.OpenSearch;

var osUri = Environment.GetEnvironmentVariable("OS_URI") ?? "http://localhost:9201"; // host-run default
var indexFormat = Environment.GetEnvironmentVariable("OS_INDEX") ?? "event-logs-{0:yyyy.MM.dd}";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.FromLogContext()
    .WriteTo.OpenSearch(new OpenSearchSinkOptions(new Uri(osUri))
    {
        AutoRegisterTemplate = true,
        IndexFormat = indexFormat,
        BatchPostingLimit = 50,
        Period = TimeSpan.FromSeconds(2)
    })
    .CreateLogger();

try
{
    Log.Information("Demo app starting at {UtcTime}", DateTime.UtcNow);

    // Example event-style logs
    Log.ForContext("EventType", "OrderPlacedyess")
       .ForContext("OrderId", 1234)
       .ForContext("UserId", "u-789")
       .Information("Order placed successfully.");

    Log.ForContext("Feature", "Payments")
       .ForContext("LatencyMs", 850)
       .Warning("Payment latency higher than threshold.");

    Log.Error(new Exception("SimulatedFailure"), "Something went wrong processing order {OrderId}", 1234);

    Log.Information("Demo app finished.");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.CloseAndFlush();
}
