using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Soccer.NotificationPublishers._Shared.Configuration;
using Soccer.NotificationServices.Matches;
using Soccer.NotificationServices.OneSignal._Shared.Configuration;
using Soccer.NotificationServices.OneSignal.Matches;

namespace Soccer.NotificationPublishers._Shared.Middlewares
{
    public static class ServiceMiddleware
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration, IAppSettings appSettings)
        {
            var oneSignalSettings = new OneSignalSettings();
            configuration.GetSection("PushServices:OneSignal").Bind(oneSignalSettings);
            services.AddSingleton<IOneSignalSettings>(oneSignalSettings);

            services.AddSingleton(RestService.For<IPushApi>(oneSignalSettings.ServiceUrl));
            services.AddSingleton<IMatchNotificationService, MatchNotificationService>();
        }
    }
}
