using System.Collections.Generic;

namespace Soccer.NotificationServices.OneSignal.Dtos
{
    public class PushDto
    {
        public string app_id { get; set; }

        public Dictionary<string, string> headings { get; set; }

        public Dictionary<string, string> contents { get; set; }

        public string[] include_player_ids { get; set; }

        public Dictionary<string, string> data { get; set; }
    }
}
