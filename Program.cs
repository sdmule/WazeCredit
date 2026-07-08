using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WazeCredit.Data;
using WazeCredit.Middleware;
using WazeCredit.Service;
using WazeCredit.Service.LifeTimeExample;
using WazeCredit.Utility.AppSettingsClasses;
using WazeCredit.Utility.DI_Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
//builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();
//builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
//builder.Services.AddSingleton<IMarketForecaster>(new MarketForecasterV2());//this can done while using only singleton as it will create one instance at starting and also we are using here "new" keyword

////Use below way when we don't have abstraction
//builder.Services.AddTransient<MarketForecasterV2>();
//builder.Services.AddSingleton(new MarketForecasterV2());

////Use below way when we have implementation only
//builder.Services.AddTransient(typeof(MarketForecasterV2));
//builder.Services.AddTransient(typeof(IMarketForecaster), typeof(MarketForecasterV2));

builder.Services.AddTransient<TransientService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddSingleton<SingletonService>();
//TryAdd() - checks whether the implementation for this service is already exist and if exist then it will not register the new implementation
//TryAdd() is an extension method provided by Microsoft.Extensions.DependencyInjection.Extensions that registers a service only if a service of that type has not already been registered.
//builder.Services.TryAddTransient<IMarketForecaster, MarketForecaster>();

//Below is the way to register same service multiple times
//builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
//builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();

//Below is the way to replace registration
//builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
//builder.Services.Replace(ServiceDescriptor.Transient<IMarketForecaster, MarketForecaster>());

builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();

//RemoveAll<> - if we want to remove all the implemenatations    
//builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
//builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();
//builder.Services.RemoveAll<IMarketForecaster>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddAppSettingsConfig(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<CustomMiddleware>();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
