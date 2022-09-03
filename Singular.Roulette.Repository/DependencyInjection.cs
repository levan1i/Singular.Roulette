using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Singular.Roulette.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services,string ConnectionString)
        {


            services.AddTransient<ISpinRepository, SpinRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBetRepository, BetRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();  


            services.AddDbContext<SingularDbContext>(options =>
                 options.UseMySql(ConnectionString,  new MySqlServerVersion(new Version(8, 0, 22))));
          
            return services;
        }
    }
}
