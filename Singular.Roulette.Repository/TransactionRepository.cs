using Microsoft.EntityFrameworkCore;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Repository
{
    internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SingularDbContext context) : base(context)
        {
        }

        public async Task<Bet> MakeBetTransaction(long AccountId,string Currency, decimal Amount,Bet bet)
        {

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                
                try
                {

                    //Add Bet Record On Db
                    var addedBet= _context.Bets.Add(bet);
                 
                    //User game account
                    //var userGameAccount = _context.Accounts.First(x => x.TypeId == 10 && x.Currency == userAccount.Currency && x.UserId == userAccount.UserId);
                   
                    //User Jackpot Account
                    //var userJackpotAccount = _context.Accounts.First(x => x.TypeId == 11 && x.Currency == userAccount.Currency && x.UserId == userAccount.UserId);
                    //Main Game Account
                    var mainGameAccount = _context.Accounts.First(x => x.TypeId == 100 && x.Currency ==Currency);
                   
                    //Main Jackpot Account
                    var mainJackpotAccount = _context.Accounts.First(x => x.TypeId == 101 && x.Currency == Currency);
                    

                  

                    //Block transaction From userGame to main game account
                    //Blocked transactions will be processed later by background task
                    _context.Transactions.Add(new Transaction
                    {
                        FromAccountId = AccountId,
                        ToAccountId = mainGameAccount.Id,
                        ParentTransactionId = 0,
                        TransactionDate = DateTime.Now,
                        TransactionStatusCode = 201,
                        TransactionTypeId = 100,
                        Amount = Amount * (decimal)0.99
                    });

                    //Block transaction From userJackpot to main jackpot account
                    _context.Transactions.Add(new Transaction
                    {
                        FromAccountId = AccountId,
                        ToAccountId = mainJackpotAccount.Id,
                        ParentTransactionId = 0,
                        TransactionDate = DateTime.Now,
                        TransactionStatusCode = 201,
                        TransactionTypeId = 101,
                        Amount = Amount * (decimal)0.01

                    });


                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    return addedBet.Entity;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    return null;
                }
            }
        }
        public async Task<bool> MakeBetWinTransaction(long AccountId, decimal Amount)
        {

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var userAccount = _context.Accounts.Find(AccountId);
                    
                    var mainGameAccount = _context.Accounts.First(x => x.TypeId == 100 && x.Currency == userAccount.Currency);
                  
                

                    _context.Transactions.Add(new Transaction
                    {
                        FromAccountId =mainGameAccount.Id,
                        ToAccountId = userAccount.Id,
                        ParentTransactionId = 0,
                        TransactionDate = DateTime.Now,
                        TransactionStatusCode = 201,
                        TransactionTypeId = 201,
                        Amount = Amount
                    });
                 


                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> FinishBlockedFundsTransaction(long transactionId)
        {

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var transaction = _context.Transactions.Find(transactionId);
                    var accountTo = _context.Accounts.Find(transaction.ToAccountId);
                    var accountFrom = _context.Accounts.Find(transaction.FromAccountId);
                   

                    
                    accountFrom.Balance -= transaction.Amount;
                    accountTo.Balance += transaction.Amount;

                    
                    transaction.TransactionStatusCode = 200;
                    if(accountFrom.Balance<0|| accountTo.Balance < 0)
                    {

                    }
                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    return false;
                }
            }

        }

        public async Task<IEnumerable<Transaction>> GetBlockedTransactions()
        {
            return await _context.Transactions.Where(x => x.TransactionStatusCode == 201).ToListAsync();
        }
    }
}

