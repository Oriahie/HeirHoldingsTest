using HeirsHolding.Core.Interfaces.Database;
using HeirsHolding.Core.Interfaces.Services;
using HeirsHolding.Infrastructure.DataBase;
using HeirsHolding.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Infrastructure.Extensions
{
    public static class AppServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddTransient<IDataSource, DataSource>();
        }
    }
}
