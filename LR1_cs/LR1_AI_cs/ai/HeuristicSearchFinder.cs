using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class HeuristicSearchFinder : AbstractSolutionFinder
    {
        private Game _game = new Game();
        private int countClosed { get; set; }
        private int countOpen { get; set; }
        private State _target = null;
        
        public override List<State> findMoves(State inititalState, State targetState)
        {
            Queue<State> OpenQueue = new Queue<State>();
            Queue<State> CloseQueue = new Queue<State>();

            this._target = targetState;
            
            OpenQueue.Enqueue(inititalState);
            while (OpenQueue.Count != 0)
            {
                State currentState = OpenQueue.Dequeue();
                if (currentState.Equals(targetState))
                    return generateHistory(currentState);
                CloseQueue.Enqueue(currentState);
                List<State> childStates = openState(currentState);
              /*  childStates.ToList().Where(
                        state => !CloseQueue.Contains(state) && !OpenQueue.Contains(state)
                    )
                    .ToList().ForEach(state => OpenQueue.Enqueue(state));      */
            }
            //no solution, return empty history
            return new List<State>();
        }

   
        

        private int calucateHeuristic1(State stateToProcess, State target)
        {
            
            int dif  = 0;
            for (int i = 0; i < 19; i++)
            {
                if (stateToProcess._cells[i].color != target._cells[i].color)
                    dif++;
            }
            return dif;
            
            
        }
    

       
    }
}