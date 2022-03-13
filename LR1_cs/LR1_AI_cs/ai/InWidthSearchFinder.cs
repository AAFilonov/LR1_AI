using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class InWidthSearchFinder : AbstractSolutionFinder
    {
    
        private int countClosed { get; set; }
        private int countOpen { get; set; }

        public override List<State> findMoves(State inititalState, State targetState)
        {
            Queue<State> OpenQueue = new Queue<State>();
            Queue<State> CloseQueue = new Queue<State>();

            OpenQueue.Enqueue(inititalState);
            while (OpenQueue.Count != 0)
            {
                State currentState = OpenQueue.Dequeue();
                if (currentState.Equals(targetState))
                    return generateHistory(currentState);
                CloseQueue.Enqueue(currentState);
                List<State> childStates = openState(currentState);

                foreach (var child in childStates)
                {
                    if (!Utils.contains(CloseQueue , child) &&
                        !Utils.contains(OpenQueue, child))
                    {
                        OpenQueue.Enqueue(child);
                    }
                }
            }

            //no solution, return empty history
            return new List<State>();
        }
        
    }
}