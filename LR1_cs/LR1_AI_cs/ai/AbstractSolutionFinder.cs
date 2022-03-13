using System.Collections.Generic;
using System.Linq;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    abstract public class AbstractSolutionFinder:ISolutionFinder
    {
        public abstract List<State> findMoves(State inititalState, State targetState);

        public List<State> openState(State currentState)
        {
            List<State> childs = new List<State>();
            List<int> posibleMoves = State.adjacentCellsMap.Keys.ToList();
            foreach (var position in posibleMoves)
            {
                State newChild = Game.rotateClockwise(currentState, position);

                if (!Utils.contains(childs,newChild)&& !currentState.Equals(newChild) )
                {
                    childs.Add(newChild);
                }
            }
            
            foreach (var position in posibleMoves)
            {
                State newChild = Game.rotateCounterclockwise(currentState, position);

                if (!Utils.contains(childs,newChild)&& !currentState.Equals(newChild) )
                {
                    childs.Add(newChild);
                }
            }

            return childs;
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