using System;
using System.Threading.Tasks;
using Fanex.Logging;
using Refit;
using Soccer.Core.Notification.Models;
using Soccer.NotificationServices.Matches;
using Soccer.NotificationServices.OneSignal._Shared.Configuration;
using Soccer.NotificationServices.OneSignal.Dtos;
using Soccer.NotificationServices.OneSignal.Matches.DataMappers;

namespace Soccer.NotificationServices.OneSignal.Matches
{
    public interface IPushApi
    {
        [Post("/notifications")]
        Task Push([Header("authorization")] string apiKey, [Body] PushDto push);
    }

    public class MatchNotificationService : IMatchNotificationService
    {
        private readonly IOneSignalSettings settings;
        private readonly ILogger logger;
        private readonly IPushApi pushApi;

        public MatchNotificationService(
            IOneSignalSettings settings,
            ILogger logger,
            IPushApi pushApi)
        {
            this.settings = settings;
            this.logger = logger;
            this.pushApi = pushApi;
        }

        public async Task<string> PushNotification(MatchEventNotification eventNotification)
        {
            var pushMessage = MatchNotificationMapper.MapPush(settings.AppId, eventNotification);

            try
            {
                await pushApi.Push(settings.ApiKey, pushMessage);
            }
            catch (Exception ex)
            {
                if (ex is ApiException)
                {
                    await logger.ErrorAsync($"Match PushNotification Request Url {(ex as ApiException).Uri.ToString()}", ex);
                }

                await logger.ErrorAsync("Match PushNotification", ex);
            }

            return string.Empty;
        }
    }
}
