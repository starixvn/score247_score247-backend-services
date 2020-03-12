namespace Soccer.EventProcessors.Matches.MatchEvents
{
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using MassTransit;
    using Score247.Shared;
    using Score247.Shared.Constants;
    using Soccer.Core._Shared.Resources;
    using Soccer.Core.Matches.Models;
    using Soccer.Core.Matches.QueueMessages.MatchEvents;
    using Soccer.Core.Shared.Enumerations;
    using Soccer.Core.Timelines.Models;
    using Soccer.Core.Timelines.QueueMessages;
    using Soccer.Database.Matches.Criteria;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class VarOverEventConsumer : IConsumer<IVarOverEventMessage>
    {
        private static readonly CacheItemOptions MatchInfoCacheOptions = new CacheItemOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(360), //Max minutes of a match
        };

        private readonly IBus messageBus;
        private readonly ICacheManager cacheManager;
        private readonly IDynamicRepository dynamicRepository;
        private readonly ILanguageResourcesService languageResources;

        public VarOverEventConsumer(IBus messageBus,
            ICacheManager cacheManager,
            IDynamicRepository dynamicRepository,
            ILanguageResourcesService languageResources)
        {
            this.messageBus = messageBus;
            this.cacheManager = cacheManager;
            this.dynamicRepository = dynamicRepository;
            this.languageResources = languageResources;
        }

        public async Task Consume(ConsumeContext<IVarOverEventMessage> context)
        {
            var matchEvent = context?.Message?.MatchEvent;

            await PublishDefaultVarOverEventCommentary(matchEvent);
        }

        private async Task PublishDefaultVarOverEventCommentary(MatchEvent matchEvent)
        {
            var canGetTimelineId = long.TryParse(matchEvent.Timeline.Id, out var timelineId);
            if (!canGetTimelineId)
            {
                return;
            }

            TimelineCommentary commentary;
            if (matchEvent.Timeline.Commentaries?.Any() == true)
            {
                commentary = new TimelineCommentary(timelineId, matchEvent.Timeline.Commentaries.ToList());
            }
            else
            {
                var match = await GetAndCacheMatchAsync(matchEvent.MatchId);
                var teamName = match.Teams.FirstOrDefault(team => team.IsHome == matchEvent.Timeline.IsHome)?.Name;
                var comment = new Commentary($"{languageResources.GetString(matchEvent.Timeline.Outcome.DisplayName)} {teamName}");
                commentary = new TimelineCommentary(timelineId, new List<Commentary> { comment });
            }

            await messageBus.Publish<IMatchCommentaryFetchedMessage>(
                    new MatchCommentaryFetchedMessage(matchEvent.LeagueId, matchEvent.MatchId, commentary, Language.en_US, matchEvent.EventDate));
        }

        //TODO Remove Duplicate Code In ReceiveMatchNotificationConsumer
        private async Task<Match> GetAndCacheMatchAsync(string matchId)
        {
            var matchCacheKey = $"{CacheKeys.MATCH_INFO_CACHE_KEY}_{matchId}";

            var match = await cacheManager.GetOrSetAsync(
                matchCacheKey,
                async () =>
                {
                    return await dynamicRepository.GetAsync<Match>(new GetMatchByIdCriteria(matchId, Language.en_US));
                },
                MatchInfoCacheOptions);

            return match;
        }
    }
}