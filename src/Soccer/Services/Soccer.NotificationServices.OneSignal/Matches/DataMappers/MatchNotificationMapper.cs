using System.Collections.Generic;
using Soccer.Core.Notification.Models;
using Soccer.NotificationServices.OneSignal.Dtos;

namespace Soccer.NotificationServices.OneSignal.Matches.DataMappers
{
    public static class MatchNotificationMapper
    {
        public static PushDto MapPush(string appId, MatchEventNotification eventNotification)
        => new PushDto
        {
            app_id = appId,
            headings = new Dictionary<string, string> { { "en", eventNotification.Title } },
            contents = new Dictionary<string, string> { { "en", eventNotification.Content } },
            include_player_ids = eventNotification.UserIds
        };
    }
}
