using WazeCredit.Data.Repository;
using WazeCredit.Data.Repository.IRepository;
using WazeCredit.Models;
using WazeCredit.Service;
using WazeCredit.Service.LifeTimeExample;
using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Utility.DI_Config
{

    public static class ConfigureDIServices
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services)
        {
            services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            //builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();
            //builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            //builder.Services.AddSingleton<IMarketForecaster>(new MarketForecasterV2());//this can done while using only singleton as it will create one instance at starting and also we are using here "new" keyword

            ////Use below way when we don't have abstraction
            //builder.Services.AddTransient<MarketForecasterV2>();
            //builder.Services.AddSingleton(new MarketForecasterV2());

            ////Use below way when we have implementation only
            //builder.Services.AddTransient(typeof(MarketForecasterV2));
            //builder.Services.AddTransient(typeof(IMarketForecaster), typeof(MarketForecasterV2));

            services.AddTransient<TransientService>();
            services.AddScoped<ScopedService>();
            services.AddSingleton<SingletonService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //TryAdd() - checks whether the implementation for this service is already exist and if exist then it will not register the new implementation
            //TryAdd() is an extension method provided by Microsoft.Extensions.DependencyInjection.Extensions that registers a service only if a service of that type has not already been registered.
            //builder.Services.TryAddTransient<IMarketForecaster, MarketForecaster>();

            //Below is the way to register same service multiple times
            //builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            //builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();

            //Below is the way to replace registration
            //builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            //builder.Services.Replace(ServiceDescriptor.Transient<IMarketForecaster, MarketForecaster>());

            services.AddTransient<IMarketForecaster, MarketForecaster>();

            //RemoveAll<> - if we want to remove all the implemenatations    
            //builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            //builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();
            //builder.Services.RemoveAll<IMarketForecaster>();

            services.AddScoped<CreditApprovedHigh>();
            services.AddScoped<CreditApprovedLow>();
            services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range =>
            {
                switch (range)
                {
                    case CreditApprovedEnum.High: return ServiceProvider.GetService<CreditApprovedHigh>();
                    case CreditApprovedEnum.Low: return ServiceProvider.GetService<CreditApprovedLow>();
                    default: return ServiceProvider.GetService<CreditApprovedLow>();
                }
            });

            services.AddScoped<IValidationChecker, AddressValidationChecker>();
            services.AddScoped<IValidationChecker, CreditValidationChecker>();
            services.AddScoped<ICreditValidator, CreditValidator>();


            return services;
        }
    }
}
