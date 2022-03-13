using System.Collections.Generic;
using System.Linq;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    public class Game
    {
        private const int INDEX_OF_LAST_ADJACENT_CELL = 5;
        public  const int BOARD_SIZE = 19;
       public static State rotateClockwise(State oldState, int pos)
        {
            var newState = new State(oldState);
            var adjacentCells = newState.getAdjacentCells(pos);
            var temp = adjacentCells[0].color;

            for (int i = 1; i <= INDEX_OF_LAST_ADJACENT_CELL; i++)
            {
                var tempColor = adjacentCells[i].color;
                adjacentCells[i].color = temp;
                temp = tempColor;
            }

            adjacentCells[0].color = temp;
            newState.parent = oldState;
            return newState;
        }
       
       public static State rotateCounterclockwise(State oldState, int pos)
       {
           var newState = new State(oldState);
           var adjacentCells = newState.getAdjacentCells(pos);
           var temp = adjacentCells[INDEX_OF_LAST_ADJACENT_CELL].color;

           for (int i = INDEX_OF_LAST_ADJACENT_CELL-1; i >= 0; i--)
           {
               var tempColor = adjacentCells[i].color;
               adjacentCells[i].color = temp;
               temp = tempColor;
           }

           adjacentCells[INDEX_OF_LAST_ADJACENT_CELL].color = temp;
           newState.parent = oldState;
           return newState;
       }
       public static List<State> getAllChildren(State currentState)
       {
           List<State> children = new List<State>();
           var possibleMoves = State.adjacentCellsMap.Keys.ToList();
           foreach (var newChild in possibleMoves
               .Select(position => Game.rotateClockwise(currentState, position))
               .Where(newChild => !Utils.containsValue(children, newChild) && !currentState.Equals(newChild)))
           {
               children.Add(newChild);
           }

           foreach (var newChild in
               possibleMoves
                   .Select(position => Game.rotateCounterclockwise(currentState, position))
                   .Where(newChild => !Utils.containsValue(children, newChild) && !currentState.Equals(newChild)))
           {
               children.Add(newChild);
           }

           return children;
       }
    }
}