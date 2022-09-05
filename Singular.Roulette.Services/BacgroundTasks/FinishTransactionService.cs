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

    /// <summary>
    /// Service for finsih blocked transactions
    /// </summary>
    internal class FinishTransactionService :BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(10);
    private readonly ILogger<FinishTransactionService> _logger;
    private readonly IServiceScopeFactory _factory;
   
       

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
                        //Get all transactions with status 201(blocked funds)
                        var transaction =await _unitOfWork.Transactions.GetBlockedTransactions();


                        var grouped = transaction.GroupBy(x => new { x.FromAccountId });


                        //Finish transactions One By One
                      
                        foreach(var item in transaction)
                        {
                           
                            await _unitOfWork.Transactions.FinishBlockedFundsTransaction(item.TransactionId);
                        }
                      
            
                        FinishJobStat.isEnabled = true;
                }
                else
                {
                    _logger.LogInformation(
                        "Skipped FinishTransactionService");
                }
                   
            }
            catch (Exception ex)
            {
                    FinishJobStat.isEnabled=true; 
                _logger.LogError(
                    $"Failed to execute FinishTransactionService with exception message {ex.Message}");
            }
        }
    }
}
}
