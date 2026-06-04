using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace AdoPet.Infrastructure;

public static class LocalizationDependencyInjection
{
    public static IServiceCollection AddConfigurationLocalization(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo> { new("en"), new("pt-BR") };

            options.DefaultRequestCulture = new RequestCulture("pt-BR");

            options.SupportedCultures = supportedCultures;

            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new AcceptLanguageHeaderRequestCultureProvider()
            };
        });
        return services;
    }

    public static IApplicationBuilder UseConfiguredLocalization(this IApplicationBuilder app)
    {
        var localizationOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();

        app.UseRequestLocalization(localizationOptions.Value);

        return app;
    }
}
