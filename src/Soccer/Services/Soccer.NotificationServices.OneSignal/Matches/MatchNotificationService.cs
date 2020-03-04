using System;
using System.Threading.Tasks;
using Soccer.Core.Notification.Models;
using Soccer.NotificationServices.Matches;

namespace Soccer.NotificationServices.OneSignal.Matches
{
    public class MatchNotificationService : IMatchNotificationService
    {
        public Task<string> PushNotification(MatchEventNotification eventNotification)
        {
            throw new NotImplementedException();
        }
    }
}
