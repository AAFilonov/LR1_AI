using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class BoundedInDepthSearchSearcher : AbstractSolutionSearcher
    {
        private int maxDepth;

        public BoundedInDepthSearchSearcher(int maxDepth)
        {
            this.maxDepth = maxDepth;
        }

        public override List<State> findMoves(State inititalState, State targetState)
        {
            Stack<State> OpenQueue = new Stack<State>();
            Queue<State> CloseQueue = new Queue<State>();

            OpenQueue.Push(inititalState);
            int depth = 0;
            int iterations = 0;
            while (OpenQueue.Count != 0)
            {
                iterations++;
                State currentState = OpenQueue.Pop();
                if (currentState.Equals(targetState))
                {
                    updateStatisitcs(OpenQueue.Count, CloseQueue.Count, iterations);
                    return generateHistory(currentState);
                }
                  
                if (depth >= maxDepth)
                {
                    continue;
                }

                CloseQueue.Enqueue(currentState);
                List<State> childStates = openState(currentState);
                childStates.ToList().Where(
                        state => !CloseQueue.Contains(state) && !OpenQueue.Contains(state)
                    )
                    .ToList().ForEach(state => OpenQueue.Push(state));
                depth++;
            }

            //no solution, return empty history
            return new List<State>();
        }
    }
}