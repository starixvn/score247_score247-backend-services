namespace Soccer.EventProcessors.Matches.MatchEvents
{
    using MassTransit;
    using Soccer.Core._Shared.Resources;
    using Soccer.Core.Matches.Models;
    using Soccer.Core.Matches.QueueMessages.MatchEvents;
    using Soccer.Core.Shared.Enumerations;
    using Soccer.Core.Timelines.Models;
    using Soccer.Core.Timelines.QueueMessages;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VarOverEventConsumer : IConsumer<IVarOverEventMessage>
    {
        private readonly IBus messageBus;
        private readonly ILanguageResourcesService languageResources;

        public VarOverEventConsumer(IBus messageBus,
          ILanguageResourcesService languageResources)
        {
            this.messageBus = messageBus;
            this.languageResources = languageResources;
        }

        public async Task Consume(ConsumeContext<IVarOverEventMessage> context)
        {
            var matchEvent = context?.Message?.MatchEvent;

            await PublishDefaultVarOverEventCommentary(matchEvent);
        }

        private async Task PublishDefaultVarOverEventCommentary(MatchEvent matchEvent)
        {
            if (matchEvent.Timeline.Type == EventType.VarOver)
            {
                var canGetTimelineId = long.TryParse(matchEvent.Timeline.Id, out var timelineId);
                if (!canGetTimelineId)
                {
                    return;
                }

                var comment = new Commentary($"{languageResources.GetString(matchEvent.Timeline.Outcome.DisplayName)} {matchEvent.Timeline.Team}");
                var commentary = new TimelineCommentary(timelineId, new List<Commentary> { comment });
                await messageBus.Publish<IMatchCommentaryFetchedMessage>(
                        new MatchCommentaryFetchedMessage(matchEvent.LeagueId, matchEvent.MatchId, commentary, Language.en_US, matchEvent.EventDate));
            }
        }
    }
}