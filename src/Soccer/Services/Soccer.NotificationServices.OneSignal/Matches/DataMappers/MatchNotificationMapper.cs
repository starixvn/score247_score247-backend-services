using System.Collections.Generic;
using Soccer.Core._Shared.Enumerations;
using Soccer.Core.Notification.Models;
using Soccer.Core.Shared.Enumerations;
using Soccer.NotificationServices.OneSignal._Shared.Extensions;
using Soccer.NotificationServices.OneSignal.Dtos;

namespace Soccer.NotificationServices.OneSignal.Matches.DataMappers
{
    public static class MatchNotificationMapper
    {
        public static PushDto MapPush(string appId, MatchEventNotification eventNotification)
        => new PushDto
        {
            app_id = appId,
            headings = new Dictionary<string, string> { { Language.en_US.ToOneSignalFormat(), eventNotification.Title } },
            contents = new Dictionary<string, string> { { Language.en_US.ToOneSignalFormat(), eventNotification.Content } },
            include_player_ids = eventNotification.UserIds,
            data = new Dictionary<string, string>
            {
                  { "id", eventNotification.MatchId },
                  { nameof(eventNotification.SportId), eventNotification.SportId.ToString() },
                  { "type", NotificationType.MatchValue.ToString() },
                  { nameof(eventNotification.EventDate), eventNotification.EventDate.ToString() }
            }
        };
    }
}
