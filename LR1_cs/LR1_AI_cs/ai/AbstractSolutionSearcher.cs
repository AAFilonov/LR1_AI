using System.Collections.Generic;
using System.Linq;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    abstract public class AbstractSolutionSearcher:ISolutionFinder
    {
        public abstract List<State> findMoves(State inititalState, State targetState);

        public List<State> openState(State currentState)
        {
            return Game.getAllChildren(currentState);
        }

        

        protected List<State> generateHistory(State currentState)
        {
            List<State> solutionHistory = new List<State>();
            solutionHistory.Add(currentState);
            while (currentState.parent != null)
            {
                solutionHistory.Add(currentState.parent);
                currentState = currentState.parent;
            }

            solutionHistory.Reverse();
            return solutionHistory;
        }
    }
}