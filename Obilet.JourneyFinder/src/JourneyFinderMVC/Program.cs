using Microsoft.Extensions.Options;
using Middlewares;
using Models.Configuration;
using Repository;
using Repository.Interfaces;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Services.Configure<ObiletApiSettings>(builder.Configuration.GetSection("ObiletApi"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<ObiletApiSettings>>().Value);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IObiletApiClient, ObiletApiClient>();
builder.Services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<IBusLocationService, BusLocationService>();

var app = builder.Build();

app.UseMiddleware<ObiletSessionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();