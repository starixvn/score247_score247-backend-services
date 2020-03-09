using Soccer.Core._Shared.Resources;
using Soccer.Core.Matches.Extensions;
using Soccer.Core.Matches.Models;
using Soccer.Core.Shared.Enumerations;
using Soccer.Core.Teams.Models;

namespace Soccer.EventProcessors.Notifications.Models
{
    public class VAROverNotification : TimelineNotification
    {
        private const string EmptyContent = "   ";

        public VAROverNotification(
          ILanguageResourcesService languageResources,
          TimelineEvent timeline,
          Team home,
          Team away,
          byte matchTime,
          MatchResult matchResult) : base(languageResources, timeline, home, away, matchTime, matchResult) { }

        //Add whitespaces for empty body
        public override string Content(string language = Language.English)
        => Timeline.Outcome.IsGoalOutcome() 
            ? $"{HomeTeam.Name} {MatchResult.HomeScore}{TeamSeparator}{MatchResult.AwayScore} {AwayTeam.Name}" 
            : EmptyContent;

        public override string Title(string language = Language.English)
        => $"VAR - {LanguageResources.GetString(Timeline.Outcome.DisplayName)} {TeamReceived.Name}";
    }
}
