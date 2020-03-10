using System.Collections.Generic;
using MessagePack;
using Score247.Shared.Enumerations;

namespace Soccer.Core.Models.Favorites
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class UserFavorite
    {
        public UserFavorite(
            string userId, 
            string deviceId, 
            IList<Favorite> favorites, 
            string platform, 
            string pushService = PushServiceProvider.OneSignalName)
        {
            UserId = userId;
            PushService = pushService ?? PushServiceProvider.OneSignalName;
            DeviceId = deviceId;
            Favorites = favorites;
        }

        public string UserId { get; }

        public string PushService { get; }

        public string DeviceId { get; }

        public IList<Favorite> Favorites { get; }
    }
}
