using System.Collections.Generic;
using Soccer.Core.Shared.Enumerations;

namespace Soccer.NotificationServices.OneSignal._Shared.Extensions
{
    public static class LanguageExtension
    {
        private static readonly Dictionary<string, string> LanguageMapping = new Dictionary<string, string>
        {
            { Language.English, "en" }
        };

        public static string ToOneSignalFormat(this Language language)
        {
            if (!LanguageMapping.ContainsKey(language.DisplayName))
            {
                throw new KeyNotFoundException($"Missing OneSignal definition for {language.DisplayName}");
            }

            return LanguageMapping[language.DisplayName];
        }
    }
}
