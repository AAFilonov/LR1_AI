using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class InDepthSearchSearcher : AbstractSolutionSearcher
    {
        private Game _game = new Game();
        private int countClosed { get; set; }
        private int countOpen { get; set; }
  
        public override List<State> findMoves(State inititalState, State targetState)
        {
            Stack<State> OpenQueue = new Stack<State>();
            Queue<State> CloseQueue = new Queue<State>();

            OpenQueue.Push(inititalState);
            while (OpenQueue.Count != 0)
            {
                State currentState = OpenQueue.Pop();
                if (currentState.Equals(targetState))
                    return generateHistory(currentState);
                CloseQueue.Enqueue(currentState);
                List<State> childStates = openState(currentState);
                childStates.ToList().Where(
                        state => !CloseQueue.Contains(state) && !OpenQueue.Contains(state)
                    )
                    .ToList().ForEach(state => OpenQueue.Push(state));
            }
            //no solution, return empty history
            return new List<State>();
        }

        

    

    
    }
}