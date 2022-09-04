using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Singular.Roulette.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.BacgroundTasks
{
   static class FinishJobStat
    {
      public  static bool isEnabled { get; set; } =true;
    }
    internal class FinishTransactionService :BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(60);
    private readonly ILogger<FinishTransactionService> _logger;
    private readonly IServiceScopeFactory _factory;
    private int _executionCount = 0;
       

    public FinishTransactionService(
        ILogger<FinishTransactionService> logger,
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (FinishJobStat.isEnabled)
                {
                        FinishJobStat.isEnabled=false;
                     await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    IUnitOfWork _unitOfWork = asyncScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        var transaction =await _unitOfWork.Transactions.GetBlockedTransactions();

                        foreach(var item in transaction)
                        {
                           await _unitOfWork.Transactions.FinishBlockedFundsTransaction(item.TransactionId);
                        }
                        Thread.Sleep(4000);
                    _executionCount++;
                    _logger.LogInformation(
                        $"Executed PeriodicHostedService - Count: {_executionCount}");
                        FinishJobStat.isEnabled = true;
                }
                else
                {
                    _logger.LogInformation(
                        "Skipped PeriodicHostedService");
                }
                   
            }
            catch (Exception ex)
            {
                    FinishJobStat.isEnabled=true; 
                _logger.LogInformation(
                    $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
            }
        }
    }
}
}
