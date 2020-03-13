using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Score247.Shared.Enumerations;
using Soccer.Core.Matches.QueueMessages;
using Soccer.EventPublishers.Hubs;
using Soccer.EventPublishers.Matches.SignalR;

namespace Soccer.EventPublishers.Matches
{
    public class UpdateLiveMatchPublisher : IConsumer<ILiveMatchUpdatedMessage>
    {
        private readonly IHubContext<SoccerHub> hubContext;

        public UpdateLiveMatchPublisher(IHubContext<SoccerHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<ILiveMatchUpdatedMessage> context)
        {
            var message = context.Message;

            if (message != null)
            {
                var newMatches = message.NewMatches;
                var removedMatches = message.RemovedMatches;
                var signalRMessage = JsonConvert.SerializeObject(
                    new LiveMatchSignalRMessage(Sport.Soccer.Value, newMatches, removedMatches));

                await hubContext.Clients.All.SendAsync("LiveMatches", signalRMessage);
            }
        }
    }
}