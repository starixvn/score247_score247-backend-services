namespace Soccer.NotificationServices.OneSignal._Shared.Configuration
{
    public interface IOneSignalSettings
    {
        string ServiceUrl { get; set; }
#pragma warning restore S3996 // URI properties should not be strings

        string AppId { get; set; }

        string ApiKey { get; set; }
    }

    public class OneSignalSettings : IOneSignalSettings
    {
        public string ServiceUrl { get; set; }

        public string AppId { get; set; }

        public string ApiKey { get; set; }
    }
}
