using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service.Interface;

namespace Onion.Ioc
{
    public static class IocContainer
    {
        public static void ConfigureIoc(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository.Repository>();
            services.AddTransient<IService, Service.Implementation.Service>();
        }
    }
}