using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.MongoDB.Console.Samples;
using Sample.MongoDB.Domain.Infrastructure.Informational;
using Sample.MongoDB.Domain.Infrastructure.Informational.Server;
using Sample.MongoDB.Domain.Infrastructure.Informational.Server.Requests;
using Sample.MongoDB.Domain.Models.Informational;


namespace Sample.MongoDB.Console;

internal class ConsoleWorker : BackgroundService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public ConsoleWorker(IHostApplicationLifetime applicationLifetime,
                         ILogger<ConsoleWorker> logger,
                         IMediator mediator)
    {
        _applicationLifetime = applicationLifetime ??
            throw new ArgumentNullException(nameof(applicationLifetime));

        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));

        _mediator = mediator ??
            throw new ArgumentNullException(nameof(mediator));
    }


    #region Override BackgroundService Methods
    public override Task StartAsync(CancellationToken cancellationToken = default)
    {
        this._logger.LogInformation("Worker started.");
        return base.StartAsync(cancellationToken);
    }


    public override Task StopAsync(CancellationToken cancellationToken = default)
    {
        this._logger.LogInformation("Worker stopped.");
        return base.StopAsync(cancellationToken);
    }


    /// <summary>
    /// Entry point for main execution
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Beginning execution");

            cancellationToken.ThrowIfCancellationRequested();

            // Execute Processors
            System.Console.WriteLine();
            await Informational.DisplayServerInformation(_mediator);
            System.Console.WriteLine();

            _logger.LogInformation("Ending execution");
            _applicationLifetime.StopApplication();
        }

        catch (OperationCanceledException ex)
        {
            _logger.LogWarning(ex, "Ending execution do to cancellation token");
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Ending execution do to an exception");
            _applicationLifetime.StopApplication();
        }
    }
    #endregion

}
