using Microsoft.Extensions.DependencyInjection;
using Singular.Roulette.Services.BacgroundTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBetService, BetService>();
            services.AddHostedService<FinishTransactionService>();

            return services;
        }
    }
}
