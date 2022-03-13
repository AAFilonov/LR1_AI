using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class HammingEstimator : IHeuristicEstimator
    {
        public int estimate(State initialState, State targetState)
        {
            return calcHammingDistance(initialState, targetState);
        }

        public static int calcHammingDistance(State stateToProcess, State target)
        {
            int dif = 0;
            for (int i = 0; i < 19; i++)
            {
                if (stateToProcess._cells[i].color != target._cells[i].color)
                    dif++;
            }

            return dif;
        }
    }
}