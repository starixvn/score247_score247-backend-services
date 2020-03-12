using System;
using Soccer.Core.Matches.Models;

namespace Soccer.Core.Notification.QueueMessages
{
    public interface IMatchNotificationReceivedMessage
    {
        string MatchId { get; }

        string LeagueId { get; }

        TimelineEvent Timeline { get; }

        MatchResult MatchResult { get; }

        DateTimeOffset EventDate { get; }
    }

    public class MatchNotificationReceivedMessage : IMatchNotificationReceivedMessage
    {
        public MatchNotificationReceivedMessage(
            string matchId, 
            string leagueId, 
            TimelineEvent timeline, 
            MatchResult matchResult, 
            DateTimeOffset eventDate) 
        {
            MatchId = matchId;
            LeagueId = leagueId;
            Timeline = timeline;
            MatchResult = matchResult;
            EventDate = eventDate;
        }

        public string MatchId { get; }

        public string LeagueId { get; }

        public TimelineEvent Timeline { get; private set; }

        public MatchResult MatchResult { get; }

        public DateTimeOffset EventDate { get; }
    }
}
