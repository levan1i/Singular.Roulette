using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Singular.Roulette.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services,string ConnectionString)
        {


            services.AddDbContext<SingularDbContext>(options =>
options.UseMySql(ConnectionString,  new MySqlServerVersion(new Version(8, 0, 22))));
            //services.AddDbContext<ApplicationDbContext>(opt => opt
            //    .UseSqlServer("Server=DESKTOP-UUBJ14C\\SQLEXPRESS; Database=OrderDb;Trusted_Connection=True;"));
            return services;
        }
    }
}
