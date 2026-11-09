using Cart.Api.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Cart.Api;

public static class Extension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddScoped<ICartService, CartService>();
        services.AddTransient<ISaleCalcuator, FiveDaysSaleCalcuator>();

        services.AddHttpContextAccessor();

        services.AddDistributedMemoryCache();
        services.AddSession(options => 
        {
            options.IdleTimeout = TimeSpan.FromMinutes(1);
            options.Cookie.MaxAge = TimeSpan.FromMinutes(2);
            options.Cookie.Name = Cart;
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.Path = "/";
            if (isDevelopment)
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            }
            else
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            }
        });
    }

    public static bool DaysLeft(this DateTimeOffset startDate, int day) =>
        startDate.ToUniversalTime().Date.AddDays(day) >= DateTimeOffset.UtcNow.Date;

    public static T? GetSessionValue<T>(this IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);

        var ctx = httpContextAccessor.HttpContext;
        ArgumentNullException.ThrowIfNull(ctx);

        var data = ctx.Session.GetString(Cart);
        if(string.IsNullOrWhiteSpace(data)) return default!;

        return JsonSerializer.Deserialize<T>(data);
    }

    public static void SetSessionValue<T>(this IHttpContextAccessor httpContextAccessor, T value)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        ArgumentNullException.ThrowIfNull(value);

        var ctx = httpContextAccessor.HttpContext;
        ArgumentNullException.ThrowIfNull(ctx);

        ctx.Session.SetString(
            Cart,
            JsonSerializer.Serialize(value));
    }

    public static readonly string Cart = "Cart";
}