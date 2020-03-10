namespace Score247.Shared.Enumerations
{
    public class PushServiceProvider : Enumeration
    {
        public const string AppCenterName = "AppCenter";
        public const string OneSignalName = "OneSignal";

        public static readonly PushServiceProvider AppCenter = new PushServiceProvider(1, AppCenterName);
        public static readonly PushServiceProvider OneSignal = new PushServiceProvider(2, OneSignalName);

        public PushServiceProvider()
        {
        }

        private PushServiceProvider(byte value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
