namespace Soccer.EventPublishers.Matches
{
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;
    using Newtonsoft.Json;
    using Score247.Shared.Enumerations;
    using Soccer.Core.Matches.Extensions;
    using Soccer.Core.Matches.QueueMessages;
    using Soccer.EventPublishers.Hubs;
    using Soccer.EventPublishers.Matches.SignalR;

    public class ProcessMatchEventPublisher : IConsumer<IMatchEventProcessedMessage>
    {
        private readonly IHubContext<SoccerHub> hubContext;

        public ProcessMatchEventPublisher(IHubContext<SoccerHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<IMatchEventProcessedMessage> context)
        {
            var matchEvent = context?.Message?.MatchEvent;

            if (matchEvent?.Timeline?.IsBasicEvent() == true && matchEvent.IsLatest)
            {
                var message = JsonConvert.SerializeObject(new MatchEventSignalRMessage(Sport.Soccer.Value, matchEvent));

                const string matchEventName = "MatchEvent";
                await hubContext.Clients.All.SendAsync(matchEventName, message);
            }
        }
    }
}