using Middlewares;
using Repository;
using Repository.Interfaces;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IObiletApiClient, ObiletApiClient>();
builder.Services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
builder.Services.AddScoped<IJourneyService, JourneyService>();

var app = builder.Build();

app.UseMiddleware<ObiletSessionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();