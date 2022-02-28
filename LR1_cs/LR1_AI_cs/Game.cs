using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    public class Game
    {
        private const int INDEX_OF_LAST_ADJACENT_CELL = 5;
       public State rotateClockwise(State oldState, int pos)
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
    }
}