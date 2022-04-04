using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class InWidthSearcher : AbstractSolutionSearcher
    {
    
     
        public override List<State> findMoves(State inititalState, State targetState)
        {
            Queue<State> OpenQueue = new Queue<State>();
            Queue<State> CloseQueue = new Queue<State>();

            OpenQueue.Enqueue(inititalState);
            int iterations = 0;
            while (OpenQueue.Count != 0)
            {
                iterations++;
                State currentState = OpenQueue.Dequeue();
                if (currentState.Equals(targetState))
                {
                    updateStatisitcs(CloseQueue.Count, OpenQueue.Count, iterations);
                    return generateHistory(currentState);
                }
                  
                CloseQueue.Enqueue(currentState);
                List<State> childStates = openState(currentState);

                foreach (var child in childStates)
                {
                    if (!Utils.containsValue(CloseQueue , child) &&
                        !Utils.containsValue(OpenQueue, child))
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