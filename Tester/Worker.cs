using Switchcraft.Client;

namespace Tester;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISwitch _switch;

    public Worker(ILogger<Worker> logger, ISwitch s)
    {
        _logger = logger;
        _switch = s;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            if(_switch.On("ShouldRunCode"))
            {
                _logger.LogInformation("ShouldRunCode is true");
            }
            else
            {
                _logger.LogInformation("ShouldRunCode is false");
            }

            await Task.Delay(1000, stoppingToken);

            if(_switch.On("ShouldRunCode2"))
            {
                _logger.LogInformation("ShouldRunCode2 is true");
            }
            else
            {
                _logger.LogInformation("ShouldRunCode2 is false");
            }

            await Task.Delay(1000, stoppingToken);

            if(_switch.On("ShouldRunCode3"))
            {
                _logger.LogInformation("ShouldRunCode3 is true");
            }
            else
            {
                _logger.LogInformation("ShouldRunCode3 is false");
            }

            await Task.Delay(1000, stoppingToken);

            if( _switch.On("ShouldRunCode4"))
            {
                _logger.LogInformation("ShouldRunCode4 is true");
            }
            else
            {
                _logger.LogInformation("ShouldRunCode4 is false");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
