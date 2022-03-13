using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class InDepthSearchFinder : ISolutionFinder
    {
        private Game _game = new Game();
        private int countClosed { get; set; }
        private int countOpen { get; set; }

        public async Task<List<State>> findAsync(State inititalState, State targetState)
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


        private List<State> openState(State currentState)
        {
            List<State> childs = new List<State>();
            List<int> posibleTurns = State.adjacentCellsMap.Keys.ToList();
            foreach (var position in posibleTurns)
            {
                State child = _game.rotateClockwise(currentState, position);
               
         
               
                    childs.Add(child);
            }

            childs.Reverse();
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