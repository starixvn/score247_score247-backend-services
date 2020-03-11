namespace Soccer.Core.Matches.QueueMessages.MatchEvents
{
    using Soccer.Core.Matches.Models;

    public interface IVarOverEventMessage
    {
        MatchEvent MatchEvent { get; }
    }

    public class VarOverEventMessage : IVarOverEventMessage
    {
        public VarOverEventMessage(MatchEvent matchEvent)
        {
            MatchEvent = matchEvent;
        }

        public MatchEvent MatchEvent { get; }
    }
}