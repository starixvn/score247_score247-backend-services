using System.Collections.Generic;

namespace Soccer.NotificationServices.OneSignal.Dtos
{
    public class PushDto
    {
        public string app_id { get; set; }

        public IList<ContentDto> headings { get; set; }

        public IList<ContentDto> contents { get; set; }

        public string[] include_player_ids { get; set; }
    }
}
