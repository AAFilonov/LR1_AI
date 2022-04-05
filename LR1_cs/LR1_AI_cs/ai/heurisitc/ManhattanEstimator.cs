using System.Collections.Generic;
using System.Linq;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class ManhattenEstimator : IHeuristicEstimator
    {
        private static InWidthSearcher _searcher = new InWidthSearcher();

        public int estimate(State initialState, State targetState)
        {
            int difference = 0;
            List<Cell> notInPlaceCells = findNotInPlaceCells(initialState, targetState);
            foreach (var intialCell in notInPlaceCells)
            {
                int distance = findDistanceToNearestTarget(intialCell, targetState);
                difference += distance;
            }

            return difference;
        }

        private int findDistanceToNearestTarget(Cell intialCell, State targetState)
        {
            var cellsOfSameColor = targetState._cells.Where(cell => cell.color.Equals(intialCell.color)).ToList();
            int minDistance = 10000;
            foreach (var targetCell in cellsOfSameColor)
            {
                var distance = calcManhattenDistance(intialCell.position, targetCell.position);
                if (minDistance > distance)
                    minDistance = distance;
            }

            return minDistance-1;
        }

        private Cell findDistanceToNearestTargetByAdjacent(Cell intialCell, State targetState)
        {
          
            // захардкодить соседние для кадой клетки а не только узловых и перебирать соседнии таким способом
            //заснули в список пар начальную клетку и расстояние 0
            //удалили из него начальное получили все соседние от нее   клетки, засунули их  с расстоянием 1
            //для каждого неперебранного:
            //если наткнулись на искомую клетку - выход с вовзратом расстония ее предка +1
            //иначе удалить клетку из списка, засунуть все потомковые   клетки с расстоянием на 1 больше чем у предка
            
            return null;
        }

        private List<Cell> findNotInPlaceCells(State initialState, State target)
        {
            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < Game.BOARD_SIZE; i++)
            {
                if (initialState._cells[i].color != target._cells[i].color)
                    cells.Add(initialState._cells[i]);
            }

            return cells;
        }

        public static int calcManhattenDistance(int startPosition, int endPosition)
        {
            //тоже возможно переписывание через хардкодные соседние
            State inititalState = new State();
            inititalState._cells[startPosition-1].color = Cell.Color.RED;
            State targetState = new State();
            targetState._cells[endPosition-1].color = Cell.Color.RED;
            var moves = _searcher.findMoves(inititalState, targetState);

            return moves.Count;
        }
    }
}