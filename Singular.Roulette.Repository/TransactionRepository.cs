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

        public async Task<Bet> MakeBetTransaction(long AccountId, decimal Amount,Bet bet)
        {

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var addedBet= _context.Bets.Add(bet);
                    var userAccount = _context.Accounts.Find(AccountId);
                    var userGameAccount = _context.Accounts.First(x => x.TypeId == 10 && x.Currency == userAccount.Currency && x.UserId == userAccount.UserId);
                    var userJackpotAccount = _context.Accounts.First(x => x.TypeId == 11 && x.Currency == userAccount.Currency && x.UserId == userAccount.UserId);
                    var mainGameAccount = _context.Accounts.First(x => x.TypeId == 100 && x.Currency == userAccount.Currency);
                    var mainJackpotAccount = _context.Accounts.First(x => x.TypeId == 101 && x.Currency == userAccount.Currency);
                    userAccount.Ballance -= Amount;
                    userGameAccount.Ballance += Amount * (decimal)0.99;
                    userJackpotAccount.Ballance += Amount * (decimal)0.01;

                    _context.Transactions.Add(new Transaction
                    {
                        FromAccountId = userAccount.Id,
                        ToAccountId = userGameAccount.Id,
                        ParentTransactionId = 0,
                        TransactionDate = DateTime.Now,
                        TransactionStatusCode = 200,
                        TransactionTypeId = 10,
                        Amount = Amount * (decimal)0.99
                    });
                    _context.Transactions.Add(new Transaction
                    {
                        FromAccountId = userAccount.Id,
                        ToAccountId = userJackpotAccount.Id,
                        ParentTransactionId = 0,
                        TransactionDate = DateTime.Now,
                        TransactionStatusCode = 200,
                        TransactionTypeId = 11,
                        Amount = Amount * (decimal)0.01
                    });

                    _context.Transactions.Add(new Transaction
                    {
                        FromAccountId = userGameAccount.Id,
                        ToAccountId = mainGameAccount.Id,
                        ParentTransactionId = 0,
                        TransactionDate = DateTime.Now,
                        TransactionStatusCode = 201,
                        TransactionTypeId = 100,
                        Amount = Amount * (decimal)0.99
                    });
                    _context.Transactions.Add(new Transaction
                    {
                        FromAccountId = userJackpotAccount.Id,
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
                    var accountFrom = _context.Accounts.Find(transaction.FromAccountId);
                    var accountTo = _context.Accounts.Find(transaction.ToAccountId);

                    accountFrom.Ballance -= transaction.Amount;
                    accountTo.Ballance += transaction.Amount;

                    transaction.TransactionStatusCode = 200;

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

