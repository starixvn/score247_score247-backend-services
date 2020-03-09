using System;
using System.Linq;
using Soccer.Core._Shared.Enumerations;

namespace Soccer.Core.Matches.Extensions
{
    public static class OutcomeExtensions
    {
        private static readonly OutcomeType[] GoalOutcomeTypes = {
            OutcomeType.Goal,
            OutcomeType.NoGoal,
        };

        public static bool IsGoalOutcome(this OutcomeType outcomeType)
          => GoalOutcomeTypes.Contains(outcomeType);
    }
}
