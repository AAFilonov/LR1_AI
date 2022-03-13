using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class HeuristicSearchFinder : ISolutionFinder
    {
        private Game _game = new Game();
        private int countClosed { get; set; }
        private int countOpen { get; set; }
        private State _target = null;

        public async Task<List<State>> findAsync(State inititalState, State targetState)
        {
            Queue<HeuristicState> OpenQueue = new Queue<HeuristicState>();
            Queue<State> CloseQueue = new Queue<State>();

            this._target = targetState;
            
            OpenQueue.Enqueue(toHeurisiticState(inititalState));
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

        HeuristicState toHeurisiticState(State state)
        {
            int score = calucateHeuristic1(state, _target);
            return new HeuristicState(state, score);
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
        private List<State> openState(State currentState)
        {
            List<State> childs = new List<State>();
            List<int> posibleTurns = State.adjacentCellsMap.Keys.ToList();
            foreach (var position in posibleTurns)
            {
              //  HeuristicState child = _game.rotateClockwise(currentState, position);
            
         
               
                //    childs.Add(child);
            }

            return childs;
        }

        private List<State> generateHistory(State currentState)
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

        public List<State> find(State inititalState, State targetState)
        {
            throw new System.NotImplementedException();
        }
    }
}