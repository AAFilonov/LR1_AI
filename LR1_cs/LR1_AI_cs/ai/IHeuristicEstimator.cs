using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public interface IHeuristicEstimator
    {
        int estimate(State initialState, State targetState);

    }
}